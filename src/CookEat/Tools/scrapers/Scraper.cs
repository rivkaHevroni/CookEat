using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CookEat
{
    public abstract class Scraper
    {
        public string BaseUrl { get; }

        public Scraper(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public abstract Task<Recipe> ScrapeAsync(string url);

        public bool IsRelevantUrl(string url) =>
            url.StartsWith(BaseUrl);

        protected uint GetIdFromUrl(string url)
        {
            return BitConverter.ToUInt32(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(url)), 0);
        }

        protected int GetNumberOfDishes(string dishes)
        {
            int numOfDishes = 0;
            string[] WordsInStr = dishes.Split(' ');

            for (int wordIndex = 0; wordIndex < WordsInStr.Length; wordIndex++)
            {
                int.TryParse(WordsInStr[wordIndex], out numOfDishes);
                break;
            }

            return numOfDishes;
        }
    }
}
