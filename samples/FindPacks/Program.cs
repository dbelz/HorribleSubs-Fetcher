using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Samples.FindPacks
{
    internal class Program
    {
        private static async Task Main()
        {
            var httpClient = new HttpClient();

            var fetcher = new Fetcher(httpClient);
            var tokenSource = new CancellationTokenSource();

            var packList = await fetcher.FindPacksAsync("Toradora", tokenSource.Token);

            foreach (var item in packList)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
