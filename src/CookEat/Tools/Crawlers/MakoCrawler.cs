using HtmlAgilityPack;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat
{
    public sealed class MakoCrawler : Crawller
    {
        private const string _baseUrl = "https://www.mako.co.il/food-recipes/recipes_column";

        private static readonly List<string> _recipesCategoriesList = new List<string>
        {
            "cakes",
            "desserts",
            "chicken",
            "bake-off-israel-recipes",
            "pasta",
            "stuffed",
            "bread",
            "salads",
            "vegan-recipes",
            "vegetarian-recipes",
            "healthy",
            "hospitality",
            "30-minutes",
            "one-pot-meal",
            "all-night",
            "gluten-free",
            "masterchef",
            "meat",
            "jams",
            "sauces",
            "fish-seafood",
            "holidays",
            "diet",
            "beverages",
            "special",
            "soups"
        };

        public MakoCrawler(DBManager dbManager, CancellationToken cancellationToken)
            : base(dbManager, cancellationToken)
        {
        }

        public override async Task<List<string>> CrawlAsync()
        {
            Console.WriteLine($"{nameof(MakoCrawler)} {nameof(CrawlAsync)} started");

            var urls = new List<string>();
            foreach (var recipeCategory in _recipesCategoriesList)
            {
                var web = new HtmlWeb();

                var htmlDoc = await web.LoadWithRetryAsync($"{_baseUrl}-{recipeCategory}");
                var lastPageString =
                    htmlDoc.
                        DocumentNode.
                        SelectSingleNode("//div/max")
                        .InnerText;
                var lastPage = int.Parse(lastPageString);

                for (var counterPages = 1; counterPages <= lastPage; counterPages++)
                {
                    var singleUrl = $"{_baseUrl}-{recipeCategory}?page={counterPages}";
                    var htmlInnerDoc = web.Load(singleUrl);

                    while (htmlInnerDoc.DocumentNode.InnerHtml.Contains("Apache Tomcat/6.0.18 - Error report"))
                    {
                        htmlInnerDoc = await web.LoadWithRetryAsync(singleUrl);
                    }

                    var nodes = htmlInnerDoc.
                        DocumentNode.
                        SelectNodes("//li[@class='hover']/div[@class='line-clamp']/h5/a");

                    if (nodes == null)
                    {
                        Console.WriteLine($"WARNING: nodes is null [{nameof(singleUrl)}={singleUrl}]");
                        continue;
                    }

                    nodes.
                        ForEach(htmlNode => urls.Add("https://www.mako.co.il" + htmlNode.GetAttributeValue("href", "")));

                    Console.WriteLine($"{nameof(MakoCrawler)} {nameof(CrawlAsync)} Crawled {urls.Count} {nameof(urls)}");
                }
            }

            var nonExistingUrls =
                urls.
                    Where(url => !CrawlerProfile.SavedUrls.Contains(url)).
                    Distinct().
                    Take(400).
                    ToList();

            CrawlerProfile.SavedUrls.AddRange(nonExistingUrls);

            Console.WriteLine($"{nameof(MakoCrawler)} {nameof(CrawlAsync)} finished [{nameof(nonExistingUrls.Count)}={nonExistingUrls.Count}]");

            return nonExistingUrls;
        }
    }
}
