using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
    public class MakoScraper : Scraper
    {
        public MakoScraper() : base("http://www.mako.co.il")
        {
        }

        public override Task<Recipe> ScrapeAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
