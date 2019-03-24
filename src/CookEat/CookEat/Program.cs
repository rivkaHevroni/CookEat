using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Collections;

namespace CookEat
{
	public class Program
	{
		public static async Task Main(string[] Args)
        {
			UserProfile user = new UserProfile();
			user.Id = 21;
			user.UserID = "21a"; //access token from googelAPI
			user.UserRecipes = new List<string>();
			user.UserRecipes.Add("5");
			user.UserRecipes.Add("6");
			user.UserRecipes.Add("7");
			DBManager db = new DBManager();
			await db.InsertToUserProfileCollection(user);
			await db.RemoveRecipeFromUserProfile(user.UserID, "5");


			/* await DoSomethingAsyncInsert();
			const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("CookEatDB");
			var collection = database.GetCollection<Recipe>("RecipesCollection");
			var filter = Builders<Recipe>.Filter.Empty;
			var result = collection.Find(filter).ToList();
			foreach (Recipe recipe in result)
			{
				Console.WriteLine(recipe.Id);
			}
			*/
		}
	}
}
