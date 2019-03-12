using HorribleSubsFetcher;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var horribleSubsFetchApi = new FetchApi();
            var cancellationTokenSource = new CancellationTokenSource();

           var bots = await horribleSubsFetchApi.GetBotlistAsync(CancellationToken.None);

            /* var packs = await fetcher.SearchPacklistAsync("Toradora", cancellationTokenSource.Token);

            var firstPack = packs.First();

            Console.WriteLine(firstPack); */

            Console.ReadLine();
        }
    }
}
