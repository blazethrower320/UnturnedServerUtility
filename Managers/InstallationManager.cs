using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.Json;
using UnturnedServerUtility.Helpers;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.Managers
{
    public class InstallationManager
    {
        public readonly string dataBaseDirectory = Path.Combine(FileHelper.getBaseDirectory(), "UnturnedServerUtility", "Data");
        public readonly string steamCMDDirectory = Path.Combine(FileHelper.getBaseDirectory(), "UnturnedServerUtility", "SteamCMD");
        public string serverDirectory => Path.Combine(steamCMDDirectory, "steamapps", "common", "Unturned");

        public void InitProject()
        {
            string templateSteamDataPath = Path.Combine(FileHelper.getBaseDirectory(), "Data", "SteamData.json");

            Directory.CreateDirectory(dataBaseDirectory);
            Directory.CreateDirectory(steamCMDDirectory);
            if (!File.Exists(JsonHelper.SteamDataPath))
            {
                File.Copy(templateSteamDataPath, JsonHelper.SteamDataPath);
            }
        }


        public bool CheckSteamCMDInstallation()
        {
            return File.Exists(Path.Combine(steamCMDDirectory, "steamcmd.exe"));
        }


        public bool InstallSteamCMD()
        {
            string steamCmdExePath = Path.Combine(steamCMDDirectory, "steamcmd.exe");
            string steamCmdZipPath = Path.Combine(steamCMDDirectory, "steamcmd.zip");
            string logFilePath = Path.Combine(steamCMDDirectory, "steamcmd_installation.log");
            
            try
            {
                var jsonContent = File.ReadAllText(JsonHelper.SteamDataPath);
                var steamData = JsonSerializer.Deserialize<SteamData>(jsonContent, new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip });
                if (steamData == null)
                    throw new Exception("Invalid SteamData.json");
                
                // Download SteamCMD
                if (!File.Exists(steamCmdExePath))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(steamData.SteamInstallation.SteamCMDUrl, steamCmdZipPath);
                    }

                    ZipFile.ExtractToDirectory(steamCmdZipPath, steamCMDDirectory,true);
                }


                string arguments = $"+force_install_dir \"{serverDirectory}\" " +"+login anonymous " +"+app_update 1110390 " + "+quit";
                string output = RunSteamCMD(steamCmdExePath, arguments);
                File.WriteAllText(logFilePath, output);

                // SteamCMD first launch updates itself.
                // Retry after update.
                if (output.Contains("Update complete, launching") || output.Contains("Missing configuration"))
                {
                    System.Threading.Thread.Sleep(5000);
                    output = RunSteamCMD(steamCmdExePath, arguments);
                    File.AppendAllText(logFilePath, "\n\n===== RETRY =====\n\n" + output);
                }
                string unturnedExe = Path.Combine(serverDirectory, "Unturned.exe");

                if (File.Exists(unturnedExe))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                File.WriteAllText(logFilePath, ex.ToString());
                return false;
            }
        }
        private string RunSteamCMD(string exe, string arguments)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = exe,
                WorkingDirectory = steamCMDDirectory,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            StringBuilder output = new StringBuilder();
            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    output.AppendLine(e.Data);
                }
            };
            process.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    output.AppendLine(e.Data);
                }
            };


            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            return output.ToString();
        }
    }
}