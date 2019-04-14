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
            string title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").GetAttributeValue("content","");
            string image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").GetAttributeValue("content", "");
            string prepTime = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='table_container']/li[@class='titleContainer']/div/span[@itemprop='totalTime']").InnerText;
            string link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']").GetAttributeValue("content", "");
            var numOfDishes = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients']/h3[@class='IngredientsTitle fontSize']");
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients']/ul[@class='recipeIngredients']/li/span");

            Recipe recipeToAdd = new Recipe
            {
                Id = GetIdFromUrl(link),
                PreparationTime = prepTime,
                Link = link,
                NumberOfDishes = GetNumberOfDishes(numOfDishes[0].InnerText),
                Picture = image,
                RecipeTitle = title,
                IngredientsList = CreateIngredientsList(ingredients),
				ValuesToSearch = TokanizationHelper.Tokenaize(title),
				NormalaizedIngredients = CreatenormalaizedIngredientsList(ingredients)
			};

            return recipeToAdd;
        }
    }
}
