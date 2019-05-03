using System;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Humanizer;

namespace CookEat
{
    public static class HtmlWebExtensions
    {
        public static async Task<HtmlDocument> LoadWithRetryAsync(this HtmlWeb htmlWeb, string url)
        {
            for (var attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    return htmlWeb.Load(url);
                }
                catch (WebException exception)
                {
                    Console.WriteLine($"WARNING: {exception.Message}");
                    await Task.Delay(1.Minutes());
                }
            }

            return htmlWeb.Load(url);
        }
    }
}