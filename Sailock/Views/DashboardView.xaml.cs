using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sailock.ViewModels;

namespace Sailock.Views
{
    public partial class DashboardView : UserControl
    {
        private DashboardViewModel? _vm;

        public DashboardView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_vm != null)
                _vm.PropertyChanged -= OnViewModelPropertyChanged;

            _vm = DataContext as DashboardViewModel;

            if (_vm != null)
                _vm.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                // Modal Add: limpiar PasswordBox al abrir
                case nameof(DashboardViewModel.IsAddModalOpen):
                    if (_vm?.IsAddModalOpen == true)
                        AddPasswordBox.Clear();
                    break;

                // Modal Edit: poblar PasswordBox con la contraseña real al abrir
                case nameof(DashboardViewModel.IsEditModalOpen):
                    if (_vm?.IsEditModalOpen == true)
                    {
                        var pwd = _vm.EditingEntry?.Password ?? string.Empty;
                        EditPasswordBoxModal.Password = pwd;
                    }
                    else
                    {
                        // Al cerrar el modal Edit (cancel o save), limpiar el PasswordBox
                        EditPasswordBoxModal.Clear();
                    }
                    break;

                // Modal Master Password: limpiar al abrir
                case nameof(DashboardViewModel.IsMasterPasswordModalOpen):
                    if (_vm?.IsMasterPasswordModalOpen == true)
                        MasterPasswordVerify.Clear();
                    break;

                // Sincronizar cuando se alterna visible/oculto
                case nameof(DashboardViewModel.IsPasswordVisible):
                    SyncPasswordBoxes();
                    break;
            }
        }

        private void SyncPasswordBoxes()
        {
            if (_vm == null) return;
            var currentPassword = _vm.EditingEntry?.Password ?? string.Empty;

            if (!_vm.IsPasswordVisible)
            {
                if (_vm.IsAddModalOpen)
                    AddPasswordBox.Password = currentPassword;
                else if (_vm.IsEditModalOpen)
                    EditPasswordBoxModal.Password = currentPassword;
            }
        }

        private void EditPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is not DashboardViewModel vm) return;
            if (vm.EditingEntry == null) return;
            vm.EditingEntry.Password = ((PasswordBox)sender).Password;
        }

        private void MasterPasswordVerify_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is DashboardViewModel vm)
                vm.MasterPasswordInput = MasterPasswordVerify.Password;
        }

        private void MasterPasswordVerify_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is DashboardViewModel vm)
                vm.ConfirmMasterPasswordCommand.Execute(null);
        }
    }
}