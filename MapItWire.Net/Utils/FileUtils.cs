namespace MapItWire.Net.Utils;

internal static class FileUtils
{
    private const string SolutionFileSearchPattern = "*.sln";

    internal static string GetRequestFolderPath(string requestIdentifier)
    {
        DirectoryInfo directory = GetSolutionDirectory();
        return Path.Combine(
            directory.FullName,
            Constants.MapItWireConstants.BasePathName,
            requestIdentifier);
    }

    private static DirectoryInfo GetSolutionDirectory()
    {
        DirectoryInfo? directory = new(Directory.GetCurrentDirectory());
        while (directory?
                   .GetFiles(SolutionFileSearchPattern)
                   .Length == 0)
        {
            directory = directory.Parent;
        }

        if (directory == null)
        {
            throw new ArgumentException("No solution directory found.");
        }

        return directory;
    }
}
