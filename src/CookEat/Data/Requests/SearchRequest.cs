using System.Collections.Generic;

namespace CookEat
{
    public class SearchRequest
    {
        public string SearchQuery { get; set; }
        public string ImageBytes { get; set; }
        public List<string> IngrediantNames { get; set; }
    }
}