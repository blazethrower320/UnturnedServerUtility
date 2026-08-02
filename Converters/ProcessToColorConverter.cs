using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace UnturnedServerUtility.Converters
{
    public class ProcessToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Green - Online
            // Red - Offline
            return value != null ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
