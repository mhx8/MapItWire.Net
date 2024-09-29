using System.Diagnostics;
using MapItWire.Net.Extensions;
using MapItWire.Net.Utils;
using Microsoft.AspNetCore.Http;

namespace MapItWire.Net.Middlewares;

internal class RequestMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (Debugger.IsAttached &&
            context.TryGetRequestIdentifier(out string? requestIdentifier))
        {
            context.Request.EnableBuffering();
            context.Request.Body.Position = 0;

            using StreamReader reader = new(
                context.Request.Body,
                encoding: default,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: -1,
                leaveOpen: true);
            string requestBody = await reader.ReadToEndAsync();
            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                await RequestUtils.SaveRequestAsync(
                    requestBody,
                    requestIdentifier!);

                context.Request.Body.Position = 0;
            }
        }

        await next(context);
    }
}
