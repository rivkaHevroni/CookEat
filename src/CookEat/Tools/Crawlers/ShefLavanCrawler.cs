using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CookEat
{
    public class ShefLavanCrawler : Crawller
    {
        public ShefLavanCrawler(DBManager dbManager, CancellationToken cancellationToken)
            : base(dbManager, cancellationToken)
        {
        }

        public override async Task<List<string>> CrawlAsync()
        {
			//CrawlerProfile.SavedUrls.Add("qwklrjqwl");
			List<string> urls = new List<string>();
			var web = new HtmlWeb();
			var htmlDoc = await web.LoadFromWebAsync("https://www.chef-lavan.co.il/%D7%9E%D7%AA%D7%9B%D7%95%D7%A0%D7%99%D7%9D/");
			var lastPageString = htmlDoc.DocumentNode.SelectSingleNode("//a[@title='עבור לעמוד האחרון']").GetAttributeValue("data-action-value", "");
			int lastPage = int.Parse(lastPageString);
			for (int counterPages = 1; counterPages <= lastPage; counterPages++)
			{
				string namOfPAge = counterPages.ToString();
				string singelUrl = "https://www.chef-lavan.co.il/%D7%9E%D7%AA%D7%9B%D7%95%D7%A0%D7%99%D7%9D?recipes_per_page=24&amp;page=" + namOfPAge;
				var htmlInnerDoc = await web.LoadFromWebAsync(singelUrl);
				var urlsFromSinglePage = htmlDoc.DocumentNode.SelectNodes("//div[@class='list-box-content-wrapper']/a[@class='card-link']");
				for (int currRecipe = 0; currRecipe < urlsFromSinglePage.Count; currRecipe++)
				{
					urls.Add(urlsFromSinglePage[currRecipe].GetAttributeValue("href", ""));
				}
			}
			throw new System.NotImplementedException();
        }
    }
}