using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using UnturnedServerUtility.Models;
using UnturnedServerUtility.ViewModels;

namespace UnturnedServerUtility
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel? _viewModel;
        private WindowNotificationManager _notificationManager;

        public static MainWindow? Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            if (Design.IsDesignMode) return;

            Instance = this;

            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            _notificationManager = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.BottomRight,
                MaxItems = 3
            };
        }

        // Helper for other parts of the UI to show notifications
        public void ShowNotification(Notification notification)
        {
            _notificationManager?.Show(notification);
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            base.OnClosing(e);



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

        public void NavigateToConsole(ServerCollection server)
        {
            _viewModel?.ShowConsolePage(server);
        }

        public void NavigateToServerFiles(ServerCollection server)
        {
            _viewModel?.ShowServerFilesPage(server);
        }

        public void NavigateToFileEditor(ServerCollection server, string path)
        {
            _viewModel?.ShowFileEditorPage(server, path);
        }
    }
}