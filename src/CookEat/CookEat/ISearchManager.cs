using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CookEat
{
	public interface ISearchManager
	{
		SearchResponse Search(SearchRequest searchRequest);
	}
}
