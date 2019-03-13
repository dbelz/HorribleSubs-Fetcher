using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Parsing
{
    internal static class PacklistParser
    {
        internal async Task<IEnumerable<Pack>> ParseAsync(
            Stream stream,
            CancellationToken token)
        {
            var result = new List<Pack>();
            var reader = new StreamReader(stream);

            string line = await reader.ReadLineAsync();

            while (!string.IsNullOrWhiteSpace(line) && !token.IsCancellationRequested)
            {
                var pack = ExtractPack(line);

                if (pack != null)
                    result.Add(pack);

                line = await reader.ReadLineAsync();
            }

            return result;
        }

        internal static Pack ExtractPack(string line)
        {
            var lineSplitted = line.Split('=');

            if (lineSplitted.Length < 2)
                return null;

            var json = lineSplitted[1];

            if (json.EndsWith(";"))
                json = json.Substring(0, json.Length - 1);

            return JsonConvert.DeserializeObject<Pack>(json);
        }
    }
}
