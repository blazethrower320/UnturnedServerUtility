using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UnturnedServerUtility.ViewModels
{
    public partial class ServerSelectionViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Models.ServerCollection> _serverList;

        public ServerSelectionViewModel()
        {
            _serverList = new ObservableCollection<Models.ServerCollection>
            {
                new Models.ServerCollection { severName = "Server 1", playerCount = "10", playerMax = "20" },
                new Models.ServerCollection { severName = "Server 2", playerCount = "5", playerMax = "15" },
                new Models.ServerCollection { severName = "Server 3", playerCount = "8", playerMax = "25" }
            };
        }
    }
}
