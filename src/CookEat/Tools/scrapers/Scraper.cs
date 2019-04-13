using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using HtmlAgilityPack;

namespace CookEat
{
    public abstract class Scraper
    {
        public string BaseUrl { get; }

        public Scraper(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public abstract Task<Recipe> ScrapeAsync(string url);

        public bool IsRelevantUrl(string url) =>
            url.StartsWith(BaseUrl);

        protected uint GetIdFromUrl(string url)
        {
            return BitConverter.ToUInt32(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(url)), 0);
        }

        protected int GetNumberOfDishes(string dishes)
        {
            int numOfDishes = 0;
            string[] WordsInStr = dishes.Split(' ');

            for (int wordIndex = 0; wordIndex < WordsInStr.Length; wordIndex++)
            {
                int.TryParse(WordsInStr[wordIndex], out numOfDishes);
                break;
            }

            return numOfDishes;
        }

		protected List<Ingredient> createIngredientsList(HtmlNodeCollection ingredients)
		{
			List<double> IngredientsListAmounts = new List<double>();
			List<string> IngredientsNamesList = new List<string>();

			for (int currIngredient = 0; currIngredient < ingredients.Count; currIngredient++)
			{
				string nodeToString = ingredients[currIngredient].InnerText;
				string[] WordsInStr = nodeToString.Split(' ');
				double amount;
				if (double.TryParse(WordsInStr[0], out amount))
				{
					IngredientsListAmounts.Add(amount);
					IngredientsNamesList.Add(concatIngredientName(WordsInStr));

				}
				else if (Constants.IngredientNameToAmount.ContainsKey(WordsInStr[0]))
				{
					double amountValue = Constants.IngredientNameToAmount[WordsInStr[0]];

					IngredientsListAmounts.Add(amountValue);
					IngredientsNamesList.Add(concatIngredientName(WordsInStr));
				}
				else
				{
					IngredientsListAmounts.Add(0);
					IngredientsNamesList.Add(ingredients[currIngredient].InnerText);
				}
			}

			List<Ingredient> ingredientsList = new List<Ingredient>();

			for (int currIngredientIndex = 0; currIngredientIndex < IngredientsListAmounts.Count; currIngredientIndex++)
			{
				ingredientsList[currIngredientIndex].Quantity.Amount = IngredientsListAmounts[currIngredientIndex];
				ingredientsList[currIngredientIndex].Name = IngredientsNamesList[currIngredientIndex];
			}

			return ingredientsList;
		}

		private string concatIngredientName(string[] splitedWords)
		{
			string ingredientName = null;
			for (int wordToConcatIndex = 1; wordToConcatIndex < splitedWords.Length; wordToConcatIndex++)
			{
				ingredientName += splitedWords[wordToConcatIndex];
				ingredientName += ' ';
			}

			return ingredientName;
		}
	}
}
