using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoClient.Data
{
    public class Token
    {
        [JsonPropertyName("alg")]
        public string Algorithm { get; set; }

        [JsonPropertyName("typ")]
        public string Type { get; set; }

        [JsonPropertyName("sub")]
        public string UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iat")]
        public DateTime TimestampUtc { get; set; }
    }
}
