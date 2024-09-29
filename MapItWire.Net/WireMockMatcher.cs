namespace MapItWire.Net;

internal class WireMockMatcher
{
    public string? Name { get; init; }

    public string? Pattern { get; init; }

    public bool IgnoreCase { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is not WireMockMatcher matcher)
        {
            return false;
        }

        return Name == matcher.Name &&
               Pattern == matcher.Pattern &&
               IgnoreCase == matcher.IgnoreCase;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Name,
            Pattern,
            IgnoreCase);
    }
}
