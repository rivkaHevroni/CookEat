using System;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace CookEat
{
    public sealed class ShefLavanScraper : Scraper
    {
        public ShefLavanScraper(DBManager dbManager)
            : base(dbManager, "https://www.chef-lavan.co.il/")
        {
        }

        protected override async Task<Recipe> ScrapeInternalAsync(string url)
        {
            Console.WriteLine($"{nameof(ShefLavanScraper)} {nameof(ScrapeInternalAsync)} started [{nameof(url)}={url}]");

            var web = new HtmlWeb();
            var htmlDoc = await web.LoadWithRetryAsync(url);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").GetAttributeValue("content", "");
            var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").GetAttributeValue("content", "");
            var preparationTime = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='short-properties bold']/span[@class='cooking-time']")?.InnerText;
            var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']").GetAttributeValue("content", "");
            var numberOfDishes = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='properties']/span[@class='property']/strong")?.InnerText;
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients col-lg-3 col-md-4 col-sm-6 ']/ul[@class='ingredients-list']/li");

            var recipeToAdd = new Recipe
            {
                Id = GetIdFromUrl(link),
                PreparationTime = preparationTime,
                Link = link,
                NumberOfDishes = GetNumberOfDishes(numberOfDishes),
                Picture = image,
                RecipeTitle = title,
                IngredientsList = CreateIngredientsList(ingredients),
                ValuesToSearch = TokanizationHelper.Tokenaize(title),
                NormalaizedIngredients = CreatenormalaizedIngredientsList(ingredients)
            };

            Console.WriteLine($"{nameof(ShefLavanScraper)} {nameof(ScrapeInternalAsync)} finished.");

            return recipeToAdd;
        }
    }
}