using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

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

		public void GetPropertiesFromHTML(string url)
		{
			var html = @"https://food.walla.co.il/recipe/653354";

			HtmlWeb web = new HtmlWeb();

			HtmlDocument htmlDoc = web.Load(html);

			var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
			Console.WriteLine(title.OuterHtml);

			var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='taboola:image']");
			Console.WriteLine(image.OuterHtml);

			var preperationtime = htmlDoc.DocumentNode.SelectSingleNode("//meta[@itemprop='prepTime']");
			Console.WriteLine(preperationtime.OuterHtml);

			var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
			Console.WriteLine(link.OuterHtml);

			var NumOfDiners = htmlDoc.DocumentNode.SelectNodes("//ul[@class='fc recipe-more-info']/li/span[@class='text']");
			Console.WriteLine(NumOfDiners[NumOfDiners.Count - 1].OuterHtml);// to fix
		}
    }
}
