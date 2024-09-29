using System.Reflection;
using MapItWire.Net.Utils;
using WireMock.Server;
using Xunit.Sdk;

namespace MapItWire.Net.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class MapItWireMappingAttribute(
    string requestIdentifier) : BeforeAfterTestAttribute
{
    public override void Before(
        MethodInfo methodUnderTest)
    {
        WireMockServer? server = MapItWireServer.GetServer();
        if (server is null)
        {
            throw new InvalidOperationException("WireMock server is not started");
        }

        MappingUtils.ReadStaticMappings(
            requestIdentifier,
            server);
    }

    public override void After(
        MethodInfo methodUnderTest)
        => MapItWireServer.ResetMappings();
}