using MapItWire.Net.Exceptions;
using Newtonsoft.Json;
using WireMock.Server;

namespace MapItWire.Net.Utils;

internal static class MappingUtils
{
    private const string WireMockMappingFile = "mappings.json";
    private const string InitialScenarioState = "1";
    private const string DefaultScenarioState = "1";

    private static readonly object FileLock = new();

    internal static void ReadStaticMappings(
        string requestIdentifier,
        WireMockServer wireMockServer)
    {
        ArgumentNullException.ThrowIfNull(wireMockServer);
        lock (FileLock)
        {
            string requestFolderPath = FileUtils.GetRequestFolderPath(requestIdentifier);
            if (!Directory.Exists(requestFolderPath))
            {
                throw new MapItWireMappingNotFoundException($"No mapping folder found for request identifier '{requestIdentifier}'.");
            }

            DirectoryInfo staticMappingFolderInfo = new(requestFolderPath);
            FileInfo[] staticMappingFiles = staticMappingFolderInfo.GetFiles();
            if (staticMappingFiles.Length == 0)
            {
                throw new MapItWireMappingNotFoundException($"No mapping files found for request identifier '{requestIdentifier}'.");
            }

            wireMockServer.ReadStaticMappings(requestFolderPath);
        }
    }

    internal static void WriteMappingToFile(
        MapItWireMapping mapping,
        string requestIdentifier)
    {
        ArgumentNullException.ThrowIfNull(mapping);
        lock (FileLock)
        {
            string requestFolderPath = FileUtils.GetRequestFolderPath(requestIdentifier);
            if (!Directory.Exists(requestFolderPath))
            {
                Directory.CreateDirectory(requestFolderPath);
            }

            string staticMappingFilePath = Path.Combine(
                requestFolderPath,
                WireMockMappingFile);
            string content = null!;
            if (File.Exists(staticMappingFilePath))
            {
                content = File.ReadAllText(staticMappingFilePath);
            }

            List<MapItWireMapping> mappings = [];
            if (!string.IsNullOrWhiteSpace(content))
            {
                mappings = JsonConvert.DeserializeObject<List<MapItWireMapping>>(content) ?? [];
            }

            CreateScenarioForDuplicateRequests(
                mappings,
                mapping);
            mappings.Add(mapping);

            string json = JsonConvert.SerializeObject(
                mappings,
                Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText(
                staticMappingFilePath,
                json);
        }
    }

    private static void CreateScenarioForDuplicateRequests(
        IEnumerable<MapItWireMapping> mappings,
        MapItWireMapping currentMapping)
    {
        MapItWireMapping? equalMapping = mappings.Where(
                map => Equals(
                    map.Request,
                    currentMapping.Request))
            .MaxBy(map => int.Parse(map.SetStateTo ?? DefaultScenarioState));

        if (equalMapping is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(equalMapping.Scenario))
        {
            equalMapping.Scenario = equalMapping.GetHashCode()
                .ToString();
            equalMapping.WhenStateIs = null;
            equalMapping.SetStateTo = InitialScenarioState;
        }

        currentMapping.Scenario = equalMapping.Scenario;
        currentMapping.WhenStateIs = equalMapping.SetStateTo;
        currentMapping.SetStateTo = (int.Parse(equalMapping.SetStateTo!) + 1).ToString();
    }
}
