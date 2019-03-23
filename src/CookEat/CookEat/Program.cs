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
                id = 12,
                preparationTime = 100,
                link = "https://www.chef-lavan.co.il/מתכונים/שבלולי-שמרים-במילוי-3-שוקולדים",
                numberOfDiners = 3,
                picture = "https://www.chef-lavan.co.il/uploads/images/03c867e7ea65eebd00be9e6b0dddf586.jpg",
                recipeTitle = "יאמי"
            };

            await collection.InsertOneAsync(recipe);
        }
    }
}
