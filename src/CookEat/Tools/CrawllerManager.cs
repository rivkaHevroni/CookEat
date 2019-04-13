using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AsyncUtilities;
using Humanizer;

namespace CookEat
{
    public class CrawllerManager
    {
        private List<Crawller> _crawllers;
        private ScrapingManager _scrapingManager;

        public CrawllerManager(DBManager dbManager, CancellationToken cancellationToken)
        {
            _crawllers = new List<Crawller>
            {
                new ShefLavanCrawler(dbManager,cancellationToken)
            };

            _scrapingManager = new ScrapingManager(dbManager,cancellationToken);

            TaskExtension.RunPeriodicly(
                async () =>
                {
                    var urls =
                        (await _crawllers.
                            Select(async crawler => await crawler.CrawlAsync()).
                            ToList()).
                            SelectMany(list => list).
                            ToList();

                    urls.ForEach(url => _scrapingManager.UrlsQueue.Enqueue(url));
                },
                1.Hours(),
                cancellationToken);
        }
    }
}