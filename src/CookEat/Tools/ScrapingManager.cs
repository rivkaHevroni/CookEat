using AsyncUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookEat.Tools.scrapers;
using Humanizer;
using TaskExtensions = AsyncUtilities.TaskExtensions;

namespace CookEat
{
    public class ScrapingManager
    {
        private readonly DBManager _dBManager;
        private readonly List<Scraper> _scrapers;

        public Queue<string> UrlsQueue;

        public ScrapingManager(DBManager dBManager, CancellationToken cancellationToken)
        {
            _dBManager = dBManager;
            _scrapers = new List<Scraper>
            {
                new WallaItemScraper(),
                new MakoScraper()
            };

            UrlsQueue = new Queue<string>();

            TaskExtension.RunPeriodicly(
                async () =>
                {
                    var urlList = new List<string>();
                    while (UrlsQueue.Count != 0)
                    {
                        urlList.Add(UrlsQueue.Dequeue());
                    }

                    await ScrapeAsync(urlList);
                },
                1.Hours(),
                cancellationToken);
        }

        public async Task ScrapeAsync(List<string> urls)
        {
             await urls.Select(
                async url =>
                {
					Recipe recipe = await _scrapers.
                    Single(scraper => scraper.IsRelevantUrl(url)).
                    ScrapeAsync(url);

                    //await _dBManager.RecipesCollection.InsertOneAsync(recipe);
                }).ToList();
        }

        public void GetPropertiesFromHTML(string url)
        {

        }
    }
}
