using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnturnedServerUtility.Managers;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.ViewModels
{
    public partial class ServerSelectionViewModel : ObservableObject
    {
        public List<ServerCollection> ServerList => ServerManager.Servers;

        public ServerSelectionViewModel()
        {
        }
    }
}
