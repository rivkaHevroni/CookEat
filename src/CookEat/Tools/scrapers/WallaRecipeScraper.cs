using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookEat
{
    public class WallaRecipeScraper : Scraper
    {
        public WallaRecipeScraper()
            : base("https://food.walla.co.il/recipe/")
        {
        }

        public override async Task<Recipe> ScrapeAsync(string url)
        {
            var web = new HtmlWeb();

            var htmlDoc = await web.LoadFromWebAsync(url);

            string title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").GetAttributeValue("content", "");

            string image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='taboola:image']").GetAttributeValue("content", "");

            string preperationTime = htmlDoc.DocumentNode.SelectSingleNode("//meta[@itemprop='prepTime']").GetAttributeValue("content", "");

            string link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']").GetAttributeValue("content", "");

            string numberOfDishes2 = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']").InnerText;

            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='cont']/ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");
            List<string> IngredientsList = new List<string>();
            for (int i = 0; i < ingredients.Count; i++)
            {
                IngredientsList.Add(ingredients[i].InnerText);
            }

            return new Recipe();
        }
    }
}
