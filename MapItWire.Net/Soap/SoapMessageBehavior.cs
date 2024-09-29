using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace MapItWire.Net.Soap;

internal class SoapMessageBehavior(SoapMessageInspector messageInspector) : IEndpointBehavior
{
    public void AddBindingParameters(
        ServiceEndpoint endpoint,
        BindingParameterCollection bindingParameters)
    {
        // nothing to do
    }

    public void ApplyClientBehavior(
        ServiceEndpoint endpoint,
        ClientRuntime clientRuntime)
    {
        ArgumentNullException.ThrowIfNull(clientRuntime);
        clientRuntime.ClientMessageInspectors.Add(messageInspector);
    }

    public void ApplyDispatchBehavior(
        ServiceEndpoint endpoint,
        EndpointDispatcher endpointDispatcher)
    {
        // nothing to do
    }

    public void Validate(ServiceEndpoint endpoint)
    {
        // nothing to do
    }
}
