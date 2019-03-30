using System.Collections.Generic;

namespace CookEat
{
    public class SearchRequest
    {
        public string SearchQuery { get; }
        public byte[] ImageBytes { get; }
        public List<string> IngrediantNames { get; }
    }
}