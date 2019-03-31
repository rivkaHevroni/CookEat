using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Collections;

using System.Xml;
using HtmlAgilityPack;

namespace CookEat
{
	public class Program
	{
		public static async Task Main(string[] Args)
		{
			/*UserProfile user = new UserProfile();
			user.Id = "231587367162"; //access token from googelAPI
			user.UserRecipes = new List<string>();
			user.UserRecipes.Add("5");
			user.UserRecipes.Add("6");
			user.UserRecipes.Add("7");
            DBManager db= new DBManager();
            UserProfileManager upm = new UserProfileManager(db);
            await upm.InsertToUserProfileCollection(user);
            await upm.RemoveRecipeFromUserProfile(user.Id, "7");
			*/
			var html = @"https://food.walla.co.il/item/3225210";

			HtmlWeb web = new HtmlWeb();

			HtmlDocument htmlDoc = web.Load(html);

			var title = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
			Console.WriteLine(title.OuterHtml);

			var image = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='taboola:image']");
			Console.WriteLine(image.OuterHtml);

			var preperationtime = htmlDoc.DocumentNode.SelectSingleNode("//meta[@itemprop='prepTime']");
			Console.WriteLine(preperationtime.OuterHtml);

			var link = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:url']");
			Console.WriteLine(link.OuterHtml);

			//code for recipe
			var NumberOfDishes1 = htmlDoc.DocumentNode.SelectNodes("//ul[@class='fc recipe-more-info']/li/span[@class='text']");
			if (NumberOfDishes1[NumberOfDishes1.Count - 1].OuterHtml.Contains("סועדים"))
			{
				Console.WriteLine(NumberOfDishes1[NumberOfDishes1.Count - 1].OuterHtml);
			}
			else
			{
				Console.WriteLine("-");
			}
			//code for item
			var NumberOfDishes2 = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li[@itemprop='recipeIngredient']");
			{
				Console.WriteLine(NumberOfDishes2[0].OuterHtml+"!!!!!!!!");
			}
			//code for item
			var ingredientsAmount = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='amount']");
			for (int i = 0; i < ingredientsAmount.Count; i++)
			{
				Console.WriteLine(ingredientsAmount[i].OuterHtml);
			}

			var ingredientsName = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ingredients-table']/li/ul[@class='box']/li[@itemprop='recipeIngredient']/span[@class='name']");
			for (int i = 0; i < ingredientsName.Count; i++)
			{
				Console.WriteLine(ingredientsName[i].OuterHtml);
			}

			//code for recipe

			Console.ReadKey();
		}
	}
}
