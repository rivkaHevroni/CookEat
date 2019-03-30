using System.Collections.Generic;

namespace CookEat.Requests
{
    public class SearchRequest
    {
        public string SearchQuery { get; }
        public byte[] ImageBytes { get; }
        public List<string> IngrediantNames { get; }
    }

    public class SearchResponse
    {
        public List<Recipe> Results;
    }
}