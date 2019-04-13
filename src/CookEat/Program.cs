using CookEat.Extensions;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;

namespace CookEat
{
    public static class Program
    {
        public static async Task Main(string[] Args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var htmls = new List<string>
            {
                "https://food.walla.co.il/recipe/653062",
                "https://www.mako.co.il/food-cooking_magazine/healthy-eating-recipes/Recipe-8340ea953c5d951006.htm"
            };


            DBManager dbManager = new DBManager();
            ScrapingManager scrapingManger = new ScrapingManager(dbManager);
            await scrapingManger.ScrapeAsync(htmls);

            using (WebApp.Start(
                new StartOptions("http://*:80"),
                app =>
                {
                    app.
                        UseWebApi(
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