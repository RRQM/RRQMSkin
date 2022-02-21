using System;
using System.Globalization;
using System.Windows.Data;

namespace RRQM.Emoji.Conver
{
    class ImageConverSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if ((int)value == 0)
                {
                    return 30;
                }
                else
                    return 50;
            }
            return 50;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
