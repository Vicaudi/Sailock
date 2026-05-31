using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;
using System.Collections.Generic;

namespace Sailock.ViewModels
{
    public class ChangelogViewModel : ViewModelBase
    {
        public string CurrentVersion { get; } = $"v{VersionService.Current}";
        public List<ChangelogEntry> Entries { get; } = ChangelogService.GetEntries();
        public System.Action OnClose { get; set; }

        public Helpers.RelayCommand CloseCommand { get; }

        public ChangelogViewModel()
        {
            CloseCommand = new Helpers.RelayCommand(_ => OnClose?.Invoke());
        }
    }
}