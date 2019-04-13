using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CookEat
{
    public class ShefLavanCrawler : Crawller
    {
        public ShefLavanCrawler(DBManager dbManager, CancellationToken cancellationToken)
            : base(dbManager, cancellationToken)
        {
        }

        public override Task<List<string>> CrawlAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}