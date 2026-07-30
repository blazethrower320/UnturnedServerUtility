using Avalonia.Controls;
using Avalonia.Interactivity;
using UnturnedServerUtility.ViewModels;

namespace UnturnedServerUtility
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel? _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            if (Design.IsDesignMode) return;

            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        private void Sidebar_Dashboard_Click(object? sender, RoutedEventArgs e)
        {
            _viewModel?.ShowDashboardPage();
        }

        private void Sidebar_Servers_Click(object? sender, RoutedEventArgs e)
        {
            _viewModel?.ShowServerSelectionPage();
        }

        private void Sidebar_Settings_Click(object? sender, RoutedEventArgs e)
        {
        }
    }
}