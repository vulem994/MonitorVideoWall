using MVW_ClassLibrary.Common.DtoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MVW_ClassLibrary.Common.Adorners
{
    public class MonitorRectangleName_Adorner : Adorner, INotifyPropertyChanged
    {
        #region -ConnectedMonitor- property
        private DtoMonitor _ConnectedMonitor;
        public DtoMonitor ConnectedMonitor
        {
            get { return _ConnectedMonitor; }
            set
            {
                if (_ConnectedMonitor != value)
                {
                    _ConnectedMonitor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Colors
        SolidColorBrush textColor = Brushes.Red;
        #endregion


        public MonitorRectangleName_Adorner(UIElement adornedElement, DtoMonitor inConnectedMonitor = null)
        : base(adornedElement)
        {
            ConnectedMonitor = inConnectedMonitor;
            this.IsHitTestVisible = false;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            if (ConnectedMonitor != null)
            {
                FormattedText textRender = new FormattedText(ConnectedMonitor?.Name, new System.Globalization.CultureInfo(1), FlowDirection.LeftToRight, new Typeface("s"), 10, textColor);

                var typedRectPath = AdornedElement as Path;
                if (typedRectPath != null)
                {
                    var typedRectGeometry = typedRectPath.Data as RectangleGeometry;
                    if (typedRectGeometry != null)
                    {
                        var positionX = typedRectGeometry.Rect.X + typedRectGeometry.Rect.Width / 2;
                        var positionY = typedRectGeometry.Rect.Y + typedRectGeometry.Rect.Height / 2;
                        drawingContext.DrawText(textRender, new Point(positionX, positionY));
                    }
                };
            }
        }


        //PropertyChanger
        #region INotifyPropertyChange implementation
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
