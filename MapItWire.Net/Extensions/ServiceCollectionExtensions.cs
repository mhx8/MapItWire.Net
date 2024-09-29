using MapItWire.Net.Rest;
using MapItWire.Net.Soap;
using MapItWire.Net.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace MapItWire.Net.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapItWire(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddTransient<RestHttpMessageHandler>()
            .AddTransient<SoapMessageBehavior>()
            .AddTransient<SoapMessageInspector>()
            .AddSingleton<IRequestIdentifierStore, RequestIdentifierStore>();
}
