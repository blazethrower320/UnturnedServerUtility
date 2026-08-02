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
    public partial class ServerConsole : UserControl
    {
        InstallationManager steamCMD = new InstallationManager();
        public ServerConsole()
        {
            InitializeComponent();
            DataContext = new ServerConsoleViewModel();
        }



       
    }
}