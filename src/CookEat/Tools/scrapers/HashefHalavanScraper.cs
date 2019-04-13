﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CookEat.Tools.scrapers
{
    public class HashefHalavanScraper : Scraper
    {
        public HashefHalavanScraper() : base("https://www.chef-lavan.co.il/")
        {
        }

        public override async Task<Recipe> ScrapeAsync(string url)
        {
            var html = @"https://www.chef-lavan.co.il/%D7%A4%D7%90%D7%99-%D7%92%D7%96%D7%A8-%D7%95%D7%AA%D7%A4%D7%95%D7%97%D7%99-%D7%90%D7%93%D7%9E%D7%94";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            string title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']").GetAttributeValue("content", "");
            string image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").GetAttributeValue("content", "");
            string preperationTime = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='short-properties bold']/span[@class='cooking-time']").InnerText;
            string link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']").GetAttributeValue("content", "");
            string numberOfDishes = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='properties']/span[@class='property']/strong").InnerText;
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients col-lg-3 col-md-4 col-sm-6 ']/ul[@class='ingredients-list']/li");

            Recipe recipeToAdd = new Recipe
            {
                Id = GetIdFromUrl(link),
                PreparationTime = preperationTime,
                Link = link,
                NumberOfDishes = GetNumberOfDishes(numberOfDishes),
                Picture = image,
                RecipeTitle = title,
                IngredientsList = createIngredientsList(ingredients)
            };

            return recipeToAdd;
        }

        private List<Ingredient> createIngredientsList(HtmlNodeCollection ingredients)
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
                    string ingredientName = null;
                    for (int wordToConcatIndex = 1; wordToConcatIndex < WordsInStr.Length; wordToConcatIndex++)
                    {
                        ingredientName += WordsInStr[wordToConcatIndex];
                        ingredientName += ' ';
                    }

                    IngredientsNamesList.Add(ingredientName);

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


    }
}