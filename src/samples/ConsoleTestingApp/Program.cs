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
            var api = new FetchApi();

            var watch = new Stopwatch();
            watch.Start();
            var result = await api.FetchPackListAsync(CancellationToken.None);
            watch.Stop();

            Console.WriteLine("Hello World!");
        }
    }
}
