﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GameShopAPP.Services.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility))]
    class AchievementsAmountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value > 0? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
