using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UnturnedServerUtility.Models
{
    public class ServerCollection
    {
        public ServerTracker Tracker { get; set; }
        public string severName { get; set; }
        public string playerCount { get; set; }
        public string playerMax { get; set; }
    }

    public partial class ServerTracker : ObservableObject
    {
        public string ServerName { get; set; }
        public string ServerExecutable { get; set; }
        public Guid? TrackingId { get; set; }
        public int? ProcessId { get; set; }
        public DateTime? StartDate { get; set; }
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsRunning))] // Tells UI that IsRunning changed too!
        private Process? _process;

        // 3. Keep our helper for Avalonia XAML
        public bool IsRunning => _process != null;
    }
}
