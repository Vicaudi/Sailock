using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;
using System.Windows;
using System.Windows.Input;

namespace Sailock.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly StorageService _storage = new StorageService();
        private readonly AutoLockService _autoLock = new AutoLockService();
        private string _masterPassword;
        private AppData _appData;
        private DashboardViewModel _dashboardVM;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                SetProperty(ref _currentView, value);
                OnPropertyChanged(nameof(SidebarVisibility));
                // Registrar actividad al cambiar de vista
                _autoLock.Reset();
            }
        }

        public Visibility SidebarVisibility =>
            CurrentView is LoginViewModel ? Visibility.Collapsed : Visibility.Visible;

        private string _logoSource = "pack://application:,,,/Resources/sailock_logo.png";
        public string LogoSource
        {
            get => _logoSource;
            set => SetProperty(ref _logoSource, value);
        }

        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateGeneratorCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ShowChangelogCommand { get; }

        public MainViewModel()
        {
            NavigateDashboardCommand = new RelayCommand(_ => ShowDashboard());
            NavigateGeneratorCommand = new RelayCommand(_ => ShowGenerator());
            NavigateSettingsCommand = new RelayCommand(_ => ShowSettings());
            LogoutCommand = new RelayCommand(_ => Logout());
            ShowLogin();
            ShowChangelogCommand = new RelayCommand(_ => ShowChangelog());
        }

        private void ShowLogin()
        {
            _autoLock.Disable();
            _masterPassword = null;
            _appData = null;
            _dashboardVM = null;

            var loginVM = new LoginViewModel(_storage);
            loginVM.OnLoginSuccess = OnLoginSuccess;
            loginVM.ShowChangelogCommand = ShowChangelogCommand;
            CurrentView = loginVM;
        }

        private void OnLoginSuccess(string password, AppData data)
        {
            _masterPassword = password;
            _appData = data;

            ThemeService.ApplyTheme(_appData.Settings.IsDarkTheme);
            ThemeService.ApplyContrast(_appData.Settings.IsHighContrast);
            ThemeService.ApplyTextSize(_appData.Settings.TextSize);

            LogoSource = _appData.Settings.IsDarkTheme
                ? "pack://application:,,,/Resources/sailock_logo.png"
                : "pack://application:,,,/Resources/sailock_logo_dark.png";

            // Activar auto-lock si está configurado
            if (_appData.Settings.AutoLockEnabled)
                _autoLock.Enable(Logout);

            ShowDashboard();
        }

        private void ShowDashboard()
        {
            _dashboardVM ??= new DashboardViewModel(_appData, _storage, _masterPassword);
            _dashboardVM.IsAddModalOpen = false;
            _dashboardVM.IsEditModalOpen = false;
            _dashboardVM.IsMasterPasswordModalOpen = false;
            CurrentView = _dashboardVM;
        }

        private void ShowGenerator()
        {
            CurrentView = new GeneratorViewModel();
        }

        private void ShowSettings()
        {
            var settingsVM = new SettingsViewModel(_appData, _storage, _masterPassword);

            settingsVM.OnDataImported = () => _dashboardVM = null;

            settingsVM.OnOpen2FASetup = setupVM =>
            {
                var previousSettings = CurrentView;
                setupVM.OnSetupComplete = () =>
                {
                    settingsVM.Is2FAEnabled = true;
                    settingsVM.StatusMessage = "2FA activado correctamente.";
                    CurrentView = previousSettings;
                };
                setupVM.OnCancelled = () => CurrentView = previousSettings;
                CurrentView = setupVM;
            };

            settingsVM.OnThemeChanged = isDark =>
            {
                LogoSource = isDark
                    ? "pack://application:,,,/Resources/sailock_logo.png"
                    : "pack://application:,,,/Resources/sailock_logo_dark.png";
            };

            // Cuando el usuario cambia Auto-Lock en Settings, aplicarlo en tiempo real
            settingsVM.OnAutoLockChanged = enabled =>
            {
                if (enabled)
                    _autoLock.Enable(Logout);
                else
                    _autoLock.Disable();
            };

            CurrentView = settingsVM;
        }

        /// <summary>
        /// Llamar desde la View en cada interacción del usuario (mouse, teclado).
        /// </summary>
        public void RegisterActivity() => _autoLock.Reset();

        private void Logout()
        {
            _dashboardVM = null;
            ShowLogin();
        }

        private void ShowChangelog()
        {
            var changelogVM = new ChangelogViewModel();
            var previousView = CurrentView;
            changelogVM.OnClose = () => CurrentView = previousView;
            CurrentView = changelogVM;
        }
    }
}