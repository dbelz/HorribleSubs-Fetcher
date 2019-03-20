using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Samples.FetchBotList
{
    internal class Program
    {
        private static async Task Main()
        {
            var httpClient = new HttpClient();

            var fetcher = new Fetcher(httpClient);
            var tokenSource = new CancellationTokenSource();

            var bots = await fetcher.FetchBotListAsync(tokenSource.Token);

            foreach (var bot in bots)
                Console.WriteLine(bot);

            Console.ReadLine();
        }
    }
}
