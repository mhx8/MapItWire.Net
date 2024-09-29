using MapItWire.Net.Exceptions;

namespace MapItWire.Net.Utils;

internal static class RequestUtils
{
    private const string WireMockRequestFile = "request.json";

    internal static async Task SaveRequestAsync(
        string requestBody,
        string requestIdentifier)
    {
        string requestFolderPath = Path.Combine(
            FileUtils.GetRequestFolderPath(requestIdentifier));

        await File.WriteAllTextAsync(
            Path.Combine(
                requestFolderPath,
                WireMockRequestFile),
            requestBody);
    }

    internal static string GetRequest(
        string requestIdentifier)
    {
        string wireMockRequestFilePath = Path.Combine(
            FileUtils.GetRequestFolderPath(requestIdentifier),
            WireMockRequestFile);

        if (!File.Exists(wireMockRequestFilePath))
        {
            throw new MapItWireRequestNotFoundException($"No request found for request identifier '{requestIdentifier}'.");
        }

        string fileContent = File.ReadAllText(wireMockRequestFilePath);
        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new MapItWireRequestNotFoundException($"Empty request found for request identifier '{requestIdentifier}'.");
        }

        return fileContent;
    }
}
