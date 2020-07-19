using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MVW_ClassLibrary.Common.Converters
{
    public class LeftShiftDown2ObjectColor : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if(value is bool?)
            {
                bool? typValue = value as bool?;
                if (typValue.HasValue && typValue.Value == true)
                {
                    return Brushes.Red;
                }
            }
            return Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
        #endregion
    }
}