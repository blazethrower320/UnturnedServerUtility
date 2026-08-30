using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UnturnedServerUtility.Models;

namespace UnturnedServerUtility.ViewModels
{
    public partial class FileEditorViewModel : ObservableObject
    {
        [ObservableProperty]
        private ServerCollection? selectedServer;
        [ObservableProperty]
        private string filePath = "";
        [ObservableProperty]
        private TextDocument? xmlDocument;

        public FileEditorViewModel(ServerCollection? server = null, string? filePath = null)
        {
            SelectedServer = server;
            FilePath = filePath ?? "";

            xmlDocument = new TextDocument(File.ReadAllText(filePath));
        }
    }
}
