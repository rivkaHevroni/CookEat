using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
	public class Recipe
	{
        public int Id { get; set; }
		public int PreparationTime { get; set; }
		public string Link { get; set; }
		public int NumberOfDiners { get; set; }
		public string Picture { get; set; }
		public string RecipeTitle { get; set; }
	}
}
