using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookEat.Data;

namespace CookEat
{
    public static class Constants
    {
        public static Dictionary<string, double> IngredientNameToAmount { get; } = new Dictionary<string, double>()
        {
            { "1/2", 0.5},
            { "1/3", 0.333},
            { "1/4", 0.25},
            { "2/3", 0.667},
            { "חצי", 0.5},
            { "שליש", 0.333},
            { "רבע", 0.25},
            { "מיכל", 1},
        };

		public static Dictionary<string, string> HeberwUnnecessaryToEmptyString { get; } = new Dictionary<string, string>()
		{
			{ "עמ", ""},
			{ "על", ""},
			{ "או", ""},
			{ "גמ", ""},
			{ "הרבה", ""},
			{ "מעט", ""},
			{ "קצת", ""},
			{ "גדול", ""},
			{ "קטן", ""},
			{ "בינוני", ""},
			{ "כוס", ""},
			{ "כוסות", ""},
			{ "כפית", ""},
			{ "כפיות", ""},
			{ "כף", ""},
			{ "כפות", ""},
			{ "אחוז", ""},
			{ "%", ""},
			{ "מ#", ""},
			{ ":", ""},
			{ "/", ""},
			{ "&", ""},
			{ ";", ""},
			{ ",", ""},
			{ ".", ""},
			{ "(", ""},
			{ ")", ""},
			{ "שפ", ""},
			{ "הלבינ", ""},
			{ "מתכונ", ""}
		};
	}
}
