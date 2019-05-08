using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookEat
{
    public static class Constants
    {
        public static Dictionary<string, double> IngredientNameToAmount { get; } = new Dictionary<string, double>()
        {
            { "1/2", 0.5},
            { "1/3", (double)1/3},
            { "1/4", 0.25},
            { "2/3", (double)2/3},
            { "חצי", 0.5},
            { "שליש", (double)1/3},
            { "רבע", 0.25},
            { "מיכל", 1},
        };

        public static List<string> HebrewUnnecessaryToEmptyString { get; } = new List<string>
        {
            "עמ",
            "על",
            "או",
            "גמ",
            "הרבה",
            "מעט",
            "קצת",
            "גדול",
            "קטן",
            "בינוני",
            "כוס",
            "כוסות",
            "כפית",
            "כפיות",
            "כף",
            "כפות",
            "אחוז",
            "%",
            "מ#",
            ":",
            "/",
            "&",
            ";",
            ",",
            ".",
            "(",
            ")",
            "\"",
            "תווית",
            "[{\"",
            "iw",
            "label",
            "{",
            "}",
            "]",
            "[",
            ")",
            "שפ",
            "הלבינ",
            "מתכונ",
            "גרמ",
            "גרמימ",
            "קילו",
            "חצי",
            "שליש",
            "רבע",
            "מיכל",
            "quot",
            "סמ",
            "ר",
            "מ",
            "ל",
            "”",
            "-",
            "׳׳",
            "׳",
            "לקישוט",
            "טבילה",
            "של",
            "en",
            "languageCode",
            "תנובה"
        };
    }
}
