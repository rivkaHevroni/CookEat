using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CookEat
{
    [RoutePrefix("Api/Search")]
    public sealed class SearchManager : ApiController
    {
        private readonly DBManager _dbManager;
        private readonly GoogleApiHelper _googleApiHelper;
        private readonly CancellationToken _cancellationToken;

        public SearchManager(DBManager dbManager)
        {
            _dbManager = dbManager;
            _googleApiHelper = new GoogleApiHelper();
        }

        [Route("")]
        [HttpPost]
        public async Task<SearchResponse> Search([FromBody] SearchRequest searchRequest)
        {
            List<Recipe> results;
            if (searchRequest.SearchQuery != null)
            {
                results = SearchByQuery(searchRequest.SearchQuery);
            }
            else if (searchRequest.ImageBytes != null)
            {
                try
                {
                    results = await SearchByImageAsync(searchRequest.ImageBytes);
                }
                catch (Exception e)
                {
                    results = null;
                }
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

        public List<Recipe> SearchRecipesByIds(List<string> idsRecipesList)
        {
            return _dbManager.
                RecipesCollection.
                Find(recipe => idsRecipesList.Contains(recipe.Id)).
                ToList();
        }

        private List<Recipe> SearchByQuery(string query)
        {
            var tokenaizedSearchValues = TokanizationHelper.Tokenaize(query);

            var recipesBeforeSort =
                _dbManager.
                    RecipesCollection.
                    Find(
                        recipe =>
                            tokenaizedSearchValues.
                                Any(value => recipe.ValuesToSearch.Contains(value))).
                    ToList();

            var recipesWithRank = new List<RecipeWithRank>();

            foreach (var recipe in recipesBeforeSort)
            {
                var recipeWithRank = new RecipeWithRank(recipe, CheckNumberOfMatchValues(recipe, tokenaizedSearchValues));

                recipesWithRank.Add(recipeWithRank);
            }

            var matchToQueryAfterSort = recipesWithRank.OrderByDescending(recipeWithRank => recipeWithRank.Rank);
            var recipesAfterSort = new List<Recipe>();

            foreach (var recipeWithRank in matchToQueryAfterSort)
            {
                recipesAfterSort.Add(recipeWithRank.Recipe);
            }

            return recipesAfterSort;
        }

        private int CheckNumberOfMatchValues(Recipe recipe, List<string> tokenaizedSearchValues)
        {
            var countMatchWords = 0;

            foreach (var tokenaizedSearchValue in tokenaizedSearchValues)
            {
                if (recipe.ValuesToSearch.Contains(tokenaizedSearchValue))
                {
                    countMatchWords++;
                }
            }

            return countMatchWords;
        }

        private async Task<List<Recipe>> SearchByImageAsync(byte[] imageBytes)
        {
            var query = await _googleApiHelper.GetQueryFromImage(imageBytes);
            return SearchByQuery(query);
        }

        private List<Recipe> SearchByIngredients(List<string> ingredientNames)
        {
            var tokenaizedSearchValues = new List<string>();

            foreach (var ingredientName in ingredientNames)
            {
                tokenaizedSearchValues.Add(TokanizationHelper.TokenaizeForOneValue(ingredientName));
            }

            string queryToSearchByIngredients = null;

            foreach (var ingredient in tokenaizedSearchValues)
            {
                queryToSearchByIngredients += ingredient;
                queryToSearchByIngredients += " ";
            }

            return SearchByQuery(queryToSearchByIngredients);
        }
    }
}