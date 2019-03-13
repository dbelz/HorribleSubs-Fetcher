using System;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Samples.FetchBots
{
    internal class Program
    {
        static async Task Main()
        {
            var fetcher = new Fetcher();
            var tokenSource = new CancellationTokenSource();

            var bots = await fetcher.FetchBotsAsync(tokenSource.Token);

            foreach (var bot in bots)
                Console.WriteLine(bot);

            Console.ReadLine();
        }
    }
}
