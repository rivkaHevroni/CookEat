using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string UserID { get; set; }
		public List<string> UserRecipes { get; set; }
	
	}
}
