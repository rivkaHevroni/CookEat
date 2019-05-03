using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using MoreLinq;
using MoreLinq.Extensions;

namespace CookEat
{
    [RoutePrefix("search")]
    public class SearchManager : ApiController
    {
        private readonly DBManager _dbManager;
        private readonly CancellationToken _cancellationToken;

        public SearchManager(DBManager dbManager)
        {
            _dbManager = dbManager;
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

        public List<Recipe> SearchByQuery(string query)//change to private
        {
            List<string> tokenaizedSearchValues = TokanizationHelper.Tokenaize(query);
            IFindFluent<Recipe, Recipe> releventCollection =
                    _dbManager.
                        RecipesCollection.
                        Find(recipe => tokenaizedSearchValues.Any(value => recipe.ValuesToSearch.Contains(value)));
            List<Recipe> recipesBeforSort = releventCollection.ToList();

            /*Dictionary<int, string> matchToQuery = new Dictionary<Recipe, int>();
            foreach (Recipe recipe in recipesBeforSort)
            {
                matchToQuery.Add(CheckNumberOfMatchValues(recipe, tokenaizedSearchValues), recipe.Id);
            }

            matchToQuery.OrderByDescending(matchValues => matchValues.Key);
            var matchToQueryIdList = matchToQuery.Values.ToList();
            List<Recipe> recipesAfterSort = new List<Recipe>();
            foreach (string id in matchToQueryIdList)
            {
                Recipe recipeAfterSort = recipesBeforSort.Find(recipe => recipe.Id == id);
                recipesAfterSort.Add(recipeAfterSort);
            }

            return recipesAfterSort;*/

            return recipesBeforSort;
        }


        private int CheckNumberOfMatchValues(Recipe recipe, List<string> tokenaizedSearchValues)
        {
            int countMatchWords = 0;

            foreach (string tokenaizedSearchValue in tokenaizedSearchValues)
            {
                if (recipe.ValuesToSearch.Contains(tokenaizedSearchValue))
                {
                    countMatchWords++;
                }
            }

            return countMatchWords;
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
    }
}