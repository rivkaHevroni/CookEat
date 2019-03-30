using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CookEat
{
	public class UserProfile
	{

        [BsonId]
		public string Id { get; set; }
		public List<string> UserRecipes { get; set; }
	
	}
}
