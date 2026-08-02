using System;
using System.Collections.Generic;
using System.Text;

namespace UnturnedServerUtility.Models
{
    public class SteamData
    {
        public SteamInstallation SteamInstallation { get; set; }
    }

    public class SteamInstallation
    {
        public string SteamCMDUrl { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string SteamCMDExecutable { get; set; }
    }
}
