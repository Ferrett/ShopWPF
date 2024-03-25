using System;
using System.Globalization;
using System.Windows.Data;

namespace GameShopAPP.Services.Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    class IsEnabledToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == false ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
