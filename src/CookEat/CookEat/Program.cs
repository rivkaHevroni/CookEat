using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CookEat
{
	public class Program
	{
		public static async Task Main(string[] Args)
        {
            await DoSomethingAsync();
        }

        static async Task DoSomethingAsync()
        {
            const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("CookEatDB");
            var collection = database.GetCollection<Recipe>("RecipesCollection");
            var recipe = new Recipe
            {
                Id=2,
                PreparationTime = 200,
                Link = "https://www.chef-lavan.co.il/מתכונים/שבלולי-שמרים-במילוי-3-שוקולדים",
                NumberOfDiners = 5,
                Picture = "https://www.chef-lavan.cso.il/uploads/images/03c867e7ea65eebd00be9e6b0dddf586.jpg",
                RecipeTitle = "3יאמי2"
            };
            await collection.InsertOneAsync(recipe);
        }
    }
}
