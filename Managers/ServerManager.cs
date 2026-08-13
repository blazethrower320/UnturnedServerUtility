using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using UnturnedServerUtility.Helpers;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.Managers
{
    public static class ServerManager
    {
        public static readonly string serverDirectory = Path.Combine(FileHelper.getBaseDirectory(), "UnturnedServerUtility", "SteamCMD", "steamapps", "common", "Unturned");
        public static List<ServerCollection> Servers { get; set; }
        static ServerManager()
        {
            Servers = new List<ServerCollection>();
            Servers.Add(new ServerCollection
            {
                Tracker = new ServerTracker
                {
                    ServerName = "Example",
                    ServerExecutable = "ExampleServer.bat",
                    TrackingId = Guid.NewGuid(),
                    ProcessId = null,
                    Process = null,
                    StartDate = null
                },
                severName = "Example Server",
                playerCount = "0",
                playerMax = "20"
            });
            Servers.Add(new ServerCollection
            {
                Tracker = new ServerTracker
                {
                    ServerName = "TestServer",
                    ServerExecutable = "TestServer.bat",
                    TrackingId = Guid.NewGuid(),
                    ProcessId = null,
                    Process = null,
                    StartDate = null,
                },
                severName = "Test Server",
                playerCount = "5",
                playerMax = "20"
            });
        }

        public static List<ServerCollection> getRunningServers()
        {
            return Servers;
        }

        public static List<string> getAllServerExecutables()
        {
            List<string> executableNames = new List<string>();
            if(Directory.Exists(serverDirectory))
            {
                // Full path 
                string[] exeFiles = Directory.GetFiles(serverDirectory, "*.bat", SearchOption.TopDirectoryOnly);
                foreach (string exeFile in exeFiles)
                {
                    // File name only
                    executableNames.Add(Path.GetFileName(exeFile));
                }
            }
            // ServerHelper not a valid Server
            executableNames.Remove("ServerHelper.bat");
            return executableNames;
        }

        public static void RefreshServerList()
        {
            var servers = getAllServerExecutables();
            foreach (var server in servers)
            {
                Debug.WriteLine(server);
            }
        }


        public static bool restartServer(ServerCollection server)
        {
            if (stopServer(server))
            {
                startServer(server);
                return true;
            }
            return false;
        }







        /*
        public static void startServer(ServerCollection serverExe)
        {
            string unturnedExePath = Path.Combine(serverDirectory, serverExe.Tracker.ServerExecutable);

            if (!File.Exists(unturnedExePath))
            {
                Debug.WriteLine($"✗ Unturned.exe not found at {unturnedExePath}");
                return;
            }

            // Mirrors what ServerHelper.bat / ExampleServer.bat were forwarding:
            // -nographics -batchmode +LanServer/<Name> (or +InternetServer/<Id> for public servers)
            string serverArgName = Path.GetFileNameWithoutExtension(serverExe.Tracker.ServerExecutable);
            string arguments = $"-nographics -batchmode -commandline +LanServer/{serverArgName}";

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = unturnedExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = serverDirectory
            };

            process.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data))
                    Debug.WriteLine("[SERVER] " + e.Data);
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data))
                    Debug.WriteLine("[ERROR] " + e.Data);
            };

            process.Start();
            Debug.WriteLine($"✓ Unturned.exe started directly with Process ID: {process.Id}");

            process.WaitForExit();

            Debug.WriteLine(process.ExitCode);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            serverExe.Tracker.ProcessId = process.Id;
            serverExe.Tracker.Process = process;
            serverExe.Tracker.StartDate = DateTime.Now;

            var existingServer = Servers.FirstOrDefault(c => c.Tracker.TrackingId == serverExe.Tracker.TrackingId);
            if (existingServer != null)
            {
                existingServer.Tracker = serverExe.Tracker;
            }

            RefreshServerList();
        }

        public static bool stopServer(ServerCollection server)
        {
            if (server.Tracker == null || server.Tracker.Process == null)
            {
                Debug.WriteLine("Issue.... Process is null");
                return false;
            }

            Debug.WriteLine($"[STOP] Attempting to stop server: {server.severName}");

            try
            {
                while (!server.Tracker.Process.HasExited)
                {
                    server.Tracker.Process.Kill();
                    server.Tracker.Process.WaitForExit();
                }
                Debug.WriteLine($"[STOP] ✓ Server stopped");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[STOP] Error: {e}");
                return false;
            }

            server.Tracker.Process = null;
            server.Tracker.ProcessId = null;
            server.Tracker.TrackingId = null;
            server.Tracker.StartDate = null;

            return true;
        }
        */
        public static void startServer(ServerCollection server)
        {
            string unturnedExePath = Path.Combine(serverDirectory, "Unturned.exe");

            if (!File.Exists(unturnedExePath))
            {
                Debug.WriteLine($"Unturned.exe not found at: {unturnedExePath}");
                return;
            }
            string serverName = Path.GetFileNameWithoutExtension(server.Tracker.ServerExecutable);

            //string arguments = $"-batchmode -nographics +LanServer/{serverName}";

            string arguments = $"-batchmode -nographics -commandline +LanServer/{serverName}";

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = unturnedExePath,
                Arguments = arguments,
                WorkingDirectory = serverDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,  // Must be true to allocate a native Windows Console window for RocketMod input
                CreateNoWindow = false
            };

            process.EnableRaisingEvents = true;

            StringBuilder output = new StringBuilder();
            process.OutputDataReceived += (sender, e) => {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                    {
                        server.ServerConsole.Add(e.Data);
                    });
                    Debug.WriteLine($"[Server Out]: {e.Data}");
                }
            };

            // Handle Standard Error (RocketMod/Unturned often write errors here)
            process.ErrorDataReceived += (sender, e) => {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                    {
                        server.ServerConsole.Add(e.Data);
                    });
                    Debug.WriteLine($"[Server Err]: {e.Data}");
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            server.Tracker.Process = process;
            server.Tracker.ProcessId = process.Id;
            server.Tracker.StartDate = DateTime.Now;

            Debug.WriteLine($"Started {serverName} directly with PID {process.Id}");
        }

        public static bool stopServer(ServerCollection server)
        {
            if (server.Tracker?.Process == null)
            {
                Debug.WriteLine("Process is null");
                return false;
            }

            try
            {
                Process process = server.Tracker.Process;

                if (!process.HasExited)
                {
                    Debug.WriteLine($"Stopping PID: {process.Id}");

                    process.Kill(true);
                    //process.WaitForExit(5000); // Wait up to 5s for exit confirmation
                }

                process.Dispose();

                server.Tracker.Process = null;
                server.Tracker.ProcessId = null;
                server.Tracker.StartDate = null;

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Stop error: {ex}");
                return false;
            }
        }
    }
}

