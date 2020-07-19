
using MVW_ClassLibrary.Common.DrawModels;
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ClassLibrary.Common.DtoModels.HelperModels;
using MVW_ClassLibrary.Common.Enumerations;
using MVW_ClassLibrary.Common.EventHandlers;
using MVW_ClassLibrary.Common.Shared;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVW_ControlsAndFormsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for Eizo_MonitorPositionOrganisation_UserControl.xaml
    /// </summary
    public partial class MonitorPositionOrganisation_UserControl : UserControl, INotifyPropertyChanged
    {
        #region -SmartWall- property
        private DtoSmartWall _SmartWall;
        public DtoSmartWall SmartWall
        {
            get { return _SmartWall; }
            set
            {
                if (_SmartWall != value)
                {
                    _SmartWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -MonitorRectanglesList- property
        private ObservableCollection<MonitorRectangle> _MonitorRectanglesList;
        public ObservableCollection<MonitorRectangle> MonitorRectanglesList
        {
            get { return _MonitorRectanglesList; }
            set
            {
                if (_MonitorRectanglesList != value)
                {
                    _MonitorRectanglesList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -SelectedMonitorRectangle- property
        private MonitorRectangle _SelectedMonitorRectangle;
        public MonitorRectangle SelectedMonitorRectangle
        {
            get { return _SelectedMonitorRectangle; }
            set
            {
                if (_SelectedMonitorRectangle != value)
                {
                    _SelectedMonitorRectangle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Editable- property
        private bool _Editable;
        public bool Editable
        {
            get { return _Editable; }
            set
            {
                if (_Editable != value)
                {
                    _Editable = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //Helper properties
        #region -AllAspectRatioModelsList- property
        private List<AspectRatioModel> _AllAspectRatioModelsList;
        public List<AspectRatioModel> AllAspectRatioModelsList
        {
            get { return _AllAspectRatioModelsList; }
            set
            {
                if (_AllAspectRatioModelsList != value)
                {
                    _AllAspectRatioModelsList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -AllInchesSizeList- property
        private List<double> _AllInchesSizeList;
        public List<double> AllInchesSizeList
        {
            get { return _AllInchesSizeList; }
            set
            {
                if (_AllInchesSizeList != value)
                {
                    _AllInchesSizeList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -lShiftDown- property
        private bool? _lShiftDown = false;
        public bool? lShiftDown
        {
            get { return _lShiftDown; }
            set
            {
                if (_lShiftDown != value)
                {
                    _lShiftDown = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //MultiLanguageImplementation
        #region -MultiLanguageImp- property
        private MultiLanguageImplementationModel _MultiLanguageImp;
        public MultiLanguageImplementationModel MultiLanguageImp
        {
            get { return _MultiLanguageImp; }
            set
            {
                if (_MultiLanguageImp != value)
                {
                    if (_MultiLanguageImp != null)
                    {
                        _MultiLanguageImp.ResourceUpdated -= _MultiLanguageImp_ResourceUpdated;
                    }
                    _MultiLanguageImp = value;
                    _MultiLanguageImp.ResourceUpdated += _MultiLanguageImp_ResourceUpdated;
                    NotifyPropertyChanged();
                }
            }
        }

        private void _MultiLanguageImp_ResourceUpdated(object sender, EventArgs e)
        {
            NotifyPropertyChanged("MultiLanguageImp");
        }
        #endregion


        public MonitorPositionOrganisation_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeControl();
        }

        public MonitorPositionOrganisation_UserControl(DtoSmartWall inSmartWall, bool isEditable = false)
        {
            SmartWall = inSmartWall;
            Editable = isEditable;
            InitializeComponent();

            InitializeControl();
            SetUIGridByEditable(Editable);

            GenerateMonitorRectanglesList();
        }

        #region Set & Release Smart Wall functions
        public void SetSmartWall(DtoSmartWall inSmartWall, bool isEditable = false)
        {
            if (SmartWall != null)
            {
                ReleaseOldSmartWall();
            }
            SelectedMonitorRectangle = null;

            SmartWall = inSmartWall;
            Editable = isEditable;
            InitializeControl();
            SetUIGridByEditable(Editable);

            GenerateMonitorRectanglesList();
        }

        public void ReleaseOldSmartWall()
        {
            if (MonitorRectanglesList != null)
            {
                foreach (var monitor in MonitorRectanglesList)
                {
                    monitor.EizoActionCalled -= TmpMonitor_EizoActionCalled;
                    monitor.RemoveMonitorRectangleAndReleaseAllResources();
                }
                MonitorRectanglesList.Clear();
            }
        }
        #endregion


        //Initialize
        #region Initialize Control
        private void InitializeControl()
        {
            if (MonitorRectanglesList == null)
            {
                MonitorRectanglesList = new ObservableCollection<MonitorRectangle>();
            }
            //Combo boxes item sources!!
            if (AllAspectRatioModelsList == null)
            {
                AllAspectRatioModelsList = AspectRatioModel.AllOptionsList;
            }
            if (AllInchesSizeList == null)
            {
                AllInchesSizeList = new List<double>() { 7, 12.1, 15, 15.6, 17, 18.5, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                    41, 42, 43, 44, 45, 46, 47, 48, 49, 50 ,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65};
            }
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }
        }
        #endregion

        //Work with MonitorRectangles list functions
        #region Generate Monitors Rectangles Dictonary
        public void GenerateMonitorRectanglesList()
        {
            if (MonitorRectanglesList == null)
            {
                MonitorRectanglesList = new ObservableCollection<MonitorRectangle>();
            }
            if (MonitorRectanglesList != null && SmartWall != null && SmartWall.MonitorsList != null && SmartWall.MonitorsList.Count > 0)
            {
                foreach (var monitor in SmartWall.MonitorsList)
                {
                    MonitorRectangle tmpMonitor = new MonitorRectangle(canvas_Main, SmartWall, monitor);
                    MonitorRectanglesList.Add(tmpMonitor);
                    tmpMonitor.EizoActionCalled += TmpMonitor_EizoActionCalled;
                }
            }
        }
        #endregion

        //Work with UI functions
        #region Set UI by Editable
        private void SetUIGridByEditable(bool editable)
        {

        }
        #endregion

        //Work with Rectangles aligment TEST functions   //po potrebi uraditi nove
        #region Look For Rectangle Side Aligment function
        private void LookForRectangleSideAligment(MonitorRectangle inDraggingRectangle, bool compareWithFarAwayRectangles = false)
        {
            var responseValue = 3;


            if (inDraggingRectangle.draggingStarted)
            {
                if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 1)
                {
                    foreach (var monitor in MonitorRectanglesList)
                    {
                        if (monitor != inDraggingRectangle && (monitor.VirtualAroundRegionRect.IntersectsWith(inDraggingRectangle.TmpRectangle) || compareWithFarAwayRectangles))
                        {
                            if (Math.Abs(monitor.Left - inDraggingRectangle.Left) < responseValue) //Aligment stranica
                            {
                                inDraggingRectangle.SetLeft(monitor.Left);
                            }
                            else if (Math.Abs(monitor.Right - inDraggingRectangle.Right) < responseValue)
                            {
                                inDraggingRectangle.SetRight(monitor.Right);
                            }
                            else if (Math.Abs(monitor.Bottom - inDraggingRectangle.Bottom) < responseValue)
                            {
                                inDraggingRectangle.SetBottom(monitor.Bottom);
                            }
                            else if (Math.Abs(monitor.Top - inDraggingRectangle.Top) < responseValue)
                            {
                                inDraggingRectangle.SetTop(monitor.Top);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Look For Rect Distance Aligment function
        private void LokForRectangleDistanceAligment(MonitorRectangle inDraggingRectangle, MonitorRectangle inClosestRectangle) //GIVE CLOSEST MONITOR RECTANGLE FUNCTION WAY
        {
            var distanceValue = 6;
            if (inDraggingRectangle != null && inClosestRectangle != null)
            {
                if (Math.Abs(inDraggingRectangle.Right - inClosestRectangle.Left) < distanceValue && inDraggingRectangle.Right <= inClosestRectangle.Left)
                {
                    inDraggingRectangle.SetRight(inClosestRectangle.Left - distanceValue);
                }
                else if (Math.Abs(inDraggingRectangle.Left - inClosestRectangle.Right) < distanceValue && inDraggingRectangle.Left >= inClosestRectangle.Right)
                {
                    inDraggingRectangle.SetLeft(inClosestRectangle.Right + distanceValue);
                }
                else if (Math.Abs(inDraggingRectangle.Bottom - inClosestRectangle.Top) < distanceValue && inDraggingRectangle.Bottom <= inClosestRectangle.Top)
                {
                    inDraggingRectangle.SetBottom(inClosestRectangle.Top - distanceValue);
                }
                else if (Math.Abs(inDraggingRectangle.Top - inClosestRectangle.Bottom) < distanceValue && inDraggingRectangle.Top >= inClosestRectangle.Bottom)
                {
                    inDraggingRectangle.SetTop(inClosestRectangle.Bottom + distanceValue);
                }
            }
        }

        private void LokForRectangleDistanceAligment(MonitorRectangle inDraggingRectangle)  //LOOK FOR INTERSECT WITH VIRTUAL AROUNDING RECT
        {
            var distanceValue = 8;
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 1 && inDraggingRectangle.draggingStarted)
            {
                foreach (var monitor in MonitorRectanglesList)
                {
                    if (monitor != inDraggingRectangle && monitor.VirtualAroundRegionRect.IntersectsWith(inDraggingRectangle.TmpRectangle) && !monitor.TmpRectangle.IntersectsWith(inDraggingRectangle.TmpRectangle))
                    {
                        if (Math.Abs(inDraggingRectangle.Right - monitor.Left) < distanceValue && inDraggingRectangle.Right <= monitor.Left)
                        {
                            inDraggingRectangle.SetRight(monitor.Left - distanceValue);
                        }
                        else if (Math.Abs(inDraggingRectangle.Left - monitor.Right) < distanceValue && inDraggingRectangle.Left >= monitor.Right)
                        {
                            inDraggingRectangle.SetLeft(monitor.Right + distanceValue);
                        }
                        else if (Math.Abs(inDraggingRectangle.Bottom - monitor.Top) < distanceValue && inDraggingRectangle.Bottom <= monitor.Top)
                        {
                            inDraggingRectangle.SetBottom(monitor.Top - distanceValue);
                        }
                        else if (Math.Abs(inDraggingRectangle.Top - monitor.Bottom) < distanceValue && inDraggingRectangle.Top >= monitor.Bottom)
                        {
                            inDraggingRectangle.SetTop(monitor.Bottom + distanceValue);
                        }
                    }
                }
            }
        }
        #endregion

        #region Find Closest Monitor function
        private MonitorRectangle FindClosestMonitor(MonitorRectangle inSelectedMonitorRectangle)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 1 && inSelectedMonitorRectangle != null)
            {
                MonitorRectangle toRetMonitor = MonitorRectanglesList[0];
                if (toRetMonitor == inSelectedMonitorRectangle)
                {
                    toRetMonitor = MonitorRectanglesList[1];
                }
                double currentLowestDistance = Calculations.GetDistanceBetween2Points(inSelectedMonitorRectangle.CenterPointOfRectangle, toRetMonitor.CenterPointOfRectangle);
                foreach (var monitor in MonitorRectanglesList)
                {
                    if (monitor != inSelectedMonitorRectangle && monitor != toRetMonitor)
                    {
                        if (currentLowestDistance > Calculations.GetDistanceBetween2Points(inSelectedMonitorRectangle.CenterPointOfRectangle, monitor.CenterPointOfRectangle))
                        {
                            currentLowestDistance = Calculations.GetDistanceBetween2Points(inSelectedMonitorRectangle.CenterPointOfRectangle, monitor.CenterPointOfRectangle);
                            toRetMonitor = monitor;
                        }
                    }
                }
                return toRetMonitor;
            }
            return null;
        }
        #endregion


        //Events
        #region Increase & Decrease & Deselect buttons Events
        private void button_IncreaseInchesRatio_Click(object sender, RoutedEventArgs e) //Increase inches ratio SMARTWALL
        {
            if (SmartWall != null)
            {
                SmartWall.IncreaseInchesRatio();
                NotifyThatSavingsNeed();
            }
        }

        private void button_DecreaseInchesRatio_Click(object sender, RoutedEventArgs e) //Decrease inches ratio SMARTWALL
        {
            if (SmartWall != null)
            {
                SmartWall.DecreaseInchesRatio();
                NotifyThatSavingsNeed();
            }
        }

        private void button_deselectClick_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMonitorRectangle != null)
            {
                SelectedMonitorRectangle = null;
            }
        }
        #endregion

        #region Movement buttons Events
        private void button_midLeft_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.Left);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_topRight_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.UpRight);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_topCenter_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.Up);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_topLeft_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.UpLeft);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_midRight_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.Right);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_bottomLeft_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.DownLeft);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_bottomCenter_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.Down);
                }
                NotifyThatSavingsNeed();
            }
        }

        private void button_bottomRight_Click(object sender, RoutedEventArgs e)
        {
            if (MonitorRectanglesList != null && MonitorRectanglesList.Count > 0)
            {
                foreach (var item in MonitorRectanglesList)
                {
                    item.MoveRectangle(EMoveRectangleDirection.DownRight);
                }
                NotifyThatSavingsNeed();
            }
        }

        #endregion

        #region MonitorRectangles Events
        private void TmpMonitor_EizoActionCalled(object sender, MVW_UserActionEventArgument<MonitorRectangle, EMVWActions> e)
        {
            if (e.ActionType == EMVWActions.MonitorRectangleSelected)
            {
                if (sender.GetType() == typeof(MonitorRectangle))
                {
                    var typedMonitorRect = sender as MonitorRectangle;
                    if (typedMonitorRect != null)
                    {
                        SelectedMonitorRectangle = typedMonitorRect;
                    }
                }
            }
            else if (e.ActionType == EMVWActions.MonitorRectangleMoved)
            {
                if (SelectedMonitorRectangle != null)
                {
                    if (lShiftDown.HasValue && lShiftDown.Value == false)
                    {
                        LookForRectangleSideAligment(SelectedMonitorRectangle);
                        //LokForRectangleDistanceAligment(SelectedMonitorRectangle, FindClosestMonitor(SelectedMonitorRectangle));
                        LokForRectangleDistanceAligment(SelectedMonitorRectangle);
                    }
                    NotifyThatSavingsNeed();
                }
            }
            else if (e.ActionType == EMVWActions.MonitorRectangleEdited)
            {
                if (SelectedMonitorRectangle != null)
                {
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion


        #region Keybord events
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift && e.IsRepeat)
            {
                lShiftDown = true;
            }

            if (e.Key == Key.Left)
            {
                button_midLeft_Click(sender, e);
            }
            else if (e.Key == Key.Right)
            {
                button_midRight_Click(sender, e);
            }
            else if (e.Key == Key.Up)
            {
                button_topCenter_Click(sender, e);
            }
            else if (e.Key == Key.Down)
            {
                button_bottomCenter_Click(sender, e);
            }
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            lShiftDown = false;
        }
        #endregion


        //Savings Need Event
        #region Savings Need Event Handler & Notification
        public event EventHandler SavingsNeed;
        private void NotifyThatSavingsNeed()
        {
            SavingsNeed?.Invoke(this, new EventArgs());
        }
        #endregion


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
