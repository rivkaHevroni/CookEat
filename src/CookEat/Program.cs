using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat
{
    public static class Program
    {
        public static async Task Main(string[] Args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            DBManager dbManager = new DBManager();
			//
            //var crawlerManager = new CrawlerManager(dbManager, cancellationToken);

             var searchManager = new SearchManager(dbManager);
			List<string> values = new List<string>
			{
				"פסטה",
				"שמנת",
				"אספרגוס",
				"ברוקולי",
				"לימון"
			};
             var res = searchManager.SearchByIngredients(values);
             foreach (var r in res)
             {
                 Console.WriteLine(r.RecipeTitle);
            }

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

            cancellationTokenSource.Cancel();
        }
    }
}