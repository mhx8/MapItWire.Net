namespace MapItWire.Net.Exceptions;

public class MapItWireRequestNotFoundException : Exception
{
    public MapItWireRequestNotFoundException()
    {
    }

    public MapItWireRequestNotFoundException(
        string? message)
        : base(message)
    {
    }
    
    public MapItWireRequestNotFoundException(
        string? message,
        Exception? innerException)
        : base(message, innerException)
    {
    }
}