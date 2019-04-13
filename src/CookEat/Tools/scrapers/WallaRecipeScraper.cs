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
            string numberOfDishes = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']").InnerText;
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='cont']/ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");

            Recipe recipe = new Recipe();

            recipe.Id = GetIdFromUrl(link);
            recipe.PreparationTime = preperationTime;
            recipe.Link = link;
            recipe.NumberOfDiners = numberOfDishes;
            recipe.Picture = image;
            recipe.RecipeTitle = title;
            recipe.IngredientsList = createIngredientsList(ingredients);

            return recipe;
        }

        private List<Ingredient> createIngredientsList(HtmlNodeCollection ingredients)
        {
            List<int> IngredientsListAmounts = new List<int>();
            List<string> IngredientsNamesList = new List<string>();

            for (int currIngredient = 0; currIngredient < ingredients.Count; currIngredient++)
            {
                string nodeToString = ingredients[currIngredient].InnerText;
                string[] WordsInStr = nodeToString.Split(' ');
                int amount;
                if (int.TryParse(WordsInStr[0], out amount))
                {
                    IngredientsListAmounts.Add(amount);
                    string ingredientName = null;
                    for (int wordToConcatIndex = 1; wordToConcatIndex < WordsInStr.Length; wordToConcatIndex++)
                    {
                        ingredientName += WordsInStr[wordToConcatIndex];
                        ingredientName += ' ';
                    }

                    IngredientsNamesList.Add(ingredientName);

                }
            }

            List<Ingredient> IngredientsList = new List<Ingredient>();

            for (int currIngredientIndex = 0; currIngredientIndex < IngredientsListAmounts.Count; currIngredientIndex++)
            {
                IngredientsList[currIngredientIndex].Quantity = IngredientsListAmounts[currIngredientIndex];
                IngredientsList[currIngredientIndex].Name = IngredientsNamesList[currIngredientIndex];
            }

            return IngredientsList;
        }
    }
}
