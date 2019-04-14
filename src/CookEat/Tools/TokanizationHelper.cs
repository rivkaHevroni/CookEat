using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebrewNLP.Morphology;


namespace CookEat
{
	public static class TokanizationHelper
	{
		public static List<string> Tokenaize(string searchStr)
		{
			HebrewNLP.HebrewNLP.Password = "RkelEnGYeUcWCDd";
			List<string> searchValues = HebrewMorphology.NormalizeSentence(searchStr, HebrewMorphology.NormalizationType.SEARCH);
			List<string> tokenaizedSearchValues = removeHeberwConnectorsAndAdjectives(searchValues);

			return tokenaizedSearchValues;
		}

		private static List<string> removeHeberwConnectorsAndAdjectives(List<string> searchValues)
		{
			List<string> tokenaizeSearchValues = new List<string>();

			for (int searchWordIndex = 0; searchWordIndex < searchValues.Count; searchWordIndex++)
			{
				if (!(Constants.HeberwUnnecessaryToEmptyString.ContainsKey(searchValues[searchWordIndex])))
				{
					tokenaizeSearchValues.Add(searchValues[searchWordIndex]);
				}
			}

			return tokenaizeSearchValues;
		}
	}
}
