namespace MapItWire.Net;

internal class WireMockResponse
{
    public int Status { get; set; }

    public string? Body { get; set; }

    public WireMockResponseHeader? Headers { get; set; }
}
