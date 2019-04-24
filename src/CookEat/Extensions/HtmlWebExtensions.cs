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
            for (var i = 0; i < 3; i++)
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