using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SecurityProcessReader.Converters
{
    class BooleanToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsBool = (bool)value;
            return valueAsBool ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsVisibility = (Visibility) value;
            return valueAsVisibility == Visibility.Visible;
        }
    }
}
