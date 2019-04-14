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
		public MakoCrawler(DBManager dbManager, CancellationToken cancellationToken)
			: base(dbManager, cancellationToken)
		{
		}

		public override Task<List<string>> CrawlAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
