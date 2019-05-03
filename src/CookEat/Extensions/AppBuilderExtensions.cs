using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CookEat
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseWebApi(
            this IAppBuilder appBuilder,
            Dictionary<Type, Func<object>> controllerTypeToCreatorMapping)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
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