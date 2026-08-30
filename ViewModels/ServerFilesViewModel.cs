using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnturnedServerUtility.Managers;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.ViewModels
{
    public partial class ServerFilesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ServerCollection? selectedServer;
        [ObservableProperty]
        private List<ServerFilesModel> serverFiles = new List<ServerFilesModel>();
        [ObservableProperty]
        private string serverURL = "";
        private string serverBaseURL = Path.Combine(ServerManager.serverDirectory, "servers");
        public ServerFilesViewModel(ServerCollection server = null)
        {
            SelectedServer = server;
            serverURL = Path.Combine(ServerManager.serverDirectory, "servers", server.Tracker.ServerName);
            serverFiles = ServerManager.GetPathServerFiles(serverURL);
        }
    }
}
