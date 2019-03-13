using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HorribleSubsFetcher
{
    internal class Parser
    {
        internal async Task<IEnumerable<Pack>> ParsePacklistAsync(
            Stream stream,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        internal async Task<IEnumerable<string>> ParseBotsAsync(
            Stream stream,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
