using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.Tracing;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Integration.WebApi;
using CookEat.Extensions;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace CookEat
{
    public static class Program
    {
        public static async Task Main(string[] Args)
        {
            var htmls = new List<string>
            {
                "https://food.walla.co.il/recipe/653062",
                "https://www.mako.co.il/food-cooking_magazine/healthy-eating-recipes/Recipe-8340ea953c5d951006.htm"
            };

            var dbManager = new DBManager();
            var scrapingManger = new ScrapingManager(dbManager);
            await scrapingManger.ScrapeAsync(htmls);

            using (WebApp.Start(
                new StartOptions("http://*:80"),
                app =>
                {
                    var httpConfiguration = new HttpConfiguration();
                    httpConfiguration.MapHttpAttributeRoutes();
                    httpConfiguration.Formatters.Clear();
                    httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
                    httpConfiguration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    app.
                        UseWebApi(
                            httpConfiguration,
                            new Dictionary<Type, Func<object>>
                            {
                                [typeof(SearchManager)] = () => new SearchManager(dbManager)
                            }).
                        ServeStaticFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "frontEnd"));
                }))
            {
                Console.WriteLine("WebServer Started. Press Any Key To Close The Program...");
                Console.ReadKey();
            }
        }
    }
}