using System.ServiceModel.Description;
using MapItWire.Net.Soap;
using Microsoft.Extensions.DependencyInjection;

namespace MapItWire.Net.Extensions;

public static class ServiceEndpointExtensions
{
    public static void AddMapItWireEndpointBehavior(this ServiceEndpoint endpoint, IServiceProvider serviceProvider)
        => endpoint?.EndpointBehaviors.Add(serviceProvider.GetRequiredService<SoapMessageBehavior>());
}
