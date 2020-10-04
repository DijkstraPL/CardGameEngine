using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace CardGame_Client.Converters
{
    public class ReverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
                return !flag;
            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
                return !flag;
            throw new ArgumentException(nameof(value));
        }
    }
}
