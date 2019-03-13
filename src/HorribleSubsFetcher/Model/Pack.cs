using Newtonsoft.Json;

namespace HorribleSubsFetcher.Model
{
    public class Pack
    {
        [JsonProperty("b")]
        public string Bot { get; set; }

        [JsonProperty("n")]
        public string Number { get; set; }

        [JsonProperty("s")]
        public string Size { get; set; }

        [JsonProperty("f")]
        public string Filename { get; set; }

        public override string ToString()
        {
            return $"/msg {Bot} xdcc send #{Number}";
        }
    }
}
