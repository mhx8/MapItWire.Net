using MapItWire.Net.Rest;
using Microsoft.Extensions.DependencyInjection;

namespace MapItWire.Net.Extensions;

public static class HttpClientBuilderExtensions
{
    public static void AddMapItWireHttpMessageHandler(this IHttpClientBuilder builder)
        => builder.AddHttpMessageHandler<RestHttpMessageHandler>();
}
