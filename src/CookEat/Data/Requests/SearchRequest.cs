using System.Collections.Generic;

namespace CookEat
{
    public class SearchRequest
    {
        public string SearchQuery { get; set; }
        public byte[] ImageBytes { get; set; }
        public List<string> IngrediantNames { get; set; }
    }
}