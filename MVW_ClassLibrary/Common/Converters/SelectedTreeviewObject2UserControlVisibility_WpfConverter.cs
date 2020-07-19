using MVW_ClassLibrary.Common.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MVW_ClassLibrary.Common.Converters
{
    public class SelectedTreeviewObject2UserControlVisibility_WpfConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter != null && parameter is string)
            {
                var stringParameter = parameter as string;
                var type = value.GetType();
                if (type == typeof(DtoSmartWall))
                {
                    if (stringParameter == "SmartWall")
                    {
                        return Visibility.Visible;
                    }
                }
                else if (type == typeof(DtoMonitor))
                {
                    if (stringParameter == "Monitor")
                    {
                        return Visibility.Visible;
                    }
                }
                else if (type == typeof(DtoSmartWall))
                {
                    if (stringParameter == "Preset")
                    {
                        return Visibility.Visible;
                    }
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
