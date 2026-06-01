using System.Windows;

namespace Sailock.Services
{
    public static class LocalizationService
    {
        private const string BaseUri = "pack://application:,,,/Localization/";

        public static void ApplyLanguage(string language)
        {
            string fileName = language switch
            {
                "Español" => "Strings.es.xaml",
                _         => "Strings.en.xaml"
            };

            var uri = new System.Uri(BaseUri + fileName, System.UriKind.Absolute);
            var newDict = new ResourceDictionary { Source = uri };

            var resources = Application.Current.Resources.MergedDictionaries;

            // Eliminar el diccionario de localización anterior si existe
            for (int i = resources.Count - 1; i >= 0; i--)
            {
                var source = resources[i].Source?.ToString();
                if (source != null && source.Contains("/Localization/Strings."))
                {
                    resources.RemoveAt(i);
                    break;
                }
            }

            resources.Add(newDict);
        }
    }
}