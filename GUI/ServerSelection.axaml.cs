using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnturnedServerUtility.Helpers;
using UnturnedServerUtility.Managers;
using UnturnedServerUtility.Models;
using UnturnedServerUtility.ViewModels;

namespace UnturnedServerUtility.GUI.Styles
{
    public partial class ServerSelection : UserControl
    {
        InstallationManager steamCMD = new InstallationManager();
        public ServerSelection()
        {
            InitializeComponent();
            DataContext = new ServerSelectionViewModel();
        }

        private void CreateServer_Button_Click(object? sender, RoutedEventArgs e)
        {
            // Logic for creating a server
        }

        private void DeleteServer_Button_Click(object? sender, RoutedEventArgs e)
        {
            // Logic for deleting a server
        }

        private void InstallCMD_Button_Click(object? sender, RoutedEventArgs e)
        {
            steamCMD.InitProject();

            bool success = steamCMD.InstallSteamCMD();

            if (success)
            {
                Console.WriteLine("Unturned Dedicated Server installed successfully.");
            }
            else
            {
                Console.WriteLine("Installation failed. Check steamcmd_installation.log.");
            }
        }

        private void UninstallCMD_Button_Click(object? sender, RoutedEventArgs e)
        {
        }

        private void ListExecutables_Button_Click(object? sender, RoutedEventArgs e)
        {
            var executables = ServerManager.getAllServerExecutables();
            foreach (var exe in executables)
            {
                Debug.WriteLine(exe);
            }
        }
        private void StartServer_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ServerCollection server)
            {
                if (server.Tracker.IsRunning)
                {
                    // Notify user the server is already running and cancel start
                    MainWindow.Instance?.ShowNotification(new Notification(
                        "Server already running",
                        "The server is already running.",
                        NotificationType.Error,
                        TimeSpan.FromSeconds(3)));
                    return;
                }

                MainWindow.Instance?.ShowNotification(new Notification(
                    "Server Started",
                    $"You have started {server.severName}.",
                    NotificationType.Success,
                    TimeSpan.FromSeconds(3)));

                ServerManager.startServer(server);
            }

        }
        private void StopServer_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ServerCollection server)
            {
                ServerManager.stopServer(server);
            }
        }
        private void RestartServer_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ServerCollection server)
            {
                ServerManager.restartServer(server);
            }
        }

        private void ServerSelection_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ServerCollection server)
            {
                MainWindow.Instance?.NavigateToConsole(server);
            }
        }
    }
}