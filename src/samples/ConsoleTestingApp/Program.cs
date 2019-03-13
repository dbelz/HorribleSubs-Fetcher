using HorribleSubsFetcher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var api = new FetchApi();
            var result = await api.SearchPacklistAsync("Toradora", CancellationToken.None);

            Console.WriteLine("Hello World!");
        }
    }
}
