using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher.Parsing
{
    internal static class BotlistParser
    {
        private const string BOT_LINE_INDICATOR = "javascript:p.nickPacks('";

        internal static async Task<IEnumerable<string>> ParseAsync(
            Stream stream,
            CancellationToken token)
        {
            var result = new List<string>();
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

            return result;
        }

        private static string ExtractBotName(string raw)
        {
            var split = raw.Split('\'');

            if (split.Length < 3)
                return null;

            return split[1];
        }
    }
}
