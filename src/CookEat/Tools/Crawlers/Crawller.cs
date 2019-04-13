using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using MongoDB.Driver;

namespace CookEat
{
    public abstract class Crawller
    {
        protected CrawllerProfile CrawlerProfile;

        protected Crawller(DBManager dbManager,CancellationToken cancellationToken)
        {
            CrawlerProfile =
                dbManager.
                CrawlingManagerProfileCollection.
                Find(profile => profile.Id == GetType().Name).
                Single();

            TaskExtension.RunPeriodicly(
                async () =>
                    await dbManager.
                        CrawlingManagerProfileCollection.
                        FindOneAndReplaceAsync(profile => profile.Id == GetType().Name, CrawlerProfile),
                15.Minutes(),
                cancellationToken);
        }

        public abstract Task<List<string>> CrawlAsync();
    }
}