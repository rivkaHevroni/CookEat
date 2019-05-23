using Humanizer;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using Microsoft.Owin;
using Newtonsoft.Json.Converters;

namespace CookEat
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder DisableCache(this IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                context.Response.Headers["Pragma"] = "no-cache";
                context.Response.Headers["Expires"] = "0";

                return next.Invoke();
            });

            return app;
        }

        public static IAppBuilder UseExceptionHandler(this IAppBuilder app)
        {
            return app.Use(
                async (owinContext, requestHandler) =>
                {
                    var isException = false;
                    owinContext.Response.OnSendingHeaders(
                        _ =>
                        {
                            if (isException)
                            {
                                owinContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                owinContext.Response.ReasonPhrase = HttpStatusCode.InternalServerError.Humanize();
                            }
                        },
                        null);

                    try
                    {
                        await requestHandler();
                    }
                    catch (Exception exception)
                    {
                        isException = true;
                        ExceptionHandler.Handle(exception);
                    }
                });
        }

        public static IAppBuilder UseWebApi(
            this IAppBuilder appBuilder,
            Dictionary<Type, Func<object>> controllerTypeToCreatorMapping)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings = jsonSerializerSettings;
            httpConfiguration.DependencyResolver = new DependencyResolver(controllerTypeToCreatorMapping);
            httpConfiguration.Services.Replace(
                typeof(IHttpControllerSelector),
                new HttpControllerSelector(httpConfiguration, controllerTypeToCreatorMapping.Keys.ToList()));
            httpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
            httpConfiguration.EnsureInitialized();

            return appBuilder.UseWebApi(httpConfiguration);
        }

        public static IAppBuilder ServeStaticFiles(this IAppBuilder appBuilder, string path)
        {
            var pysicalFileSystem = new PhysicalFileSystem(path);
            return appBuilder.UseFileServer(
                new FileServerOptions
                {
                    DefaultFilesOptions =
                    {
                        DefaultFileNames = new[]
                        {
                            "index.html"
                        }
                    },
                    EnableDefaultFiles = true,
                    FileSystem = pysicalFileSystem,
                    StaticFileOptions =
                    {
                        FileSystem = pysicalFileSystem,
                        OnPrepareResponse = context =>
                            context.OwinContext.Response.ContentType = new MediaTypeHeaderValue(context.OwinContext.Response.ContentType)
                            {
                                CharSet = "utf-8"
                            }.ToString(),
                        ServeUnknownFileTypes = true
                    }
                });
        }

        private static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key) =>
                dictionary.TryGetValue(key, out TValue value)
                    ? value
                    : default;

        private class DependencyResolver : IDependencyResolver
        {
            private readonly Dictionary<Type, Func<object>> _controllerTypeToCreatorMapping;

            public DependencyResolver(Dictionary<Type, Func<object>> controllerTypeToCreatorMapping) =>
                _controllerTypeToCreatorMapping = controllerTypeToCreatorMapping;

            public void Dispose()
            {
            }

            public object GetService(Type serviceType) =>
                _controllerTypeToCreatorMapping.GetValueOrDefault(serviceType)?.Invoke();

            public IEnumerable<object> GetServices(Type serviceType) => Array.Empty<object>();

            public IDependencyScope BeginScope() => this;
        }

        private sealed class HttpControllerSelector : DefaultHttpControllerSelector
        {
            private readonly IDictionary<string, HttpControllerDescriptor> _controllerNameToDescriptorMapping;

            public HttpControllerSelector(
                HttpConfiguration httpConfiguration,
                IReadOnlyCollection<Type> controllerTypes)
                : base(httpConfiguration) =>
                    _controllerNameToDescriptorMapping = controllerTypes.ToDictionary(
                        _ => _.Name,
                        _ => new HttpControllerDescriptor(httpConfiguration, _.Name, _));

            public override IDictionary<string, HttpControllerDescriptor> GetControllerMapping() =>
                _controllerNameToDescriptorMapping;
        }
    }
}