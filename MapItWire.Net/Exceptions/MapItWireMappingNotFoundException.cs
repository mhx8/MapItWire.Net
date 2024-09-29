namespace MapItWire.Net.Exceptions;

public class MapItWireMappingNotFoundException : Exception
{
    public MapItWireMappingNotFoundException()
    {
    }

    public MapItWireMappingNotFoundException(
        string? message)
        : base(message)
    {
    }
    
    public MapItWireMappingNotFoundException(
        string? message,
        Exception? innerException)
        : base(message, innerException)
    {
    }
}