using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MoreLinq;

namespace CookEat
{
    public sealed class ShefLavanCrawler : Crawller
    {
        private const string _baseUrl = "https://www.chef-lavan.co.il/%D7%9E%D7%AA%D7%9B%D7%95%D7%A0%D7%99%D7%9D?recipes_per_page=96";

        public ShefLavanCrawler(DBManager dbManager, CancellationToken cancellationToken)
            : base(dbManager, cancellationToken)
        {
        }

        public override async Task<List<string>> CrawlAsync()
        {
            Console.WriteLine($"{nameof(ShefLavanCrawler)} {nameof(CrawlAsync)} started");

            var urls = new List<string>();
            var web = new HtmlWeb();
            var htmlDoc = await web.LoadWithRetryAsync(_baseUrl);

            var lastPageString = htmlDoc.DocumentNode.SelectSingleNode("//a[@title='עבור לעמוד האחרון']")
                .GetAttributeValue("data-action-value", "");
            var lastPage = int.Parse(lastPageString);

            for (var counterPages = 1; counterPages <= lastPage; counterPages++)
            {
                var singleUrl = $"{_baseUrl}&page={counterPages}";
                var htmlInnerDoc = await web.LoadWithRetryAsync(singleUrl);
                htmlInnerDoc.
                    DocumentNode.
                    SelectNodes("//div[@class='list-box-content-wrapper']/a[@class='card-link']").
                    ForEach(htmlNode => urls.Add(htmlNode.GetAttributeValue("href", "")));

                Console.WriteLine($"{nameof(ShefLavanCrawler)} {nameof(CrawlAsync)} Crawled {urls.Count} {nameof(urls)}");
            }

            var nonExistingUrls =
                urls.
                    Where(url => !CrawlerProfile.SavedUrls.Contains(url)).
                    Distinct().
                    ToList();

            CrawlerProfile.
                SavedUrls.
                AddRange(nonExistingUrls);

            Console.WriteLine($"{nameof(ShefLavanCrawler)} {nameof(CrawlAsync)} finished [{nameof(nonExistingUrls.Count)}={nonExistingUrls.Count}]");

            return nonExistingUrls;
        }
    }
}