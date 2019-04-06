using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

namespace CookEat
{
    public class WallaScraper : Scraper
    {
        public WallaScraper()
            : base("https://food.walla.co.il/")
        {
        }

        public override async Task<Recipe> ScrapeAsync(string url)
        {
            var web = new HtmlWeb();

            var htmlDoc = await web.LoadFromWebAsync(url);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
            Console.WriteLine(title.OuterHtml);

            var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='taboola:image']");
            Console.WriteLine(image.OuterHtml);

            var preperationTime = htmlDoc.DocumentNode.SelectSingleNode("//meta[@itemprop='prepTime']");
            Console.WriteLine(preperationTime.OuterHtml);

            var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
            Console.WriteLine(link.OuterHtml);

            //code for item
            /*var NumberOfDishes1 = htmlDoc.DocumentNode.SelectNodes("//ul[@class='fc recipe-more-info']/li/span[@class='text']");
            if (NumberOfDishes1[NumberOfDishes1.Count - 1].OuterHtml.Contains("סועדים"))
            {
                Console.WriteLine(NumberOfDishes1[NumberOfDishes1.Count - 1].OuterHtml);
            }
            else
            {
                Console.WriteLine("-");
            }
            */
            //code for recipe
            var numberOfDishes2 = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");

            Console.WriteLine(numberOfDishes2.InnerText);

            //code for item
            /*var ingredientsAmount = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='amount']");
            for (int i = 0; i < ingredientsAmount.Count; i++)
            {
                Console.WriteLine(ingredientsAmount[i].OuterHtml);
            }

            var ingredientsName = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='name']");
            for (int i = 0; i < ingredientsName.Count; i++)
            {
                Console.WriteLine(ingredientsName[i].OuterHtml);
            }
            */
            //code for recipe
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='cont']/ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");
            for (int i = 1; i < ingredients.Count; i++)
            {
                Console.WriteLine(ingredients[i].InnerText);
            }

            return new Recipe();
        }
    }
}
