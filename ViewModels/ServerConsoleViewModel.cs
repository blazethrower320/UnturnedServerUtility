using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnturnedServerUtility.Managers;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.ViewModels
{
    public partial class ServerConsoleViewModel : ObservableObject
    {
        [ObservableProperty]
        private ServerCollection? selectedServer;

        public ServerConsoleViewModel(ServerCollection? server = null)
        {
            SelectedServer = server;
        }

        public void startConsoleDisplay()
        {

        }

    }
}
