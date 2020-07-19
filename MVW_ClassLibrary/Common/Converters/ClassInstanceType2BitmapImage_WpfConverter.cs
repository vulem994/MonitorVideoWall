using MVW_ClassLibrary.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MVW_ClassLibrary.Common.Converters
{
    public class ClassInstanceType2BitmapImage_WpfConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {

            if (value.GetType() == typeof(ELogicalChildrenClassInstanceType))
            {
                Bitmap tmpBitmap = new Bitmap(10, 10);
                var castedValue = (ELogicalChildrenClassInstanceType)value;

                if (castedValue == ELogicalChildrenClassInstanceType.SmartWall)
                {
                    tmpBitmap = Properties.Resources.screenwall;
                }
                else if (castedValue == ELogicalChildrenClassInstanceType.Preset)
                {
                    tmpBitmap = Properties.Resources.presets;
                }
                else if (castedValue == ELogicalChildrenClassInstanceType.PresetInstance)
                {
                    tmpBitmap = Properties.Resources.multiple_presets;
                }
                else if (castedValue == ELogicalChildrenClassInstanceType.Monitor)
                {
                    tmpBitmap = Properties.Resources.monitor;
                }
                else if (castedValue == ELogicalChildrenClassInstanceType.MonitorInstance)
                {
                    tmpBitmap = Properties.Resources.multiple_monitor;
                }
                else if (castedValue == ELogicalChildrenClassInstanceType.PresetSettings)
                {
                    tmpBitmap = Properties.Resources.presets;
                }
                MemoryStream ms = new MemoryStream();
                tmpBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.EndInit();

                return image;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ELogicalChildrenClassInstanceType.None;
        }

        #endregion
    }
}