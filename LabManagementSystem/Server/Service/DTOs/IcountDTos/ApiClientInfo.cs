using Newtonsoft.Json;

namespace Service.DTOs
{
    public class ApiClientInfo
    {
        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("tz")]
        public int Tz { get; set; }

        [JsonProperty("ts")]
        public double Ts { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("rid")]
        public int Rid { get; set; }

        [JsonProperty("module")]
        public string Module { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("client_id")]
        public int ClientId { get; set; }

        [JsonProperty("custom_client_id")]
        public string CustomClientId { get; set; }
    }
}
