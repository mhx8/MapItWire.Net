using System.Diagnostics;
using MapItWire.Net.Extensions;
using MapItWire.Net.Stores;
using Microsoft.AspNetCore.Http;

namespace MapItWire.Net.Middlewares;

internal class StoreMiddleware(
    RequestDelegate next,
    IRequestIdentifierStore store)
{
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (Debugger.IsAttached &&
            context.TryGetRequestIdentifier(out string? requestIdentifier))
        {
            store.RequestIdentifier = requestIdentifier;
        }

        await next(context);
    }
}
