using MVW_ClassLibrary.Common.Adorners;
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ClassLibrary.Common.Enumerations;
using MVW_ClassLibrary.Common.EventHandlers;
using MVW_ClassLibrary.Common.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MVW_ClassLibrary.Common.DrawModels
{
    public class MonitorRectangle : INotifyPropertyChanged
    {
        #region -RectanglePath- property
        private Path _RectanglePath;
        public Path RectanglePath
        {
            get { return _RectanglePath; }
            set
            {
                if (_RectanglePath != value)
                {
                    _RectanglePath = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -MonitorTextPath- property
        private Path _MonitorTextPath;
        public Path MonitorTextPath
        {
            get { return _MonitorTextPath; }
            set
            {
                if (_MonitorTextPath != value)
                {
                    _MonitorTextPath = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -MonitorNameText- property
        private FormattedText _MonitorNameText;
        public FormattedText MonitorNameText
        {
            get { return _MonitorNameText; }
            set
            {
                if (_MonitorNameText != value)
                {
                    _MonitorNameText = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -RectangleGeometry- property
        private RectangleGeometry _RectangleGeometry;
        public RectangleGeometry RectangleGeometry
        {
            get { return _RectangleGeometry; }
            set
            {
                if (_RectangleGeometry != value)
                {
                    _RectangleGeometry = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        #region -ConnectedMonitor- property
        private DtoMonitor _ConnectedMonitor;
        public DtoMonitor ConnectedMonitor
        {
            get { return _ConnectedMonitor; }
            set
            {
                if (_ConnectedMonitor != value)
                {
                    if (_ConnectedMonitor != null)
                    {
                        _ConnectedMonitor.PropertyChanged -= ConnectedMonitor_PropertyChanged;
                    }
                    _ConnectedMonitor = value;
                    _ConnectedMonitor.PropertyChanged += ConnectedMonitor_PropertyChanged;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ConnectedSmartWall- property
        private DtoSmartWall _ConnectedSmartWall;
        public DtoSmartWall ConnectedSmartWall
        {
            get { return _ConnectedSmartWall; }
            set
            {
                if (_ConnectedSmartWall != value)
                {
                    if (_ConnectedSmartWall != null)
                    {
                        _ConnectedSmartWall.PropertyChanged -= ConnectedSmartWall_PropertyChanged;
                    }
                    _ConnectedSmartWall = value;
                    _ConnectedSmartWall.PropertyChanged += ConnectedSmartWall_PropertyChanged;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ParentPanel- property
        private Panel _ParentPanel;
        public Panel ParentPanel
        {
            get { return _ParentPanel; }
            set
            {
                if (_ParentPanel != value)
                {
                    _ParentPanel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //Current values
        #region -tmpRectStartPointPixels- property
        private Point _tmpRectStartPointPixels;
        public Point tmpRectStartPointPixels
        {
            get { return _tmpRectStartPointPixels; }
            set
            {
                if (_tmpRectStartPointPixels != value)
                {
                    _tmpRectStartPointPixels = value;
                    CalculateRectangleSideValuesVirtualRectAndCenterPoint();
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -tmpRectSizePixels- property
        private Size _tmpRectSizePixels;
        public Size tmpRectSizePixels
        {
            get { return _tmpRectSizePixels; }
            set
            {
                if (_tmpRectSizePixels != value)
                {
                    _tmpRectSizePixels = value;
                    CalculateRectangleSideValuesVirtualRectAndCenterPoint();
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -currentPixelInchUnit- property
        private double _currentPixelInchUnit;
        public double currentPixelInchUnit
        {
            get { return _currentPixelInchUnit; }
            set
            {
                if (_currentPixelInchUnit != value)
                {
                    _currentPixelInchUnit = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -CenterPointOfRectangle- property
        private Point _CenterPointOfRectangle;
        public Point CenterPointOfRectangle
        {
            get { return _CenterPointOfRectangle; }
            set
            {
                if (_CenterPointOfRectangle != value)
                {
                    _CenterPointOfRectangle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        #region -Top- property
        private double _Top;
        public double Top
        {
            get { return _Top; }
            set
            {
                if (_Top != value)
                {
                    _Top = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        #region -Left- property
        private double _Left;
        public double Left
        {
            get { return _Left; }
            set
            {
                if (_Left != value)
                {
                    _Left = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        #region -Right- property
        private double _Right;
        public double Right
        {
            get { return _Right; }
            set
            {
                if (_Right != value)
                {
                    _Right = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        #region -Bottom- property
        private double _Bottom;
        public double Bottom
        {
            get { return _Bottom; }
            set
            {
                if (_Bottom != value)
                {
                    _Bottom = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //Check intersection of Rectangle properties
        #region -VirtualAroundRegionRect- property
        private Rect _VirtualAroundRegionRect;
        public Rect VirtualAroundRegionRect
        {
            get { return _VirtualAroundRegionRect; }
            set
            {
                if (_VirtualAroundRegionRect != value)
                {
                    _VirtualAroundRegionRect = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        #region -TmpRectangle- property
        public Rect TmpRectangle
        {
            get
            {
                return new Rect(tmpRectStartPointPixels,tmpRectSizePixels);
            }

        }
        #endregion


        //Adorner
        #region -MonitorRectangleAdornerLayer- property
        private AdornerLayer _MonitorRectangleAdornerLayer;
        public AdornerLayer MonitorRectangleAdornerLayer
        {
            get { return _MonitorRectangleAdornerLayer; }
            set
            {
                if (_MonitorRectangleAdornerLayer != value)
                {
                    _MonitorRectangleAdornerLayer = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -NameStringAdorner- property
        private MonitorRectangleName_Adorner _NameStringAdorner;
        public MonitorRectangleName_Adorner NameStringAdorner
        {
            get { return _NameStringAdorner; }
            set
            {
                if (_NameStringAdorner != value)
                {
                    _NameStringAdorner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //other
        #region Dragging properties
        #region -draggingStarted- property
        private bool _draggingStarted;
        public bool draggingStarted
        {
            get { return _draggingStarted; }
            set
            {
                if (_draggingStarted != value)
                {
                    _draggingStarted = value;
                    if (RectanglePath != null)
                    {
                        if (_draggingStarted)
                        {
                            RectanglePath.Stroke = rectangleBorderIfDraggingColor;
                        }
                        else
                        {
                            RectanglePath.Stroke = rectangleBorderColor;
                        }
                    }
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        Vector vectorBetweenMouseDownAndStartPoint;
        #endregion

        #region Colors & Thickness
        SolidColorBrush rectangleFillColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
        SolidColorBrush rectangleBorderColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        SolidColorBrush rectangleBorderIfDraggingColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        SolidColorBrush textColor = Brushes.Black;

        double rectangleBorderThickness = 4;
        double textFontSize = 16;
        #endregion



        public MonitorRectangle(Panel inParentPanel, DtoSmartWall inConnectedSmartWall, DtoMonitor inConnectedMonitor)
        {
            ParentPanel = inParentPanel;
            ConnectedMonitor = inConnectedMonitor;
            if (ConnectedMonitor != null)
            {
                MonitorNameText = new FormattedText(ConnectedMonitor?.Name, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), textFontSize, textColor);
            }
            ConnectedSmartWall = inConnectedSmartWall;

            InitializeClass();
            InitializeParentPanelEvents();

            GenerateMonitorRectangle();
            AddMeToParentChildren();

            InitializeAdorner();
        }

        //Initialize
        #region Initialize Class
        private void InitializeClass()
        {
            if (RectangleGeometry == null)
            {
                RectangleGeometry = new RectangleGeometry();
            }
            if (RectanglePath == null)
            {
                RectanglePath = RectanglePath = new Path()
                {
                    Data = RectangleGeometry,
                    Fill = rectangleFillColor,
                    //Stroke = rectangleBorderColor,
                    //StrokeThickness = rectangleBorderThickness,
                };
                RectanglePath.MouseLeftButtonDown += RectanglePath_LeftMouseDown;
            }

            //Text
            if (MonitorTextPath == null)
            {
                MonitorTextPath = new Path()
                {
                    IsHitTestVisible = false,
                    Fill = textColor,
                };
            }
        }

        #endregion

        #region Initialize Parent Panel events
        private void InitializeParentPanelEvents()
        {
            if (ParentPanel != null)
            {
                ParentPanel.MouseMove += ParentPanel_MouseMove;
                ParentPanel.MouseUp += ParentPanel_MouseUp;
                //ParentPanel.MouseLeave += ParentPanel_MouseLeave;
            }
        }
        #endregion

        #region Initialize Adorner (commented)
        private void InitializeAdorner()
        {
            //if (RectanglePath != null && ConnectedMonitor != null)
            //{
            //    NameStringAdorner = new MonitorRectangleName_Adorner(RectanglePath, ConnectedMonitor); //adorner


            //    MonitorRectangleAdornerLayer = MyGetAdornerLayer(RectanglePath);
            //    if (MonitorRectangleAdornerLayer != null && NameStringAdorner != null)
            //    {
            //        MonitorRectangleAdornerLayer.Add(NameStringAdorner);
            //    }
            //}
        }
        #endregion
        //REMOVE MONITOR RECTANGLE AND RELEASE ALL RESOURCES
        #region Remove ME functions
        public void RemoveMonitorRectangleAndReleaseAllResources()
        {
            RemoveMeFromParentChildren();
            if (ParentPanel != null)
            {
                ParentPanel.MouseMove -= ParentPanel_MouseMove;
                ParentPanel.MouseUp -= ParentPanel_MouseUp;
                //ParentPanel.MouseLeave -= ParentPanel_MouseLeave;
            }
            if (RectanglePath != null)
            {
                RectanglePath.MouseLeftButtonDown -= RectanglePath_LeftMouseDown;
                RectanglePath.Data = null;
                RectanglePath = null;
            }
            if (MonitorTextPath != null)
            {
                MonitorTextPath.Data = null;
                MonitorTextPath = null;
            }
        }
        #endregion


        //Work with Rectangle & Monitor Text functions
        #region Generate Monitor Rectangle function
        private void GenerateMonitorRectangle()
        {
            if (ConnectedMonitor != null && ConnectedSmartWall != null && RectanglePath != null && MonitorTextPath != null)
            {
                const double oneInchPixelsDefault = 96;
                currentPixelInchUnit = ConnectedSmartWall.InchesRatio * oneInchPixelsDefault; // 96px = 1inch default

                double startPointXPixel = ConnectedMonitor.StartPointInchesX * currentPixelInchUnit;
                double startPointYPixel = ConnectedMonitor.StartPointInchesY * currentPixelInchUnit;
                double diagonalSizePixel = ConnectedMonitor.InchesDiagonalSize * currentPixelInchUnit;

                tmpRectStartPointPixels = new Point(startPointXPixel, startPointYPixel);
                tmpRectSizePixels = Calculations.GetRectangleSizeByAspectRationAndDiagonalInPixels(ConnectedMonitor?.AspectRatio, diagonalSizePixel);

                Rect tmpRect = new Rect(tmpRectStartPointPixels, tmpRectSizePixels);
                RectangleGeometry.Rect = tmpRect;// = new RectangleGeometry(tmpRect);
                RectanglePath.Data = RectangleGeometry;


                //text
                RefreshMonitorTextPathTextGeometry(tmpRectStartPointPixels, tmpRectSizePixels);
            }
        }
        #endregion

        #region Update rectangle position

        private void UpdateRectanglePositionMouseMove(Point inPoint)
        {
            UpdateRectanglePosition(inPoint - vectorBetweenMouseDownAndStartPoint);
        }

        private void UpdateRectanglePosition(Point inPoint)
        {
            if (RectangleGeometry != null)
            {
                tmpRectStartPointPixels = inPoint;
                RectangleGeometry.Rect = new Rect(tmpRectStartPointPixels, tmpRectSizePixels);
                UpdateStartPointInMonitor();
                //text
                RefreshMonitorTextPathTextGeometry(tmpRectStartPointPixels, tmpRectSizePixels);
            }
        }
        #endregion

        #region Set TOP BOTTOM LEFT & RIGHT functions
        public void SetLeft(double value)
        {
            double moveVector = value - Left;
            EMoveRectangleDirection dir = EMoveRectangleDirection.Right;
            if (value < Left)
            {
                moveVector = Left - value;
                dir = EMoveRectangleDirection.Left;
            }
            MoveRectangle(dir, moveVector);
        }

        public void SetTop(double value)
        {
            double moveVector = value - Top;
            EMoveRectangleDirection dir = EMoveRectangleDirection.Down;
            if (value < Top)
            {
                moveVector = Top - value;
                dir = EMoveRectangleDirection.Up;
            }
            MoveRectangle(dir, moveVector);
        }

        public void SetRight(double value)
        {
            double moveVector = value - Right;
            EMoveRectangleDirection dir = EMoveRectangleDirection.Right;
            if (value < Right)
            {
                moveVector = Right - value;
                dir = EMoveRectangleDirection.Left;
            }
            MoveRectangle(dir, moveVector);
        }

        public void SetBottom(double value)
        {
            double moveVector = value - Bottom;
            EMoveRectangleDirection dir = EMoveRectangleDirection.Down;
            if (value < Bottom)
            {
                moveVector = Bottom - value;
                dir = EMoveRectangleDirection.Up;
            }
            MoveRectangle(dir, moveVector);
        }
        #endregion

        #region Calculate Rectangle Side Values (Top,Bottom,Left & Right)
        private void CalculateRectangleSideValuesVirtualRectAndCenterPoint()
        {
            Top = tmpRectStartPointPixels.Y;
            Left = tmpRectStartPointPixels.X;
            Bottom = Top + tmpRectSizePixels.Height;
            Right = Left + tmpRectSizePixels.Width;

            Point virtualRectStartPoint = new Point(Left - tmpRectSizePixels.Width / 4, Top - tmpRectSizePixels.Height / 4);
            Size virtualSize = new Size(tmpRectSizePixels.Width * 1.5, tmpRectSizePixels.Height * 1.5);
            VirtualAroundRegionRect = new Rect(virtualRectStartPoint, virtualSize);  

            CenterPointOfRectangle = new Point((Left + Right) / 2, (Top + Bottom) / 2);
        }
        #endregion
        //text
        #region Refresh Text Path function
        private void RefreshMonitorTextPathTextGeometry(Point inMonitorStartPoint, Size inMonitorSize)
        {
            if (MonitorTextPath == null)
            {
                MonitorTextPath = new Path() { Fill = textColor };
            }
            if (MonitorNameText == null)
            {
                MonitorNameText = new FormattedText(ConnectedMonitor?.Name, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), textFontSize, textColor);
            }
            if (MonitorTextPath != null && MonitorNameText != null)
            {
                double tmpX = inMonitorStartPoint.X + inMonitorSize.Width / 2 - MonitorNameText.Width / 2;
                double tmpY = inMonitorStartPoint.Y + inMonitorSize.Height / 2 - MonitorNameText.Height / 2;

                Geometry geo = MonitorNameText.BuildGeometry(new Point(tmpX, tmpY));
                MonitorTextPath.Data = geo;
            }
        }
        #endregion

        #region Move Rectangle with given value
        public void MoveRectangle(EMoveRectangleDirection inDirection, double inValue = 3)
        {
            Point newPoint = new Point(0, 0);
            if (RectanglePath != null)
            {
                var geometry = RectanglePath.Data as RectangleGeometry;
                if (geometry != null)
                {
                    newPoint = new Point(geometry.Rect.X, geometry.Rect.Y);
                }
            }
            if (inDirection == EMoveRectangleDirection.Up)
            {
                newPoint.Y -= inValue;
            }
            else if (inDirection == EMoveRectangleDirection.Down)
            {
                newPoint.Y += inValue;
            }
            else if (inDirection == EMoveRectangleDirection.Left)
            {
                newPoint.X -= inValue;
            }
            else if (inDirection == EMoveRectangleDirection.Right)
            {
                newPoint.X += inValue;
            }
            else if (inDirection == EMoveRectangleDirection.UpLeft)
            {
                newPoint.X -= inValue;
                newPoint.Y -= inValue;
            }
            else if (inDirection == EMoveRectangleDirection.UpRight)
            {
                newPoint.X += inValue;
                newPoint.Y -= inValue;
            }
            else if (inDirection == EMoveRectangleDirection.DownLeft)
            {
                newPoint.X -= inValue;
                newPoint.Y += inValue;
            }
            else if (inDirection == EMoveRectangleDirection.DownRight)
            {
                newPoint.X += inValue;
                newPoint.Y += inValue;
            }
            UpdateRectanglePosition(newPoint);
        }
        #endregion


        //Work with Parent Panel functions
        #region Add & Remove Paths from Parent Panel Children functions
        private void AddMeToParentChildren()
        {
            if (ParentPanel != null && RectanglePath != null && !ParentPanel.Children.Contains(RectanglePath))
            {
                ParentPanel.Children.Add(RectanglePath);
            }
            if (ParentPanel != null && MonitorTextPath != null && !ParentPanel.Children.Contains(MonitorTextPath))
            {
                ParentPanel.Children.Add(MonitorTextPath);
            }
        }

        private void RemoveMeFromParentChildren()
        {
            if (ParentPanel != null && RectanglePath != null && ParentPanel.Children.Contains(RectanglePath))
            {
                ParentPanel.Children.Remove(RectanglePath);
            }
            if (ParentPanel != null && MonitorTextPath != null && ParentPanel.Children.Contains(MonitorTextPath))
            {
                ParentPanel.Children.Remove(MonitorTextPath);
            }
        }
        #endregion

        //Work with Connected Monitor functions
        #region Update Start Point in Monitor after moving function
        private void UpdateStartPointInMonitor()
        {
            if (ConnectedMonitor != null)
            {
                ConnectedMonitor.StartPointInchesX = tmpRectStartPointPixels.X / currentPixelInchUnit;
                ConnectedMonitor.StartPointInchesY = tmpRectStartPointPixels.Y / currentPixelInchUnit;
            }
        }
        #endregion

        //Dragging Rectangle work functions
        #region CalculateVectorBetweenMouseDownAndStartPoint
        private void CalculateVectorBetweenMouseDownAndStartPoint(Point inMouseDownPoint)
        {
            if (RectanglePath != null)
            {
                var tmpRectangleGeometry = RectanglePath.Data as RectangleGeometry;
                if (tmpRectangleGeometry != null)
                {
                    var tmpPoint = new Point(tmpRectangleGeometry.Rect.X, tmpRectangleGeometry.Rect.Y);
                    vectorBetweenMouseDownAndStartPoint = inMouseDownPoint - tmpPoint;
                }
            }
        }
        #endregion


        //Events
        #region RectanglePath events
        private void RectanglePath_LeftMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) //RectanglePath MOUSE DOWN
        {
            CalculateVectorBetweenMouseDownAndStartPoint(e.GetPosition(ParentPanel));
            draggingStarted = true;
            NotifyEizoActionCalled(this, this, EMVWActions.MonitorRectangleSelected);
        }
        #endregion

        #region ParentPanel events
        private void ParentPanel_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) //Parent Panel MOUSE UP
        {
            draggingStarted = false;
        }

        private void ParentPanel_MouseMove(object sender, System.Windows.Input.MouseEventArgs e) //Parent Panel MOUSE MOVE
        {
            if (draggingStarted)
            {
                UpdateRectanglePositionMouseMove(e.GetPosition(ParentPanel));
                NotifyEizoActionCalled(this, this, EMVWActions.MonitorRectangleMoved);
            }
        }

        //private void ParentPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) //Parent Panel MOUSE LEAVE
        //{
        //    if (draggingStarted && !ParentPanel.IsMouseOver && RectanglePath != null && !RectanglePath.IsMouseOver)
        //    {
        //        draggingStarted = false;
        //    }
        //}
        #endregion

        #region Connected SmartWall events
        private void ConnectedSmartWall_PropertyChanged(object sender, PropertyChangedEventArgs e) // Property Changed
        {
            if (e.PropertyName == "InchesRatio")
            {
                GenerateMonitorRectangle();
            }
        }
        #endregion

        #region Connected Monitor events
        private void ConnectedMonitor_PropertyChanged(object sender, PropertyChangedEventArgs e) //Propery changed
        {
            if (e.PropertyName == "InchesDiagonalSize")
            {
                GenerateMonitorRectangle();
                NotifyEizoActionCalled(this, this, EMVWActions.MonitorRectangleEdited);
            }
            else if (e.PropertyName == "AspectRatio")
            {
                GenerateMonitorRectangle();
                NotifyEizoActionCalled(this, this, EMVWActions.MonitorRectangleEdited);
            }
        }
        #endregion


        //Static functions
        #region My Get AdornerLayer static class
        static AdornerLayer MyGetAdornerLayer(FrameworkElement subject)
        {
            AdornerLayer layer = null;
            do
            {
                if ((layer = AdornerLayer.GetAdornerLayer(subject)) != null)
                    break;
            } while ((subject = subject.Parent as FrameworkElement) != null);
            return layer;
        }
        #endregion


        //Actions Called
        #region EizoAction Called Event
        public event EventHandler<MVW_UserActionEventArgument<MonitorRectangle, EMVWActions>> EizoActionCalled;

        public void NotifyEizoActionCalled(object inObjectCaller, MonitorRectangle inSelectedObjectInstance, EMVWActions inActionType)
        {
            EizoActionCalled?.Invoke(this, new MVW_UserActionEventArgument<MonitorRectangle, EMVWActions>(inObjectCaller, inSelectedObjectInstance, inActionType));
        }
        #endregion


        //Property Changer
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
