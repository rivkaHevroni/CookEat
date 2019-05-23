using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
	public class SaveRecipeRequest
	{
		public string UserId { get; set; }
		public string RecipeId { get; set; }
	}
}
