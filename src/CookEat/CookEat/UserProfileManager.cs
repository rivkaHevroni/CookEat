using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CookEat
{
    public class UserProfileManager
    {

        public DBManager DataBaseManager;

        public UserProfileManager(DBManager dataBaseManager)
        {
            DataBaseManager = dataBaseManager;
        }

        public async Task InsertToRecipesCollection(Recipe i_RecipeToInsert)// remove
        {
            var collection = DataBaseManager.RecipesCollection;
            await collection.InsertOneAsync(i_RecipeToInsert);
        }

        public async Task InsertToUserProfileCollection(UserProfile i_UserProfileToInsert)
        {
            var collection = DataBaseManager.UserProfileCollection;
            await collection.InsertOneAsync(i_UserProfileToInsert);
        }

        public async Task InsertToCrawlingManagerProfileCollection(CrawlingManagerProfile i_CrawlingManagerProfile)// remove
        {
            var collection = DataBaseManager.CrawlingManagerProfileCollection;
            await collection.InsertOneAsync(i_CrawlingManagerProfile);
        }

        public async Task RemoveRecipeFromUserProfile(string i_UserId, string i_RecipeID)
        {
            var collection = DataBaseManager.UserProfileCollection;
            var filter = Builders<UserProfile>.Filter.Eq(UserProfile => UserProfile.Id, i_UserId);
            var userProfile = (await collection.FindAsync(filter)).Single();
            userProfile.UserRecipes.Remove(i_RecipeID);
            await collection.ReplaceOneAsync(filter, userProfile);
        }

		public RecipeResponse SearchRecipes(RecipeRequest searchRecipes)
		{
			RecipeResponse result = null;
			//get IdUser from RecipeRequest
			// get userProfile from DB manager
			//sent idRecipeList from userprofile to searchManager
			//creat RecipeResponse

			return result;
		}

	}
}

