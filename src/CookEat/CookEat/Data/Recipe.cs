using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
	public class Recipe
	{
		//change properties to private
		public int id { get; set; }
		public int preparationTime { get; set; }
		public string link { get; set; }
		public int numberOfDiners { get; set; }
		public string picture { get; set; }
		public string recipeTitle { get; set; }

	}
}
