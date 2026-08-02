using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.Helpers
{
    public static class JsonHelper
    {
        public static readonly string SteamDataPath = Path.Combine(FileHelper.getBaseDirectory(), "UnturnedServerUtility", "Data", "SteamData.json");
        public static T getDataFromJsonFile<T>(string filePath)
        {
            if (!FileHelper.fileExists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }
            string jsonData = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
