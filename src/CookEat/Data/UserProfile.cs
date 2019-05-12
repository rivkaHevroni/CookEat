using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CookEat
{
    public sealed class UserProfile
    {
        [BsonId]
        public string Id { get; set; }
        public string Password { get; set; }
        public List<string> UserRecipes { get; set; }
    }
}
