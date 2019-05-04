using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
	public class RecipeWithRank
	{
		public Recipe Recipe { get; set; }
		public int Rank { get; set; }

		public RecipeWithRank(Recipe recipe, int rank)
		{
			Recipe = recipe;
			Rank = rank;
		}
	}
}
