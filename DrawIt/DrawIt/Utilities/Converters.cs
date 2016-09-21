using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DrawIt
{
    public class ColorConverter : IValueConverter
    {

        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                return Color.FromHex(value as string);
            }
            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
