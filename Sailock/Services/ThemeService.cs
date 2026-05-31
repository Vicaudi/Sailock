using System.Windows;

namespace Sailock.Services
{
    public static class ThemeService
    {
        public static void ApplyContrast(bool highContrast)
        {
            var resources = Application.Current.Resources;

            if (highContrast)
            {
                // Bordes marcados, separadores visibles
                resources["ContrastBorderBrush"] = ColorFromHex("#2A2A2A");
                resources["ContrastBorderThickness"] = new Thickness(1);
                resources["ContrastSeparatorBrush"] = ColorFromHex("#1A1A1A");
                resources["ContrastShadowOpacity"] = 0.0;
            }
            else
            {
                // Bordes sutiles, separadores como sombra
                resources["ContrastBorderBrush"] = ColorFromHex("#141414");
                resources["ContrastBorderThickness"] = new Thickness(0.5);
                resources["ContrastSeparatorBrush"] = ColorFromHex("#0A0A0A");
                resources["ContrastShadowOpacity"] = 0.0;
            }
        }

        private static System.Windows.Media.SolidColorBrush ColorFromHex(string hex)
        {
            var color = (System.Windows.Media.Color)
                System.Windows.Media.ColorConverter.ConvertFromString(hex);
            return new System.Windows.Media.SolidColorBrush(color);
        }

        public static void ApplyTextSize(string size)
        {
            double scale = size switch
            {
                "Small" => 0.85,
                "Large" => 1.20,
                _       => 1.0   // Default
            };

            Application.Current.Resources["GlobalScale"] = scale;
        }

        public static void ApplyTheme(bool isDark)
        {
            var res = Application.Current.Resources;

            if (isDark)
            {
                res["AppBackground"] = BrushFromHex("#000000");
                res["AppSurface"] = BrushFromHex("#0A0A0A");
                res["AppSurface2"] = BrushFromHex("#111111");
                res["AppForeground"] = BrushFromHex("#FFFFFF");
                res["AppForegroundDim"] = BrushFromHex("#666666");
                res["AppAccent"] = BrushFromHex("#00FF41");
                res["AppInputBackground"] = BrushFromHex("#0D0D0D");
                res["SidebarLogoSource"] = "pack://application:,,,/Resources/sailock_logo.png";
            }
            else
            {
                res["AppBackground"] = BrushFromHex("#F0F0F0");
                res["AppSurface"] = BrushFromHex("#E8E8E8");
                res["AppSurface2"] = BrushFromHex("#FFFFFF");
                res["AppForeground"] = BrushFromHex("#000000");
                res["AppForegroundDim"] = BrushFromHex("#555555");
                res["AppAccent"] = BrushFromHex("#007A1F");
                res["AppInputBackground"] = BrushFromHex("#FFFFFF");
                res["SidebarLogoSource"] = "pack://application:,,,/Resources/sailock_logo_dark.png";
            }
        }

        private static System.Windows.Media.SolidColorBrush BrushFromHex(string hex)
        {
            var color = (System.Windows.Media.Color)
                System.Windows.Media.ColorConverter.ConvertFromString(hex);
            return new System.Windows.Media.SolidColorBrush(color);
        }
    }
}