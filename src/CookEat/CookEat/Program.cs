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
			user.Id = "231587367162"; //access token from googelAPI
			user.UserRecipes = new List<string>();
			user.UserRecipes.Add("5");
			user.UserRecipes.Add("6");
			user.UserRecipes.Add("7");
            DBManager db= new DBManager();
            UserProfileManager upm = new UserProfileManager(db);
            await upm.InsertToUserProfileCollection(user);
            await upm.RemoveRecipeFromUserProfile(user.Id, "7");




        }
}
}
