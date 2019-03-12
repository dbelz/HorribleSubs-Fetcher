using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    public class Fetcher : IDisposable
    {
        private const string SEARCH_URL_SKELETON = "https://xdcc.horriblesubs.info/search.php?t={0}";

        private readonly HttpClient _http;
        private readonly ResponseParser _parser;

        private CancellationToken _token;

        public Fetcher(HttpClient sharedHttpClient = null)
        {
            if (sharedHttpClient != null)
                _http = sharedHttpClient;
            else
                _http = new HttpClient();

            _parser = new ResponseParser();
        }

        public async Task<IEnumerable<PackEntry>> SearchPacklistAsync(
            string term,
            CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(term))
                throw new ArgumentNullException();

            _token = token;

            var requestUri = string.Format(SEARCH_URL_SKELETON, term);
            var responseStream = await _http.GetStreamAsync(requestUri);

            return await _parser.Parse(responseStream, token);
        }

        public void Dispose()
        {
            _http?.Dispose();
        }
    }
}
