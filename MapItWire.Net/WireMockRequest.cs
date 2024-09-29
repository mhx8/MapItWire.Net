namespace MapItWire.Net;

internal class WireMockRequest
{
    public string[]? Methods { get; init; }

    public string? Url { get; init; }

    public string? Path { get; init; }

    public List<WireMockHeader>? Headers { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not WireMockRequest request)
        {
            return false;
        }

        if (Methods?.Length != request.Methods?.Length)
        {
            return false;
        }

        if (Methods != null &&
            request.Methods != null &&
            !Methods.SequenceEqual(request.Methods))
        {
            return false;
        }

        if (Headers?.Count != request.Headers?.Count)
        {
            return false;
        }

        if (Headers != null &&
            request.Headers != null &&
            !Headers.SequenceEqual(request.Headers))
        {
            return false;
        }

        return Url == request.Url &&
               Path == request.Path;
    }

    public override int GetHashCode()
    {
        HashCode hash = default;
        hash.Add(Url);
        hash.Add(Path);

        foreach (string method in Methods ?? Enumerable.Empty<string>())
        {
            hash.Add(method);
        }

        foreach (WireMockHeader header in Headers ?? Enumerable.Empty<WireMockHeader>())
        {
            hash.Add(header);
        }

        return hash.ToHashCode();
    }
}
