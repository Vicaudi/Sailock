using System.Collections.Generic;
using System.Windows.Controls;

namespace Sailock.Views
{
    public partial class SettingsView : UserControl
    {
        public static List<string> TextSizeOptions { get; } =
            new List<string> { "Small", "Default", "Large" };

        public static List<string> LanguageOptions { get; } =
            new List<string> { "English", "Español" };

        public string SelectedTextSize { get; set; } = "Default";

        public SettingsView()
        {
            InitializeComponent();
        }
    }
}