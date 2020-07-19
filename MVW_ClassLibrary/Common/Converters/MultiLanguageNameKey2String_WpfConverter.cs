
using MVW_MultiLanguageImplementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVW_ClassLibrary.Common.Converters
{
    public class MultiLanguageNameKey2String_WpfConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(MultiLanguageImplementationModel))
            {
                var typedMLIModel = value as MultiLanguageImplementationModel;
                if (parameter != null && parameter is string)
                {
                    var typedPar = parameter as string;
                    return typedMLIModel.GetStringFromResources(typedPar);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
