using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CookEat
{
    public class MakoScraper : Scraper
    {
        public MakoScraper() : base("https://www.mako.co.il")
        {
        }

        public override async Task<Recipe> ScrapeAsync(string url)
        {
            var web = new HtmlWeb();
            var htmlDoc = await web.LoadFromWebAsync(url);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
            Console.WriteLine(title.OuterHtml);

            var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
            Console.WriteLine(image.OuterHtml);

            var preperationTime = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='table_container']/li[@class='titleContainer']/div/span[@itemprop='totalTime']");
            Console.WriteLine(preperationTime.OuterHtml);

            var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
            Console.WriteLine(link.OuterHtml);

            var numOfDishes = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients']/h3[@class='IngredientsTitle fontSize']");
            Console.WriteLine(numOfDishes[0].OuterHtml);

            var ingredients = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='ingredients']/ul[@class='recipeIngredients']");
            Console.WriteLine(ingredients.OuterHtml);
            return new Recipe();
        }
    }
}
