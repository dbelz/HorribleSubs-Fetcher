using System;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Samples.FetchPackList
{
    class Program
    {
        static async Task Main()
        {
            var fetcher = new Fetcher();
            var tokenSource = new CancellationTokenSource();

            const string bot = "Ginpachi-Sensei";

            var packList = await fetcher.FetchPackListAsync(bot, tokenSource.Token);

            foreach (var item in packList)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
