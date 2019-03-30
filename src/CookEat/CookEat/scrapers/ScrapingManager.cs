using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
    public class ScrapingManager
    {
        public WallaScraper WallaScraper;
        public MakoScraper MakoScraper;

        public ScrapingManager()
        {
            WallaScraper = new WallaScraper();
            MakoScraper = new MakoScraper();
        }
    }
}
