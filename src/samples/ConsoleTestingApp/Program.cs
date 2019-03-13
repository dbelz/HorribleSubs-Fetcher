using HorribleSubsFetcher;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var api = new Fetcher();

            var result = await api.FetchBotsAsync(CancellationToken.None);
            Console.WriteLine("Hello World!");
        }
    }
}
