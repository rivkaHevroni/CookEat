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
        private const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";
        
        public IMongoCollection<Recipe> RecipesCollection { get; set; }
        public IMongoCollection<UserProfile> UserProfileCollection { get; set; }
        public IMongoCollection<CrawlingManagerProfile> CrawlingManagerProfileCollection { get; set; }

        public DBManager()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("CookEatDB");
            RecipesCollection = database.GetCollection<Recipe>(nameof(RecipesCollection));
            UserProfileCollection = database.GetCollection<UserProfile>(nameof(UserProfileCollection));
            CrawlingManagerProfileCollection = database.GetCollection<CrawlingManagerProfile>(nameof(CrawlingManagerProfileCollection));
        }
    }
}
