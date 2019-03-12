using HorribleSubsFetcher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fetcher = new Fetcher();
            var results = await fetcher.SearchPacklistAsync("Tokyo Ghoul", CancellationToken.None);

            Console.ReadLine();
        }
    }
}
