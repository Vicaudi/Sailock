using System.Windows.Controls;
using System.Windows.Input;
using Sailock.ViewModels;

namespace Sailock.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void MasterPasswordVerify_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is DashboardViewModel vm)
                vm.MasterPasswordInput = ((PasswordBox)sender).Password;
        }

        private void MasterPasswordVerify_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && DataContext is DashboardViewModel vm)
                vm.ConfirmMasterPasswordCommand.Execute(null);
        }

        private void EditPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is DashboardViewModel vm && vm.EditingEntry != null)
                vm.EditingEntry.Password = ((PasswordBox)sender).Password;
        }
    }
}