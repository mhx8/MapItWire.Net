using MapItWire.Net.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MapItWire.Net.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMapItWire(this IApplicationBuilder app)
        => app
            .UseMiddleware<PreparationMiddleware>()
            .UseMiddleware<StoreMiddleware>()
            .UseMiddleware<RequestMiddleware>();
}
