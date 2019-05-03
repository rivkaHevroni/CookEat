using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AsyncUtilities;
using Humanizer;

namespace CookEat
{
    public class CrawlerManager
    {
        private List<Crawller> _crawlers;
        private ScrapingManager _scrapingManager;

        public CrawlerManager(DBManager dbManager, CancellationToken cancellationToken)
        {
            _crawlers = new List<Crawller>
            {
                new ShefLavanCrawler(dbManager,cancellationToken),
                new MakoCrawler(dbManager,cancellationToken)
            };

            _scrapingManager = new ScrapingManager(dbManager);

            TaskExtension.RunPeriodicly(
                async () =>
                {
                    var urls =
                        (await _crawlers.
                            Select(async crawler => await crawler.CrawlAsync()).
                            ToList()).
                            SelectMany(list => list).
                            ToList();

                    await _scrapingManager.ScrapeAsync(urls);
                },
                1.Days(),
                cancellationToken);
        }
    }
}