using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CookEat
{
    public sealed class MakoScraper : Scraper
    {
        public MakoScraper(DBManager dbManager) :
            base(dbManager,"https://www.mako.co.il")
        {
        }

        protected override async Task<Recipe> ScrapeInternalAsync(string url)
        {
            Console.WriteLine($"{nameof(MakoScraper)} {nameof(ScrapeInternalAsync)} started [{nameof(url)}={url}]");

            var web = new HtmlWeb();
            var htmlDoc = await web.LoadWithRetryAsync(url);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").GetAttributeValue("content","");
            var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").GetAttributeValue("content", "");
            var prepTime = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='table_container']/li[@class='titleContainer']/div/span[@itemprop='totalTime']")?.InnerText;
            var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']").GetAttributeValue("content", "");
            var numOfDishes = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients']/h3[@class='IngredientsTitle fontSize']")?.First().InnerText;
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients']/ul[@class='recipeIngredients']/li/span");

            var recipeToAdd = new Recipe
            {
                Id = GetIdFromUrl(link),
                PreparationTime = prepTime,
                Link = link,
                NumberOfDishes = GetNumberOfDishes(numOfDishes),
                Picture = image,
                RecipeTitle = title,
                IngredientsList = CreateIngredientsList(ingredients),
                ValuesToSearch = TokanizationHelper.Tokenaize(title),
                NormalaizedIngredients = CreatenormalaizedIngredientsList(ingredients)
            };

            Console.WriteLine($"{nameof(MakoScraper)} {nameof(ScrapeInternalAsync)} finished.");

            return recipeToAdd;
        }
    }
}
