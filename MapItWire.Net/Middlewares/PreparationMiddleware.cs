using System.Diagnostics;
using MapItWire.Net.Extensions;
using MapItWire.Net.Utils;
using Microsoft.AspNetCore.Http;

namespace MapItWire.Net.Middlewares;

internal class PreparationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (Debugger.IsAttached &&
            context.TryGetRequestIdentifier(out string? requestIdentifier))
        {
            PreparationUtils.Prepare(
                requestIdentifier!);
        }

        await next(context);
    }
}
