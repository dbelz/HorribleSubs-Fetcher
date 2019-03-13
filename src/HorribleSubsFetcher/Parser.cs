using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    internal class Parser
    {
        private const string BOT_LINE_INDICATOR = "javascript:p.nickPacks('";

        internal async Task<IEnumerable<Pack>> ParsePacklistAsync(
            Stream stream,
            CancellationToken token)
        {
            return await ReadStreamAsync(
                stream,
                token,
                ParsePack);
        }

        internal async Task<IEnumerable<string>> ParseBotsAsync(
            Stream stream,
            CancellationToken token)
        {
            return await ReadStreamAsync(
                stream,
                token,
                ParseBot);

            /* var result = new List<string>();
            var reader = new StreamReader(stream);

            string line = await reader.ReadLineAsync();

            while (!string.IsNullOrWhiteSpace(line) && !token.IsCancellationRequested)
            {
                if (line.Contains(BOT_LINE_INDICATOR))
                {
                    var bot = ExtractBotName(line);
                    result.Add(bot);
                }

                line = await reader.ReadLineAsync();
            }

            return result; */
        }

        private async Task<IEnumerable<T>> ReadStreamAsync<T>(
            Stream stream,
            CancellationToken token,
            Func<string, T> callback)
        {
            var result = new List<T>();
            var reader = new StreamReader(stream);

            string line = await reader.ReadLineAsync();

            while (!string.IsNullOrWhiteSpace(line) && !token.IsCancellationRequested)
            {
                var value = callback.Invoke(line);
                result.Add(value);

                line = await reader.ReadLineAsync();
            }

            return result;
        }

        private Pack ParsePack(string input)
        {
            var lineSplitted = input.Split('=');

            if (lineSplitted.Length < 2)
                return null;

            var json = lineSplitted[1];

            if (json.EndsWith(";"))
                json = json.Substring(0, json.Length - 1);

            return JsonConvert.DeserializeObject<Pack>(json);
        }

        private string ParseBot(string input)
        {
            var split = input.Split('\'');

            if (split.Length < 3)
                return null;

            return split[1];
        }
    }
}
