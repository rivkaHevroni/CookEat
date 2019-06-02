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
            try
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;
                var dbManager = new DBManager();
                var googleApiHelper = new GoogleApiHelper();
                //var crawlerManager = new CrawlerManager(dbManager, cancellationToken);
            

                using (WebApp.Start(
                    new StartOptions("http://*:80"),
                    app =>
                    {
                        app.
                            DisableCache().
                            UseExceptionHandler().
                            UseWebApi(
                                new Dictionary<Type, Func<object>>
                                {
                                    [typeof(SearchManager)] = () => new SearchManager(dbManager,googleApiHelper),
                                    [typeof(UserProfileManager)] = () => new UserProfileManager(dbManager,googleApiHelper)
                                }).
                            ServeStaticFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "frontEnd"));
                    }))
                {
                    Console.WriteLine("WebServer Started. Press Any Key To Close The Program...");
                    Console.ReadKey();
                }

                cancellationTokenSource.Cancel();
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
                Environment.Exit(1);
            }
        }
    }
}