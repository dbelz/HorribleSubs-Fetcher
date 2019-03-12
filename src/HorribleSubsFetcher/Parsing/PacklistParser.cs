using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Parsing
{
    internal static class PacklistParser
    {
        internal static async Task<IEnumerable<PackEntry>> ParseAsync(
            Stream stream,
            CancellationToken token)
        {
            var result = new List<PackEntry>();
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

        internal static PackEntry ExtractPack(string line)
        {
            var lineSplitted = line.Split('=');

            if (lineSplitted.Length < 2)
                return null;

            var json = lineSplitted[1];

            if (json.EndsWith(";"))
                json = json.Substring(0, json.Length - 1);

            return JsonConvert.DeserializeObject<PackEntry>(json);
        }
    }
}
