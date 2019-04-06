using AsyncUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookEat
{
    public class ScrapingManager
    {
        private readonly DBManager _dBManager;
        private readonly List<Scraper> _scrapers;


        public ScrapingManager(DBManager dBManager)
        {
            _dBManager = dBManager;
            _scrapers = new List<Scraper>
            {
                new WallaScraper(),
                new MakoScraper()
            };

        }

        public async Task ScrapeAsync(List<string> urls)
        {
             await urls.Select(
                async url =>
                {
                    var recipe = await _scrapers.
                    Single(scraper => scraper.IsRelevantUrl(url)).
                    ScrapeAsync(url);

                    await _dBManager.RecipesCollection.InsertOneAsync(recipe);
                }).ToList();
        }

        public void GetPropertiesFromHTML(string url)
        {

        }
    }
}
