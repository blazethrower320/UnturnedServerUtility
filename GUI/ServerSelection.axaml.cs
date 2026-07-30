using Avalonia.Controls;
using Avalonia.Interactivity;
using UnturnedServerUtility.ViewModels;

namespace UnturnedServerUtility.GUI.Styles
{
    public partial class ServerSelection : UserControl
    {
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
    }
}