using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.TextMate;
using System;
using System.IO;
using TextMateSharp.Grammars;
using UnturnedServerUtility.Managers;
using UnturnedServerUtility.Models;
using UnturnedServerUtility.ViewModels;

namespace UnturnedServerUtility.GUI.Styles
{
    public partial class FileEditor : UserControl
    {
        private TextMate.Installation? _textMate;
        private RegistryOptions? _registryOptions;
        public FileEditor()
        {
            InitializeComponent();

            _registryOptions = new RegistryOptions(ThemeName.DarkPlus);
            _textMate = Editor.InstallTextMate(_registryOptions);
            this.DataContextChanged += FileEditor_DataContextChanged;
        }

        private void FileEditor_DataContextChanged(object? sender, EventArgs e)
        {
            if (this.DataContext is not FileEditorViewModel viewModel)
            {
                return;
            }
            Editor.Document = viewModel.XmlDocument;
            string extension = Path.GetExtension(viewModel.FilePath);
            var language = _registryOptions.GetLanguageByExtension(extension);
            if (language != null)
            {
                _textMate.SetGrammar(_registryOptions.GetScopeByLanguageId(language.Id));
                return;
            }
            _textMate.SetGrammar(_registryOptions.GetScopeByLanguageId("source.json"));
        }
    
        private void StartServer_Button_Click(object? sender, RoutedEventArgs e) { }
        private void RestartServer_Button_Click(object? sender, RoutedEventArgs e) { }
        private void StopServer_Button_Click(object? sender, RoutedEventArgs e) { }

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
            if (sender is Button button && button.CommandParameter is ServerFilesModel files)
            {
                if (DataContext is ServerFilesViewModel viewModel)
                {
                    viewModel.ServerURL = Path.Combine(viewModel.ServerURL, files.fileName);
                    viewModel.ServerFiles = ServerManager.GetPathServerFiles(viewModel.ServerURL);
                }
            }
        }
    }
}