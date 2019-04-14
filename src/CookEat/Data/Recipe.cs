using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
    public class Recipe
    {
        public uint Id { get; set; }
        public string PreparationTime { get; set; }
        public string Link { get; set; }
        public int NumberOfDishes { get; set; }
        public string Picture { get; set; }
        public string RecipeTitle { get; set; }
        public List<Ingredient> IngredientsList { get; set; }
		public List<string> ValuesToSearch { get; set; }
		public List<string> NormalaizedIngredients { get; set; }
	}
}
