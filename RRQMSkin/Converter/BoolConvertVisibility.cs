using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RRQMSkin.Converter
{
    class BoolConvertVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //gender
            if (value != null)
            {
                if ((bool)value == true)
                    return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
