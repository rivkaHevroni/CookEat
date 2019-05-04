using HebrewNLP.Morphology;
using System.Collections.Generic;
using System.Linq;

namespace CookEat
{
    public static class TokanizationHelper
    {
        public static List<string> Tokenaize(string searchStr)
        {
            HebrewNLP.HebrewNLP.Password = "RkelEnGYeUcWCDd";
            List<string> searchValues = HebrewMorphology.NormalizeSentence(searchStr);
            List<string> tokenaizedSearchValues = RemoveHebrewConnectorsAndAdjectives(searchValues);

            return tokenaizedSearchValues;
        }

        private static List<string> RemoveHebrewConnectorsAndAdjectives(List<string> searchValues)
        {
            return searchValues.
                Where(value => !Constants.HebrewUnnecessaryToEmptyString.Contains(value)).
                ToList();
        }

		public static string TokenaizeForOneValue(string searchStr)
		{
			HebrewNLP.HebrewNLP.Password = "RkelEnGYeUcWCDd";
			List<string> searchValues = HebrewMorphology.NormalizeSentence(searchStr);
			List<string> tokenaizedSearchValues = RemoveHebrewConnectorsAndAdjectives(searchValues);

			return tokenaizedSearchValues.First();
		}
	}
}
