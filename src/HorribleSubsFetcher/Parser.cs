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
        }

        private static async Task<IEnumerable<T>> ReadStreamAsync<T>(
            Stream stream,
            CancellationToken token,
            Func<string, T> callback)

            where T : class 
        {
            var result = new List<T>();

            using (var reader = new StreamReader(stream))
            {
                var line = await reader.ReadLineAsync();

                while (!string.IsNullOrWhiteSpace(line) && !token.IsCancellationRequested)
                {
                    var value = callback.Invoke(line);

                    if (value != null)
                        result.Add(value);

                    line = await reader.ReadLineAsync();
                }
            }

            stream.Dispose();

            return result;
        }

        private static Pack ParsePack(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var split = input.Split('=');

            if (split.Length < 2)
                return null;

            var json = split[1];

            if (json.EndsWith(";"))
                json = json.Substring(0, json.Length - 1);

            return JsonConvert.DeserializeObject<Pack>(json);
        }

        private static string ParseBot(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !input.Contains(BOT_LINE_INDICATOR))
                return null;

            var split = input.Split('\'');

            return split.Length < 3 ? null : split[1];
        }
    }
}
