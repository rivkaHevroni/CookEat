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

			Recipe recipeToAdd = new Recipe();

			recipeToAdd.Id = GetIdFromUrl(link);
			recipeToAdd.PreparationTime = prepTime;
			recipeToAdd.Link = link;
			recipeToAdd.NumberOfDiners = numOfDishes[0].InnerText;
			recipeToAdd.Picture = image;
			recipeToAdd.RecipeTitle = title;
			recipeToAdd.IngredientList = createIngredientsList(ingredients);

			return recipeToAdd;
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
