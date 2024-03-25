using System;
using System.Windows.Data;
using System.Windows;
using System.Globalization;

namespace GameShopAPP.Services.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    class StringEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.ToString()) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
