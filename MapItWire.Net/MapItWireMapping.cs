namespace MapItWire.Net;

internal class MapItWireMapping
{
    // Used in serialization
    public Guid Guid { get; set; } = Guid.NewGuid();

    // Used in serialization
    public int Priority { get; set; } = 1;

    public string? Scenario { get; set; }

    public string? WhenStateIs { get; set; }

    public string? SetStateTo { get; set; }

    public WireMockRequest? Request { get; init; }

    public WireMockResponse? Response { get; set; }
}
