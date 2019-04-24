using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat
{
    public sealed class ScrapingManager
    {
        private readonly List<Scraper> _scrapers;


        public ScrapingManager(DBManager dBManager)
        {
            _scrapers = new List<Scraper>
            {
                new ShefLavanScraper(dBManager),
                new MakoScraper(dBManager)
            };
        }

        public async Task ScrapeAsync(List<string> urls)
        {
            foreach (var url in urls)
            {
                await _scrapers.
                    Single(scraper => scraper.IsRelevantUrl(url)).
                    ScrapeAsync(url);
            }
        }
    }
}
