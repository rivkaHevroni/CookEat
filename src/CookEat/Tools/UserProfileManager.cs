﻿using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web.Http;

namespace CookEat
{
    [RoutePrefix("Api/UserProfile")]
    public sealed class UserProfileManager : ApiController
    {
        private readonly DBManager _dbManager;
        private readonly SearchManager _searchManager;

        public UserProfileManager(DBManager dbManager,GoogleApiHelper googleApiHelper)
        {
            _dbManager = dbManager;
            _searchManager = new SearchManager(_dbManager,googleApiHelper);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<AuthenticationResponse> Login([FromBody] AuthenticationRequest request)
        {
            var userProfile = await TryGetUserProfileAsync(request.UserName);

            if (userProfile == null)
            {
                return new AuthenticationResponse
                {
                    AuthenticationResult = AuthenticationResult.UserDoesNotExist
                };
            }

            return new AuthenticationResponse
            {
                AuthenticationResult =
                    userProfile.Password == request.Password
                        ? AuthenticationResult.Success
                        : AuthenticationResult.IncorrectPassword
            };
        }

        [HttpPost]
        [Route("Register")]
        public async Task<AuthenticationResponse> RegisterAsync([FromBody] AuthenticationRequest request)
        {
            if (await TryGetUserProfileAsync(request.UserName) != null)
            {
                return new AuthenticationResponse
                {
                    AuthenticationResult = AuthenticationResult.UserAlreadyExists
                };
            }

            await _dbManager.UserProfileCollection.InsertOneAsync(
                new UserProfile
                {
                    Id = request.UserName,
                    Password = request.Password,
                    UserRecipes = new List<string>()
                });

            return new AuthenticationResponse
            {
                AuthenticationResult = AuthenticationResult.Success
            };
        }

        [HttpPost]
        [Route("UserRecipes")]
        public async Task<GetUserSavedRecipesResponse> GetUserRecipesAsync([FromBody] GetUserSavedRecipesRequest request)
        {
            var recipeIds = (await TryGetUserProfileAsync(request.UserId)).UserRecipes;
            return new GetUserSavedRecipesResponse
            {
                Recipes = _searchManager.SearchRecipesByIds(recipeIds)
            };
        }

        private async Task<UserProfile> TryGetUserProfileAsync(string userId)
        {
            return (await _dbManager.
                UserProfileCollection.
                FindAsync(profile => profile.Id == userId)).
                SingleOrDefault();
        }

        [HttpPost]
        [Route("SaveRecipe")]
        public async Task SaveRecipeInUserProfileAsync([FromBody] SaveRecipeRequest saveRecipeRequest) 
        {
            var userProfile = await TryGetUserProfileAsync(saveRecipeRequest.UserId);
            userProfile.UserRecipes.Add(saveRecipeRequest.RecipeId);
            await _dbManager.
                UserProfileCollection.
                FindOneAndReplaceAsync(profile => profile.Id == saveRecipeRequest.UserId, userProfile);
        }

        [HttpPost]
        [Route("RemoveRecipe")]
        public async Task RemoveRecipeFromUserProfileAsync([FromBody] RemoveRecipeRequest removeRecipeRequest)
        {
            var userProfile = await TryGetUserProfileAsync(removeRecipeRequest.UserId);
            userProfile.UserRecipes.Remove(removeRecipeRequest.RecipeId);
            await _dbManager.
                UserProfileCollection.
                FindOneAndReplaceAsync(profile => profile.Id == removeRecipeRequest.UserId, userProfile);
        }
    }
}