using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace CookEat
{
    public class Recipe
    {
        [BsonId]
        public string Id { get; set; }
        public string PreparationTime { get; set; }
        public string Link { get; set; }
        public int NumberOfDishes { get; set; }
        public string Picture { get; set; }
        public string RecipeTitle { get; set; }
        public List<Ingredient> IngredientsList { get; set; }
		public List<string> ValuesToSearch { get; set; }
		public List<string> NormalaizedIngredients { get; set; }

        public bool CheckThatAtLeastOneValueToSearchExist(List<string> tokenaizedSearchValues)
        {
            foreach (var tokenaizedSearchValue in tokenaizedSearchValues)
            {
                if (ValuesToSearch.Contains(tokenaizedSearchValue))
                {
                    return true;
                }
            }

            return false;
        }
    }


}
