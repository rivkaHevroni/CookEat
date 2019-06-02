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

        private Scraper GetRelevantScraper(string url)
        {
            Scraper relevantScraper=null;
            foreach (var scraper in _scrapers)
            {
                if (scraper.IsRelevantUrl(url))
                {
                     relevantScraper = scraper;
                }
            }
            return relevantScraper;
        } 

        public async Task ScrapeAsync(List<string> urls)
        {
            foreach (var url in urls)
            {
              Scraper relevantScraper = GetRelevantScraper(url);
              await relevantScraper.ScrapeAsync(url);

            }
        }
    }
}
