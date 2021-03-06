﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CookEat
{
    public class CrawllerProfile
    {
        [BsonId]
        public string Id { get; set; }
        public List<string> SavedUrls { get; set; }
    }
}
