namespace MapItWire.Net;

internal class WireMockHeader
{
    public string? Name { get; init; }

    public bool IgnoreCase { get; init; }

    public List<WireMockMatcher>? Matchers { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not WireMockHeader header)
        {
            return false;
        }

        if (Matchers?.Count != header.Matchers?.Count)
        {
            return false;
        }

        if (Matchers != null &&
            header.Matchers != null &&
            !Matchers.SequenceEqual(header.Matchers))
        {
            return false;
        }

        return Name == header.Name &&
               IgnoreCase == header.IgnoreCase;
    }

    public override int GetHashCode()
    {
        HashCode hash = default;
        hash.Add(Name);
        hash.Add(IgnoreCase);

        foreach (WireMockMatcher matcher in Matchers ?? Enumerable.Empty<WireMockMatcher>())
        {
            hash.Add(matcher);
        }

        return hash.ToHashCode();
    }
}
