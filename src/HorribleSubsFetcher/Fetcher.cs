using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    public class Fetcher : IDisposable
    {
        private const string BASE_URL = "https://xdcc.horriblesubs.info/";
        private const string SEARCH_IN_ALL_PACKLISTS_URL = "https://xdcc.horriblesubs.info/search.php?t={0}";
        private const string SEARCH_IN_BOT_PACKLIST_URL = "https://xdcc.horriblesubs.info/search.php?t={0}&nick={1}";
        private const string BOT_PACKS_URL = "https://xdcc.horriblesubs.info/search.php?nick={0}";

        private readonly HttpClient _http;
        private readonly Parser _parser;

        public Fetcher(HttpClient customHttpClient = null)
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

            var uri = string.Format(SEARCH_IN_ALL_PACKLISTS_URL, term);
            var stream = await _http.GetStreamAsync(uri);

            return await _parser.ParsePacklistAsync(stream, token);
        }

        /// <summary>
        /// Searches for packs which match the term on a specific bot.
        /// </summary>
        /// <param name="term">The search term.</param>
        /// <param name="bot">The bot.</param>
        /// <param name="token">The cancellation token which can be used to cancel the operation.</param>
        /// <returns>A list which contains all matching packs.</returns>
        public async Task<IEnumerable<Pack>> FindPacksAsync(
            string term,
            string bot,
            CancellationToken token)
        {
            Argument.NotNullOrWhiteSpace(term, nameof(term));
            Argument.NotNullOrWhiteSpace(bot, nameof(bot));

            var uri = string.Format(SEARCH_IN_BOT_PACKLIST_URL, term, bot);
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

        /// <summary>
        /// Fetches the pack list of a specific bot.
        /// </summary>
        /// <param name="bot">The bot.</param>
        /// <param name="token">The cancellation token which can be used to cancel the operation.</param>
        /// <returns>A list which contains all packs of the bot.</returns>
        public async Task<IEnumerable<Pack>> FetchPackListAsync(
            string bot,
            CancellationToken token)
        {
            Argument.NotNullOrWhiteSpace(bot, nameof(bot));

            var uri = string.Format(BOT_PACKS_URL, bot);
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
