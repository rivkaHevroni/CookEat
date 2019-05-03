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

        public async Task InsertToRecipesCollection(Recipe iRecipeToInsert)// remove
        {
            var collection = DataBaseManager.RecipesCollection;
            await collection.InsertOneAsync(iRecipeToInsert);
        }

        public async Task InsertToUserProfileCollection(UserProfile i_UserProfileToInsert)
        {
            var collection = DataBaseManager.UserProfileCollection;
            await collection.InsertOneAsync(i_UserProfileToInsert);
        }

        public async Task RemoveRecipeFromUserProfile(string i_UserId, string i_RecipeID)
        {
            var collection = DataBaseManager.UserProfileCollection;
            var filter = Builders<UserProfile>.Filter.Eq(UserProfile => UserProfile.Id, i_UserId);
            var userProfile = (await collection.FindAsync(filter)).Single();
            userProfile.UserRecipes.Remove(i_RecipeID);
            await collection.ReplaceOneAsync(filter, userProfile);
        }

		public GetUserSavedRecipesResponse SearchRecipes(GetUserSavedRecipesRequest searchGetUserSavedRecipeses)
		{
			GetUserSavedRecipesResponse result = null;
			//get IdUser from GetUserSavedRecipesRequest
			// get userProfile from DB manager
			//sent idRecipeList from userprofile to searchManager
			//creat GetUserSavedRecipesResponse

			return result;
		}

	}
}

