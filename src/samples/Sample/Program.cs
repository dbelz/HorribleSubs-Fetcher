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
            var fetcher = new Fetcher();
            var cancellationTokenSource = new CancellationTokenSource();

            var packs = await fetcher.SearchPacklistAsync("Toradora", cancellationTokenSource.Token);

            var firstPack = packs.First();

            Console.WriteLine(firstPack);

            Console.ReadLine();
        }
    }
}
