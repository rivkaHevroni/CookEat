using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Collections;

using System.Xml;
using HtmlAgilityPack;
using System.Text;

namespace CookEat
{
    public class Program
    {
        public static async Task Main(string[] Args)
        {
            //walla
            var html = @"https://food.walla.co.il/recipe/653062";

            HtmlWeb web = new HtmlWeb();

            HtmlDocument htmlDoc = await web.LoadFromWebAsync(html);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
            Console.WriteLine(title.OuterHtml);

            var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='taboola:image']");
            Console.WriteLine(image.OuterHtml);

            var preperationtime = htmlDoc.DocumentNode.SelectSingleNode("//meta[@itemprop='prepTime']");
            Console.WriteLine(preperationtime.OuterHtml);

            var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
            Console.WriteLine(link.OuterHtml);

            //code for item
            /*var NumberOfDishes1 = htmlDoc.DocumentNode.SelectNodes("//ul[@class='fc recipe-more-info']/li/span[@class='text']");
            if (NumberOfDishes1[NumberOfDishes1.Count - 1].OuterHtml.Contains("סועדים"))
            {
                Console.WriteLine(NumberOfDishes1[NumberOfDishes1.Count - 1].OuterHtml);
            }
            else
            {
                Console.WriteLine("-");
            }
            */
            //code for recipe
            var NumberOfDishes2 = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");

            Console.WriteLine(NumberOfDishes2.InnerText);

            //code for item
            /*var ingredientsAmount = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='amount']");
            for (int i = 0; i < ingredientsAmount.Count; i++)
            {
                Console.WriteLine(ingredientsAmount[i].OuterHtml);
            }

            var ingredientsName = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='name']");
            for (int i = 0; i < ingredientsName.Count; i++)
            {
                Console.WriteLine(ingredientsName[i].OuterHtml);
            }
            */
            //code for recipe
            var ingredients = htmlDoc.DocumentNode.SelectNodes("//div[@class='cont']/ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");
            for (int i = 1; i < ingredients.Count; i++)
            {
                Console.WriteLine(ingredients[i].InnerText);
            }

            //mako
            /*var html = @"https://www.mako.co.il/food-cooking_magazine/healthy-eating-recipes/Recipe-8340ea953c5d951006.htm";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
            Console.WriteLine(title.OuterHtml);

            var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
            Console.WriteLine(image.OuterHtml);

            var preperationTime = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='table_container']/li[@class='titleContainer']/div/span[@itemprop='totalTime']");
            Console.WriteLine(preperationTime.OuterHtml);

            var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
            Console.WriteLine(link.OuterHtml);

            var numOfDishes = htmlDoc.DocumentNode.SelectNodes("//div[@class='ingredients']/h3[@class='IngredientsTitle fontSize']");
            Console.WriteLine(numOfDishes[0].OuterHtml);

            var ingredients = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='ingredients']/ul[@class='recipeIngredients']");
            Console.WriteLine(ingredients.OuterHtml);
*/
            Console.ReadKey();
        }
    }
}
