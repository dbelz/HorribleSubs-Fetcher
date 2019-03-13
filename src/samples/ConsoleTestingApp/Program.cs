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

            var botList = await api.FetchBotsAsync(CancellationToken.None);
            var result = await api.FindPacksAsync("Tokyo Ghoul", botList.First(), CancellationToken.None);
            Console.WriteLine("Hello World!");
        }
    }
}
