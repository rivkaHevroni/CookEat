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
            for (int i = 0; i < ingredientsAmount.Count; i++)
            {
                IngredientsListAmounts.Add(int.Parse(ingredientsAmount[i].InnerText));
            }

            List<string> IngredientsNamesList = new List<string>();
            for (int i = 0; i < IngredientsNamesList.Count; i++)
            {
                IngredientsNamesList.Add(ingredientsName[i].InnerText);
            }

            List<Ingredient> IngredientsList = new List<Ingredient>();
            for (int i = 0; i < ingredientsAmount.Count; i++)
            {
                IngredientsList[i].Quantity = int.Parse(ingredientsAmount[i].InnerText);
                IngredientsList[i].Name = ingredientsName[i].InnerText;
            }
            return IngredientsList;
        }




    }
    }
}
