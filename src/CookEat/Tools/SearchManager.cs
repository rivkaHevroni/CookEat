using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;

namespace CookEat
{
    [RoutePrefix("search")]
    public class SearchManager : ApiController
    {
        private readonly DBManager _dbManager;
        private readonly VisionHelper _visionHelper;

        public SearchManager(DBManager dbManager)
        {
            _dbManager = dbManager;
            _visionHelper = new VisionHelper();
        }

        [Route("")]
        [HttpPost]
        public SearchResponse Search([FromBody] SearchRequest searchRequest)
        {
            List<Recipe> results;
            if (searchRequest.SearchQuery != null)
            {
                results = SearchByQuery(searchRequest.SearchQuery);
            }
            else if (searchRequest.ImageBytes != null)
            {
                results = SearchByImage(searchRequest.ImageBytes);
            }
            else
            {
                results = SearchByIngredients(searchRequest.IngrediantNames);
            }

            return new SearchResponse
            {
                Results = results
            };
        }

        public List<Recipe> GetRecipesFromIDsRecipesList(List<string> idsRecipesList) // async??
        {
            List<Recipe> result = null;
            //create recipes list from ids Recipes List
            return result;
        }

        private List<Recipe> SearchByQuery(string query)
        {
            //takes the query
            //tokenize
            //search db
            //return results
            return new List<Recipe>();
        }

        private async Task<List<Recipe>> SearchByImage(byte[] imageBytes)
        {

            var englishSearchLabels = await _visionHelper.GetImageBestSearchLabelsAsync(imageBytes);
            // the frontend create from image bytes!!!!!!!!!
            //sent the byts to vision helper and get heberw query
            string result = "a"; //instead of translation result
            return SearchByQuery(result);
        }

        private List<Recipe> SearchByIngredients(List<string> ingrediantNames)
        {
            //Query database for recipes with the ingrediants
            //sorts the recipes by amount of relevant ingrediants
            //return sorted list
            return new List<Recipe>();
        }
    }
}