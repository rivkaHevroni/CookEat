using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CookEat.Tools.scrapers
{
    class WallaItemScraper : Scraper
    {
        public WallaItemScraper()
            : base("https://food.walla.co.il/item/")
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
            string NumOfDishes;
            var NumberOfDishesArray = htmlDoc.DocumentNode.SelectNodes("//ul[@class='fc recipe-more-info']/li/span[@class='text']");
            if (NumberOfDishesArray[NumberOfDishesArray.Count - 1].GetAttributeValue("content", "").Contains("סועדים"))
            {
                NumOfDishes = (NumberOfDishesArray[NumberOfDishesArray.Count - 1].GetAttributeValue("content", ""));
            }
            else
            {
                NumOfDishes = "-";
            }
            var ingredientsAmount = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='amount']");
            var ingredientsName = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='name']");

            Recipe recipeToAdd = new Recipe();
            recipeToAdd.Id = GetIdFromUrl(link);
            recipeToAdd.Link= link;
            recipeToAdd.NumberOfDiners = NumOfDishes;
            recipeToAdd.Picture = image;
            recipeToAdd.PreparationTime = preperationTime;
            recipeToAdd.RecipeTitle = title;
            recipeToAdd.IngredientsList = createIngredientsList(ingredientsAmount, ingredientsName);
            return recipeToAdd;
        }

        private List<Ingredient> createIngredientsList(HtmlNodeCollection ingredientsAmount, HtmlNodeCollection ingredientsName)
        {
            List<int> IngredientsListAmounts = new List<int>();
            List<string> TempIngredientsNamesList = new List<string>();
            List<string> IngredientsNamesList = new List<string>();

            for (int i = 0; i < IngredientsNamesList.Count; i++)
            {
                TempIngredientsNamesList.Add(ingredientsName[i].InnerText);
            }

            for (int currIngredient = 0; currIngredient < ingredientsAmount.Count; currIngredient++)
            {
                string nodeToString = ingredientsAmount[currIngredient].InnerText;
                string[] WordsInStr = nodeToString.Split(' ');
                int amount;
                if (int.TryParse(WordsInStr[0], out amount))
                {
                    IngredientsListAmounts.Add(amount);
                    string UpdataedIngredientName= null;
                    for (int wordToConcatIndex = 1; wordToConcatIndex < WordsInStr.Length; wordToConcatIndex++)
                    {
                        UpdataedIngredientName += WordsInStr[wordToConcatIndex];
                    }

                    UpdataedIngredientName += TempIngredientsNamesList[currIngredient];
                    IngredientsNamesList.Add(UpdataedIngredientName);
                }
            }
        
            List<Ingredient> IngredientsList = new List<Ingredient>();

            for (int currIngredient = 0; currIngredient < ingredientsAmount.Count; currIngredient++)
            {
                IngredientsList[currIngredient].Quantity = IngredientsListAmounts[currIngredient];
                IngredientsList[currIngredient].Name = IngredientsNamesList[currIngredient];
            }
            return IngredientsList;
        }




    }
}
