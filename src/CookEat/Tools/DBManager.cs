using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat
{
    public class DBManager
    {
        private const string connectionString = "mongodb+srv://CookEatUser:lianrivkasapir123@cluster0-dihhr.mongodb.net/test?retryWrites=true";

        private readonly IMongoDatabase _database;

        public IMongoCollection<Recipe> RecipesCollection { get; private set; }
        public IMongoCollection<UserProfile> UserProfileCollection { get; private set; }
        public IMongoCollection<CrawllerProfile> CrawlingManagerProfileCollection { get; private set; }

        public DBManager()
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("CookEatDB");
            RecipesCollection = _database.GetCollection<Recipe>(nameof(RecipesCollection));
            UserProfileCollection = _database.GetCollection<UserProfile>(nameof(UserProfileCollection));
            CrawlingManagerProfileCollection = _database.GetCollection<CrawllerProfile>(nameof(CrawlingManagerProfileCollection));
        }

        public async Task ClearAsync(CancellationToken cancellationToken)
        {
            await _database.DropCollectionAsync(nameof(RecipesCollection), cancellationToken);
            await _database.DropCollectionAsync(nameof(UserProfileCollection), cancellationToken);
            await _database.DropCollectionAsync(nameof(CrawlingManagerProfileCollection), cancellationToken);

            await _database.CreateCollectionAsync(
                nameof(RecipesCollection),
                null,
                cancellationToken);
            await _database.CreateCollectionAsync(
                nameof(UserProfileCollection),
                null,
                cancellationToken);
            await _database.CreateCollectionAsync(
                nameof(CrawlingManagerProfileCollection),
                null,
                cancellationToken);

            RecipesCollection = _database.GetCollection<Recipe>(nameof(RecipesCollection));
            UserProfileCollection = _database.GetCollection<UserProfile>(nameof(UserProfileCollection));
            CrawlingManagerProfileCollection = _database.GetCollection<CrawllerProfile>(nameof(CrawlingManagerProfileCollection));

            await CrawlingManagerProfileCollection.InsertManyAsync(
                new[]
                {
                    new CrawllerProfile
                    {
                        Id = nameof(ShefLavanCrawler),
                        SavedUrls = new List<string>()
                    },
                    new CrawllerProfile
                    {
                        Id = nameof(MakoCrawler),
                        SavedUrls = new List<string>()
                    }
                },
                null,
                cancellationToken);
        }
    }
}