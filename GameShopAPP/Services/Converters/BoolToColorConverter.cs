using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;

namespace GameShopAPP.Services.Converters
{
    [ValueConversion(typeof(object), typeof(Brushes))]
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
