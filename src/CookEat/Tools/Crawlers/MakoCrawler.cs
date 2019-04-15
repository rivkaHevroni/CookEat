using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat.Tools.Crawlers
{
	public class MakoCrawler : Crawller
	{
		private List<string> _recipesCategoriesList = new List<string>
		{
			"https://www.mako.co.il/food-recipes/recipes_column-cakes",
			"https://www.mako.co.il/food-recipes/recipes_column-desserts",
			"https://www.mako.co.il/food-recipes/recipes_column-chicken",
			"https://www.mako.co.il/food-recipes/recipes_column-bake-off-israel-recipes",
			"https://www.mako.co.il/food-recipes/recipes_column-pasta",
			"https://www.mako.co.il/food-recipes/recipes_column-stuffed",
			"https://www.mako.co.il/food-recipes/recipes_column-bread",
			"https://www.mako.co.il/food-recipes/recipes_column-salads",
			"https://www.mako.co.il/food-recipes/recipes_column-vegan-recipes",
			"https://www.mako.co.il/food-recipes/recipes_column-vegetarian-recipes",
			"https://www.mako.co.il/food-recipes/recipes_column-healthy",
			"https://www.mako.co.il/food-recipes/recipes_column-hospitality",
			"https://www.mako.co.il/food-recipes/recipes_column-30-minutes",
			"https://www.mako.co.il/food-recipes/recipes_column-one-pot-meal",
			"https://www.mako.co.il/food-recipes/recipes_column-all-night",
			"https://www.mako.co.il/food-recipes/recipes_column-gluten-free",
			"https://www.mako.co.il/food-recipes/recipes_column-masterchef",
			"https://www.mako.co.il/food-recipes/recipes_column-meat",
			"https://www.mako.co.il/food-recipes/recipes_column-jams",
			"https://www.mako.co.il/food-recipes/recipes_column-sauces",
			"https://www.mako.co.il/food-recipes/recipes_column-fish-seafood",
			"https://www.mako.co.il/food-recipes/recipes_column-holidays",
			"https://www.mako.co.il/food-recipes/recipes_column-diet",
			"https://www.mako.co.il/food-recipes/recipes_column-beverages",
			"https://www.mako.co.il/food-recipes/recipes_column-special",
			"https://www.mako.co.il/food-recipes/recipes_column-soups"
		};

		public MakoCrawler(DBManager dbManager, CancellationToken cancellationToken)
			: base(dbManager, cancellationToken)
		{
		}

		public List<string> RecipesCategoriesList { get => _recipesCategoriesList; set => _recipesCategoriesList = value; }

		public override Task<List<string>> CrawlAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
