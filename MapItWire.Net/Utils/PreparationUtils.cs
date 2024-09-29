namespace MapItWire.Net.Utils;

internal static class PreparationUtils
{
    internal static void Prepare(string requestIdentifier)
    {
        string wireMockRequestFolderPath = Path.Combine(
            FileUtils.GetRequestFolderPath(requestIdentifier));

        // delete old mapping and request files
        if (Directory.Exists(wireMockRequestFolderPath))
        {
            Directory.Delete(wireMockRequestFolderPath, true);
        }

        Directory.CreateDirectory(wireMockRequestFolderPath);
    }
}
