using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UnturnedServerUtility.Helpers
{
    public static class FileHelper
    {
        public static bool fileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        public static List<string> getFilesInDirectory(string filePath)
        {
            return new List<string>(Directory.GetFiles(filePath));
        }
        public static string getBaseDirectory()
        {
            return AppContext.BaseDirectory;
        }
    }
}
