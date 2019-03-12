using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    internal class ResponseParser
    {
        public async Task<IEnumerable<PackEntry>> Parse(
            Stream responseStream,
            CancellationToken token)
        {
            var result = new List<PackEntry>();
            var reader = new StreamReader(responseStream);

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

        private PackEntry ExtractPack(string line)
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
