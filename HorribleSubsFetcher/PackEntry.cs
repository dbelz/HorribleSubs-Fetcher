using Newtonsoft.Json;

namespace HorribleSubsFetcher
{
    public class PackEntry
    {
        [JsonProperty("b")]
        public string Bot { get; set; }

        [JsonProperty("n")]
        public string Pack { get; set; }

        [JsonProperty("s")]
        public string Size { get; set; }

        [JsonProperty("f")]
        public string Filename { get; set; }

        public override string ToString()
        {
            return $"/msg {Bot} xdcc send #{Pack}";
        }
    }
}
