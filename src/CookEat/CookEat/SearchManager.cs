using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace CookEat
{
    public class SearchManager : ISearchManager
    {
        private readonly DBManager DBManager;

        public SearchManager(DBManager dbManager)
        {
            DBManager = dbManager;
        }

		public SearchResponse Search(SearchRequest searchRequest)
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

        private List<Recipe> SearchByQuery(string query)
        {
            //takes the query
            //tokenize
            //search db
            //return results
            return new List<Recipe>();
        }

        private List<Recipe> SearchByImage(byte[] imageBytes)
        {
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

		public List<Recipe> GetRecipesFromIDsRecipesList(List<string> idsRecipesList) // async??
		{
			List<Recipe> result = null;
			//create recipes list from ids Recipes List
			return result;
		}
	}
}
