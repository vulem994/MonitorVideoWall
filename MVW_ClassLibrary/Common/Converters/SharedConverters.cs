using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MVW_ClassLibrary.Common.Converters
{
    public class SharedConverters
    {
        public static BitmapImage ConvertBitmap2BitmapImage(Bitmap inImage)
        {
            MemoryStream ms = new MemoryStream();
            if (inImage != null)
            {
                inImage.Save(ms, ImageFormat.Png);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.EndInit();

                return image;
            }
            return null;
        }
    }
}
