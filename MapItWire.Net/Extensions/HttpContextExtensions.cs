using MapItWire.Net.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MapItWire.Net.Extensions;

internal static class HttpContextExtensions
{
    internal static bool TryGetRequestIdentifier(
        this HttpContext httpContext,
        out string? requestIdentifier)
    {
        requestIdentifier = null;
        if (httpContext?.Request?.Headers?.TryGetValue(
                MapItWireConstants.RequestIdentifierHeader,
                out StringValues requestIdentifierValues) == true)
        {
            requestIdentifier = requestIdentifierValues.FirstOrDefault();
        }

        return !string.IsNullOrWhiteSpace(requestIdentifier);
    }
}
