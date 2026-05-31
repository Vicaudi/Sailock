using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly StorageService _storage;
        private readonly string _masterPassword;
        private readonly AppData _appData;

        private ObservableCollection<PasswordEntry> _allEntries;

        private ObservableCollection<PasswordEntry> _entries;
        public ObservableCollection<PasswordEntry> Entries
        {
            get => _entries;
            set => SetProperty(ref _entries, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { SetProperty(ref _searchText, value); ApplyFilter(); }
        }

        private PasswordEntry _selectedEntry;
        public PasswordEntry SelectedEntry
        {
            get => _selectedEntry;
            set => SetProperty(ref _selectedEntry, value);
        }

        private bool _isAddModalOpen;
        public bool IsAddModalOpen
        {
            get => _isAddModalOpen;
            set => SetProperty(ref _isAddModalOpen, value);
        }

        private bool _isEditModalOpen;
        public bool IsEditModalOpen
        {
            get => _isEditModalOpen;
            set => SetProperty(ref _isEditModalOpen, value);
        }

        private PasswordEntry _editingEntry;
        public PasswordEntry EditingEntry
        {
            get => _editingEntry;
            set => SetProperty(ref _editingEntry, value);
        }

        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set => SetProperty(ref _isPasswordVisible, value);
        }

        private bool _hasEditChanges;
        public bool HasEditChanges
        {
            get => _hasEditChanges;
            set => SetProperty(ref _hasEditChanges, value);
        }

        // --- Verificación master password ---
        private bool _isMasterPasswordModalOpen;
        public bool IsMasterPasswordModalOpen
        {
            get => _isMasterPasswordModalOpen;
            set => SetProperty(ref _isMasterPasswordModalOpen, value);
        }

        private string _masterPasswordInput;
        public string MasterPasswordInput
        {
            get => _masterPasswordInput;
            set => SetProperty(ref _masterPasswordInput, value);
        }

        private bool _masterPasswordFailed;
        public bool MasterPasswordFailed
        {
            get => _masterPasswordFailed;
            set => SetProperty(ref _masterPasswordFailed, value);
        }

        private PasswordEntry _pendingEditEntry;

        // --- Comandos ---
        public ICommand OpenAddModalCommand { get; }
        public ICommand SaveNewEntryCommand { get; }
        public ICommand CancelAddCommand { get; }
        public ICommand OpenEditModalCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand DeleteEntryCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand CopyPasswordCommand { get; }
        public ICommand ConfirmMasterPasswordCommand { get; }
        public ICommand CancelMasterPasswordCommand { get; }
        public ICommand TogglePasswordVisibilityCommand { get; }

        public DashboardViewModel(AppData appData, StorageService storage, string masterPassword)
        {
            _appData = appData;
            _storage = storage;
            _masterPassword = masterPassword;

            _allEntries = new ObservableCollection<PasswordEntry>(_appData.Entries);
            Entries = new ObservableCollection<PasswordEntry>(_allEntries);

            OpenAddModalCommand = new RelayCommand(_ => OpenAddModal());
            SaveNewEntryCommand = new RelayCommand(_ => SaveNewEntry());
            CancelAddCommand = new RelayCommand(_ => CancelAdd());
            OpenEditModalCommand = new RelayCommand(e => OpenEditModal(e as PasswordEntry));
            SaveEditCommand = new RelayCommand(_ => SaveEdit());
            DeleteEntryCommand = new RelayCommand(_ => DeleteEntry());
            CancelEditCommand = new RelayCommand(_ => CancelEdit());
            CopyPasswordCommand = new RelayCommand(e => CopyPassword(e as PasswordEntry));
            ConfirmMasterPasswordCommand = new RelayCommand(_ => ConfirmMasterPassword());
            CancelMasterPasswordCommand = new RelayCommand(_ => CancelMasterPassword());
            TogglePasswordVisibilityCommand = new RelayCommand(_ =>
                IsPasswordVisible = !IsPasswordVisible);
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Entries = new ObservableCollection<PasswordEntry>(_allEntries);
                return;
            }

            var term = SearchText.ToLower();
            var filtered = _allEntries.Where(e =>
                (e.Title != null && e.Title.ToLower().Contains(term)) ||
                (e.Username != null && e.Username.ToLower().Contains(term)) ||
                (e.Email != null && e.Email.ToLower().Contains(term)) ||
                (e.Category != null && e.Category.ToLower().Contains(term))
            );

            Entries = new ObservableCollection<PasswordEntry>(filtered);
        }

        private void OpenAddModal()
        {
            EditingEntry = new PasswordEntry();
            IsPasswordVisible = false;
            HasEditChanges = false;
            IsAddModalOpen = true;
        }

        private void CancelAdd()
        {
            IsAddModalOpen = false;
            EditingEntry = null;
            IsPasswordVisible = false;
        }

        private void SaveNewEntry()
        {
            if (string.IsNullOrWhiteSpace(EditingEntry?.Title)) return;

            _allEntries.Add(EditingEntry);
            _appData.Entries.Add(EditingEntry);
            Persist();
            ApplyFilter();
            IsAddModalOpen = false;
            EditingEntry = null;
        }

        private void OpenEditModal(PasswordEntry entry)
        {
            if (entry == null) return;

            _pendingEditEntry = entry;
            MasterPasswordInput = null;
            MasterPasswordFailed = false;
            IsPasswordVisible = false;
            IsMasterPasswordModalOpen = true;
        }

        private void ConfirmMasterPassword()
        {
            if (MasterPasswordInput != _masterPassword)
            {
                MasterPasswordFailed = true;
                return;
            }

            IsMasterPasswordModalOpen = false;
            MasterPasswordFailed = false;
            HasEditChanges = false;

            EditingEntry = new PasswordEntry
            {
                Id = _pendingEditEntry.Id,
                Category = _pendingEditEntry.Category,
                Title = _pendingEditEntry.Title,
                Email = _pendingEditEntry.Email,
                Username = _pendingEditEntry.Username,
                Password = _pendingEditEntry.Password,
                Note = _pendingEditEntry.Note
            };

            // Detectar cambios en tiempo real
            EditingEntry.PropertyChanged += (s, e) => CheckEditChanges();

            SelectedEntry = _pendingEditEntry;
            IsEditModalOpen = true;
        }

        private void CheckEditChanges()
        {
            if (_pendingEditEntry == null || EditingEntry == null)
            {
                HasEditChanges = false;
                return;
            }

            HasEditChanges =
                EditingEntry.Title != _pendingEditEntry.Title ||
                EditingEntry.Category != _pendingEditEntry.Category ||
                EditingEntry.Email != _pendingEditEntry.Email ||
                EditingEntry.Username != _pendingEditEntry.Username ||
                EditingEntry.Password != _pendingEditEntry.Password ||
                EditingEntry.Note != _pendingEditEntry.Note;
        }

        private void CancelMasterPassword()
        {
            IsMasterPasswordModalOpen = false;
            MasterPasswordInput = null;
            MasterPasswordFailed = false;
            _pendingEditEntry = null;
        }

        private void CancelEdit()
        {
            IsEditModalOpen = false;
            EditingEntry = null;
            HasEditChanges = false;
            IsPasswordVisible = false;
            _pendingEditEntry = null;
        }

        private void SaveEdit()
        {
            if (SelectedEntry == null || EditingEntry == null) return;

            SelectedEntry.Category = EditingEntry.Category;
            SelectedEntry.Title = EditingEntry.Title;
            SelectedEntry.Email = EditingEntry.Email;
            SelectedEntry.Username = EditingEntry.Username;
            SelectedEntry.Password = EditingEntry.Password;
            SelectedEntry.Note = EditingEntry.Note;

            var index = _allEntries.IndexOf(SelectedEntry);
            if (index >= 0)
            {
                _allEntries.RemoveAt(index);
                _allEntries.Insert(index, SelectedEntry);
            }

            var dataIndex = _appData.Entries.FindIndex(e => e.Id == SelectedEntry.Id);
            if (dataIndex >= 0)
                _appData.Entries[dataIndex] = SelectedEntry;

            Persist();
            ApplyFilter();
            IsEditModalOpen = false;
            EditingEntry = null;
            HasEditChanges = false;
            _pendingEditEntry = null;
        }

        private void DeleteEntry()
        {
            if (SelectedEntry == null) return;

            _allEntries.Remove(SelectedEntry);
            _appData.Entries.RemoveAll(e => e.Id == SelectedEntry.Id);
            Persist();
            ApplyFilter();
            IsEditModalOpen = false;
            EditingEntry = null;
            HasEditChanges = false;
            _pendingEditEntry = null;
        }

        private void CopyPassword(PasswordEntry entry)
        {
            if (entry?.Password != null)
                System.Windows.Clipboard.SetText(entry.Password);
        }

        private void Persist() => _storage.Save(_appData, _masterPassword);
    }
}