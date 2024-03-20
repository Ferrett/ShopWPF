using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GameShopAPP.Services.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || bool.Parse(parameter.ToString()!) == true)
                return (bool)value == true ? Visibility.Visible : Visibility.Collapsed;

            return (bool)value == false ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
