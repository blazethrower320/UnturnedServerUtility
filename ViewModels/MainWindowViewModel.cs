using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? _currentView;

        public MainWindowViewModel()
        {
            CurrentView = new ServerSelectionViewModel();
        }


        public void ShowServerSelectionPage()
        {
            CurrentView = new ServerSelectionViewModel();
        }
        public void ShowDashboardPage()
        {
            CurrentView = new MainWindowViewModel();
        }

        public void ShowConsolePage(ServerCollection? selectedServer = null)
        {
            CurrentView = new ServerConsoleViewModel(selectedServer);
        }
    }
}
