﻿using HorribleSubsFetcher.Parsing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    public class Fetcher : IDisposable
    {
        private const string BASE_URL = "https://xdcc.horriblesubs.info/";
        private const string SEARCH_URL_SKELETON = "https://xdcc.horriblesubs.info/search.php?t={0}";

        private readonly HttpClient _http;

        public Fetcher(HttpClient sharedHttpClient = null)
        {
            if (sharedHttpClient != null)
                _http = sharedHttpClient;
            else
                _http = new HttpClient();
        }

        public async Task<IEnumerable<PackEntry>> SearchPacklistAsync(
            string term,
            CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(term))
                throw new ArgumentNullException();

            var uri = string.Format(SEARCH_URL_SKELETON, term);
            var stream = await _http.GetStreamAsync(uri);

            return await PacklistParser.ParseAsync(stream, token);
        }

        public async Task<IEnumerable<string>> GetBotsAsync(
            CancellationToken token)
        {
            var stream = await _http.GetStreamAsync(BASE_URL);
            return await BotlistParser.ParseAsync(stream, token);
        }

        public void Dispose()
        {
            _http?.Dispose();
        }
    }
}
