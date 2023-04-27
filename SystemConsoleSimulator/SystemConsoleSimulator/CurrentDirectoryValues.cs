using System.IO;

namespace SystemConsoleSimulator
{
    class CurrentDirectoryValues
    {
        public static string CurrentDirectory = Directory.GetCurrentDirectory();
        public static string ShowCurrentDirectory()
        { 
            return CurrentDirectory;
        }
    }
}
