using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.IntegrationApiMdl.Model
{
    public class DigitBridgeHttpResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("error")]
        public ErrorInfo Error { get; set; }
    }

    public class ErrorInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
