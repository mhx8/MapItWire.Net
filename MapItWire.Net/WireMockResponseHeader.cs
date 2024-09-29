using Newtonsoft.Json;

namespace MapItWire.Net;

internal class WireMockResponseHeader
{
    [JsonProperty(PropertyName ="Content-Type")]
    public string? ContentType { get; set; }
}
