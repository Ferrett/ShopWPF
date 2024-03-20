using System;
using System.Globalization;
using System.Windows.Data;

namespace GameShopAPP.Services.Converters
{
    [ValueConversion(typeof(object), typeof(bool))]
    class SearchTextToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Search..."? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
