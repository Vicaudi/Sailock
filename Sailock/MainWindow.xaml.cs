using System;
using System.Windows;
using System.Windows.Input;
using Sailock.ViewModels;

namespace Sailock
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VersionText.Text = AppVersion;
        }

        // Registrar cualquier movimiento de ratón o tecla como actividad
        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            (DataContext as MainViewModel)?.RegisterActivity();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            (DataContext as MainViewModel)?.RegisterActivity();
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                BtnMaximize.Content = "□";
            }
            else
            {
                WindowState = WindowState.Maximized;
                BtnMaximize.Content = "❐";
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void VersionText_Click(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainViewModel)?.ShowChangelogCommand.Execute(null);
        }

        public string AppVersion => $"v{Services.VersionService.Current}";
    }
}