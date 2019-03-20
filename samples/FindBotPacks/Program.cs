using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Samples.FindBotPacks
{
    internal class Program
    {
        private static async Task Main()
        {
            var httpClient = new HttpClient();

            var fetcher = new Fetcher(httpClient);
            var tokenSource = new CancellationTokenSource();

            var bot = "Ginpachi-Sensei";

            var packList = await fetcher.FindBotPacksAsync("Toradora", bot, tokenSource.Token);

            foreach (var item in packList)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
