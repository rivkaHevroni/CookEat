using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtilities;
using HtmlAgilityPack;
using MoreLinq;

namespace CookEat.Tools.Crawlers
{
	public class MakoCrawler : Crawller
	{
		private const string baseUrl = "https://www.chef-lavan.co.il/%D7%9E%D7%AA%D7%9B%D7%95%D7%A0%D7%99%D7%9D";

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

		public override async Task<List<string>> CrawlAsync()
		{
			List<string> urls = new List<string>();
			for (int currRecipeCategorie = 0; currRecipeCategorie < _recipesCategoriesList.Count; currRecipeCategorie++)
			{
				var web = new HtmlWeb();
				var htmlDoc =
					await web.LoadFromWebAsync(_recipesCategoriesList[currRecipeCategorie]);
				var lastPageString = htmlDoc.DocumentNode.SelectSingleNode("/div/max").InnerText;
				int lastPage = int.Parse(lastPageString);

				for (int counterPages = 1; counterPages <= lastPage; counterPages++)
				{
					string singelUrl = $"{_recipesCategoriesList[currRecipeCategorie]}?page={counterPages}";
					var htmlInnerDoc = await web.LoadFromWebAsync(singelUrl);
					htmlDoc.
						DocumentNode.
						SelectNodes("//div[@class='line-clamp']/h5/a").
						ForEach(htmlNode => urls.Add("www.mako.co.il" + htmlNode.GetAttributeValue("href", "")));
				}
			}

			CrawlerProfile.SavedUrls = urls;

			return urls;
		}
	}
}
