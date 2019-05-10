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
                try
                {
                    results = SearchByImage(searchRequest.ImageBytes);
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

        public List<Recipe> GetRecipesFromIDsRecipesList(List<string> idsRecipesList) // async??
        {
            List<Recipe> recipes = null;
            IFindFluent<Recipe, Recipe> recipeToConvert = _dbManager.RecipesCollection.
                Find(recipe => idsRecipesList.Contains(recipe.Id));

            recipes = recipeToConvert.ToList();

            return recipes;
        }

        private List<Recipe> SearchByQuery(string query)
        {
            List<string> tokenaizedSearchValues = TokanizationHelper.Tokenaize(query);
            IFindFluent<Recipe, Recipe> releventCollection =
                    _dbManager.
                        RecipesCollection.
                        Find(recipe => tokenaizedSearchValues.Any(value => recipe.ValuesToSearch.Contains(value)));
            List<Recipe> recipesBeforSort = releventCollection.ToList();

            List<RecipeWithRank> matchToQuery = new List<RecipeWithRank>();

            foreach (Recipe recipe in recipesBeforSort)
            {
                RecipeWithRank recipeWithRank = new RecipeWithRank(recipe, CheckNumberOfMatchValues(recipe, tokenaizedSearchValues));

                matchToQuery.Add(recipeWithRank);
            }

            IOrderedEnumerable<RecipeWithRank> matchToQueryAfterSort = matchToQuery.OrderByDescending(recipeWithRank => recipeWithRank.Rank);
            List<Recipe> recipesAfterSort = new List<Recipe>();

            foreach (RecipeWithRank recipeWithRank in matchToQueryAfterSort)
            {
                recipesAfterSort.Add(recipeWithRank.Recipe);
            }

            return recipesAfterSort;
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

        private List<Recipe> SearchByImage(Byte[] imageBytes)
        {
            VisionAndTranslateHelper visionAndTranslateHelper = new VisionAndTranslateHelper();
            string res = visionAndTranslateHelper.CreateQueryFromImage(imageBytes);
            List<Recipe> recipes = SearchByQuery(res);
            return recipes;
        }

        private List<Recipe> SearchByIngredients(List<string> ingredientNames)
        {
            List<Recipe> recipesAfterSort = new List<Recipe>();
            List<string> tokenaizedSearchValues = new List<string>();

            foreach (string ingredientName in ingredientNames)
            {
                tokenaizedSearchValues.Add(TokanizationHelper.TokenaizeForOneValue(ingredientName));
            }

            string queryToSearchByIngredients = null;

            foreach (string ingredient in tokenaizedSearchValues)
            {
                queryToSearchByIngredients += ingredient;
                queryToSearchByIngredients += " ";
            }

            recipesAfterSort = SearchByQuery(queryToSearchByIngredients); 

            return recipesAfterSort;
        }
    }
}