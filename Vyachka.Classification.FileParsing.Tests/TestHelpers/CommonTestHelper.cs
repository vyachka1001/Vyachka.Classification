namespace Vyachka.Classification.FileParsing.Tests.TestHelpers
{
    internal static class CommonTestHelper
    {
        public static string BuildFilePath(string fileName)
        {
            const string directoryPath = @"..\..\";
            var fullPath = directoryPath + fileName;

            return fullPath;
        }
    }
}