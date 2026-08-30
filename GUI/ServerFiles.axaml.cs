using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class ServerFiles : UserControl
    {
        public ServerFiles()
        {
            InitializeComponent();
        }

        public ServerFiles(ServerCollection server) : this()
        {
            DataContext = new ServerFilesViewModel(server);
        }
        private void StartServer_Button_Click(object? sender, RoutedEventArgs e)
        {
        }

        private void RestartServer_Button_Click(object? sender, RoutedEventArgs e)
        {
        }

        private void StopServer_Button_Click(object? sender, RoutedEventArgs e)
        {
        }

        private void ServerConsole_Files_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ServerCollection server)
            {
                MainWindow.Instance?.NavigateToServerFiles(server);
            }
        }

        private void ServerConsole_Console_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is ServerCollection server)
            {
                MainWindow.Instance?.NavigateToConsole(server);
            }
        }

        private void ServerFile_Button_Click(object? sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.CommandParameter is ServerFilesModel files)
            {
                if (DataContext is ServerFilesViewModel viewModel)
                {
                    if(files.isFolder)
                    {
                        viewModel.ServerURL = Path.Combine(viewModel.ServerURL, files.fileName);
                        viewModel.ServerFiles = ServerManager.GetPathServerFiles(viewModel.ServerURL);
                        return;
                    }
                    MainWindow.Instance?.NavigateToFileEditor(viewModel.SelectedServer, Path.Combine(viewModel.ServerURL, files.fileName));

                }
            }
        }
    }
}