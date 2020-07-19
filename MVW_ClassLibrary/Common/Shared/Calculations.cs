
using MVW_ClassLibrary.Common.DtoModels.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVW_ClassLibrary.Common.Shared
{
    public class Calculations
    {

        public static Size GetRectangleSizeByAspectRationAndDiagonalInPixels(AspectRatioModel inAspectRatio, double inDiagonalInPixel)
        {
            Size toRetSize = new Size();
            if (inAspectRatio != null && inDiagonalInPixel > 0)
            {
                double calculationUnit = Math.Sqrt(Math.Pow(inDiagonalInPixel, 2) / (Math.Pow(inAspectRatio.WidthR, 2) + Math.Pow(inAspectRatio.HeightR, 2)));
                double width = calculationUnit * inAspectRatio.WidthR;
                double height = calculationUnit * inAspectRatio.HeightR;
                toRetSize = new Size(width, height);
            }
            return toRetSize;
        }

        public static double GetDistanceBetween2Points(Point inPoint1, Point inPoint2)
        {
            return Math.Sqrt(Math.Pow(inPoint1.X - inPoint2.X, 2) + Math.Pow(inPoint1.Y - inPoint2.Y, 2));
        }


        public static Point FindPointByStartPointDistanceAndAngle(Point inStartPoint, double inDistance, double inAngle)
        {
            double angleRadians = DegreesToRadians(inAngle);
            double x = inStartPoint.X + Math.Cos(angleRadians) * inDistance;
            double y = inStartPoint.Y + Math.Sin(angleRadians) * inDistance;
            return new Point((int)x, (int)y);
        }

        public static double DegreesToRadians(double degrees)
        {
            const double degToRadFactor = Math.PI / 180;
            return degrees * degToRadFactor;
        }

        public static double RadiansToDegrees(double radians)
        {
            const double radToDegFactor = 180 / Math.PI;
            return radians * radToDegFactor;
        }
    }
}
