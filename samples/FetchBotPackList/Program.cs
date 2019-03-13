using System;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Samples.FetchBotPackList
{
    internal class Program
    {
        private static async Task Main()
        {
            var fetcher = new Fetcher();
            var tokenSource = new CancellationTokenSource();

            var packList = await fetcher.FetchPackListAsync(tokenSource.Token);

            foreach (var item in packList)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
