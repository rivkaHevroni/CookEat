using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MoreLinq;

namespace CookEat
{
    public class ShefLavanCrawler : Crawller
    {
		private const string baseUrl = "https://www.chef-lavan.co.il/%D7%9E%D7%AA%D7%9B%D7%95%D7%A0%D7%99%D7%9D";

		public ShefLavanCrawler(DBManager dbManager, CancellationToken cancellationToken)
            : base(dbManager, cancellationToken)
        {
        }

        public override async Task<List<string>> CrawlAsync()
        {
			List<string> urls = new List<string>();
			var web = new HtmlWeb();
			var htmlDoc =
				await web.LoadFromWebAsync(baseUrl);
			var lastPageString = htmlDoc.DocumentNode.SelectSingleNode("//a[@title='עבור לעמוד האחרון']")
				.GetAttributeValue("data-action-value", "");
			int lastPage = int.Parse(lastPageString);

			for (int counterPages = 1; counterPages <= lastPage; counterPages++)
			{
				string singelUrl = $"{baseUrl}?recipes_per_page=24&amp;page={counterPages}";
				var htmlInnerDoc = await web.LoadFromWebAsync(singelUrl);
				htmlDoc.
					DocumentNode.
					SelectNodes("//div[@class='list-box-content-wrapper']/a[@class='card-link']").
					ForEach(htmlNode => urls.Add(htmlNode.GetAttributeValue("href", "")));
			}

			return urls;
		}
    }
}