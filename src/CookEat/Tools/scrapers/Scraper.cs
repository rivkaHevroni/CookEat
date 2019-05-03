using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
    public abstract class Scraper
    {
        private readonly string _baseUrl;
        private readonly DBManager _dbManager;

        public Scraper(DBManager dbManager, string baseUrl)
        {
            _dbManager = dbManager;
            _baseUrl = baseUrl;
        }

        protected abstract Task<Recipe> ScrapeInternalAsync(string url);

        public async Task ScrapeAsync(string url)
        {
            try
            {
                var recipe = await ScrapeInternalAsync(url);

                await _dbManager.RecipesCollection.InsertOneAsync(recipe);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
            }
        }

        public bool IsRelevantUrl(string url) =>
            url.StartsWith(_baseUrl);

        public List<string> CreatenormalaizedIngredientsList(HtmlNodeCollection ingredients)
        {
            List<string> normalaizedIngredients = new List<string>();

            for (int currIngredientsIndex = 0; currIngredientsIndex < ingredients.Count; currIngredientsIndex++)
            {
                string ingredient = ingredients[currIngredientsIndex].InnerText;
                List<string> ingredientValues = TokanizationHelper.Tokenaize(ingredient);

                for (int ingredientValueIndex = 0; ingredientValueIndex < ingredientValues.Count; ingredientValueIndex++)
                {
                    normalaizedIngredients.Add(ingredientValues[ingredientValueIndex]);
                }
            }

            return normalaizedIngredients;
        }

        protected string GetIdFromUrl(string url)
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(url)), 0).Replace("-", string.Empty);
        }

        protected int GetNumberOfDishes(string dishes)
        {
            if (dishes == null)
            {
                return 0;
            }

            int numOfDishes = 0;
            var words =
                dishes.
                    Replace("(", String.Empty).
                    Replace(")", String.Empty).
                    Split(' ').
                    SelectMany(str => str.Split('\t')).
                    ToList();

            foreach (var word in words)
            {
                if (int.TryParse(word, out numOfDishes))
                {
                    break;
                }
            }

            return numOfDishes;
        }

        protected List<Ingredient> CreateIngredientsList(HtmlNodeCollection ingredients)
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
                    IngredientsNamesList.Add(ConcatIngredientName(WordsInStr));
                }
                else if (Constants.IngredientNameToAmount.ContainsKey(WordsInStr[0]))
                {
                    double amountValue = Constants.IngredientNameToAmount[WordsInStr[0]];

                    IngredientsListAmounts.Add(amountValue);
                    IngredientsNamesList.Add(ConcatIngredientName(WordsInStr));
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
                Ingredient newIngredient = new Ingredient();

                newIngredient.Quantity = new Quantity(IngredientsListAmounts[currIngredientIndex]);
                newIngredient.Name = IngredientsNamesList[currIngredientIndex];
                ingredientsList.Add(newIngredient);
            }

            return ingredientsList;
        }

        private string ConcatIngredientName(string[] splitedWords)
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