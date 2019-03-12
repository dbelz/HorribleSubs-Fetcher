using HorribleSubsFetcher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fetcher = new Fetcher();
            var results = await fetcher.SearchPacklistAsync("Overlord", CancellationToken.None);

            Console.ReadLine();
        }
    }
}
