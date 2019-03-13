using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    public class FetchApi : IDisposable
    {
        private const string BASE_URL = "https://xdcc.horriblesubs.info/";
        private const string SEARCH_URL_SKELETON = "https://xdcc.horriblesubs.info/search.php?t={0}";
        private const string BOT_PACKS_URL = "https://xdcc.horriblesubs.info/search.php?nick={0}";

        private readonly HttpClient _http;
        private readonly Parser _parser;

        public FetchApi(HttpClient customHttpClient = null)
        {
            _http = customHttpClient ?? new HttpClient();
            _parser = new Parser();
        }

        /// <summary>
        /// Searches for packs which match the term on all bots.
        /// </summary>
        /// <param name="term">The search term.</param>
        /// <param name="token">The cancellation token which can be used to cancel the operation.</param>
        /// <returns>A list which contains all matching packs.</returns>
        public async Task<IEnumerable<Pack>> FindPacksAsync(
            string term,
            CancellationToken token)
        {
            Argument.NotNullOrWhiteSpace(term, nameof(term));

            var uri = string.Format(SEARCH_URL_SKELETON, term);
            var stream = await _http.GetStreamAsync(uri);

            return await _parser.ParsePacklistAsync(stream, token);
        }

        /// <summary>
        /// Fetches the pack list of all bots.
        /// </summary>
        /// <param name="token">The cancellation token which can be used to cancel the operation.</param>
        /// <returns>A list which contains all packs.</returns>
        public async Task<IEnumerable<Pack>> FetchPackListAsync(
            CancellationToken token)
        {
            var packList = new List<Pack>();
            var botList = (await FetchBotsAsync(token)).ToList();

            var tasks = botList.Select(async bot =>
            {
                var packs = await FetchPackListAsync(bot, token);
                packList.AddRange(packs);
            });

            await Task.WhenAll(tasks);

            return packList;
        }

        public async Task<IEnumerable<Pack>> FetchPackListAsync(
            string botName,
            CancellationToken token)
        {
            Argument.NotNullOrWhiteSpace(botName, nameof(botName));

            var uri = string.Format(BOT_PACKS_URL, botName);
            var stream = await _http.GetStreamAsync(uri);

            return await _parser.ParsePacklistAsync(stream, token);
        }

        /// <summary>
        /// Fetches all available bot names.
        /// </summary>
        /// <param name="token">The cancellation token which can be used to cancel the operation.</param>
        /// <returns>A list which contains all bot names.</returns>
        public async Task<IEnumerable<string>> FetchBotsAsync(
            CancellationToken token)
        {
            var stream = await _http.GetStreamAsync(BASE_URL);
            return await _parser.ParseBotsAsync(stream, token);
        }

        public void Dispose()
        {
            _http?.Dispose();
        }
    }
}
