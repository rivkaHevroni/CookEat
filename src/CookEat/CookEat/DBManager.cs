using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CookEat
{
	public class DBManager
	{
		public async Task InsertToRecipesCollection(Recipe i_RecipeToInsert)
		{
			const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("CookEatDB");
			var collection = database.GetCollection<Recipe>("RecipesCollection");
			await collection.InsertOneAsync(i_RecipeToInsert);
		}

		public async Task InsertToUserProfileCollection(UserProfile i_UserProfileToInsert)
		{
			const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("CookEatDB");
			var collection = database.GetCollection<UserProfile>("UserProfileCollection");
			await collection.InsertOneAsync(i_UserProfileToInsert);
		}

		public async Task RemoveRecipeFromUserProfile(string i_UserId, string i_RecipeID)
		{
			/*const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("CookEatDB");
			var collection = database.GetCollection<UserProfile>("UserProfileCollection");
			//UserProfile user = (collection.Find(i_UserId)) as UserProfile;
			var filter = Builders<UserProfile>.Filter.Eq(UserProfile => UserProfile.UserID, i_UserId);
			var results = collection.Find(filter).ToList();
			foreach (UserProfile user in results)
			{
				user.UserRecipes.Remove(i_RecipeID);
			}
			*/
		
			/*const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("CookEatDB");
			var collection = database.GetCollection<UserProfile>("UserProfileCollection");
			var filter = Builders<UserProfile>.Filter.Eq(UserProfile => UserProfile.UserID, i_UserId);
			var results = collection.FindOneAndDelete(filter);
			await collection.InsertOneAsync(results);
			*/

			/*const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("CookEatDB");
			var collection = database.GetCollection<UserProfile>("UserProfileCollection");
			//UserProfile user = (collection.Find(i_UserId)) as UserProfile;
			var filter = Builders<UserProfile>.Filter.Eq(UserProfile => UserProfile.UserID, i_UserId);
			var update = Builders<UserProfile>.Update();
			var results = collection.FindOneAndUpdate(filter,).ToList();
			foreach (UserProfile user in results)
			{
				user.UserRecipes.Remove(i_RecipeID);
			}
			*/
		}
	}
}
