using WireMock.Server;

namespace MapItWire.Net;

public class MapItWireFixture : IDisposable
{
    private readonly WireMockServer _server = MapItWireServer.Start();
    
    public WireMockServer GetServer() => _server;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _server.Stop();
        }
    }
}