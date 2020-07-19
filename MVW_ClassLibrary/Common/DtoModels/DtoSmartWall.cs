using MVW_ClassLibrary.Common.Converters;
using MVW_ClassLibrary.Common.DtoModels.CommonModels;
using MVW_ClassLibrary.Common.DtoModels.HelperModels;
using MVW_ClassLibrary.Common.Enumerations;
using MVW_ClassLibrary.Common.EventHandlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVW_ClassLibrary.Common.DtoModels
{
    public class DtoSmartWall : ALogicalChildrenClass, INotifyPropertyChanged
    {
        #region -Name- property
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("LogicalName");
                }
            }
        }
        #endregion

        #region -Description- property
        private String _Description;
        public String Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -MonitorsList- property
        private ObservableCollection<DtoMonitor> _MonitorsList;
        public ObservableCollection<DtoMonitor> MonitorsList
        {
            get { return _MonitorsList; }
            set
            {
                if (_MonitorsList != value)
                {
                    _MonitorsList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -PresetsList- property
        private ObservableCollection<DtoPreset> _PresetsList;
        public ObservableCollection<DtoPreset> PresetsList
        {
            get { return _PresetsList; }
            set
            {
                if (_PresetsList != value)
                {
                    _PresetsList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion



        //Layout Test models //NON SAVINGS YET
        #region -InchesRatio- property
        private Double _InchesRatio;
        public Double InchesRatio //Odnos velicine layout-a i rectangle-a -> koliko je puta veci layout
        {
            get { return _InchesRatio; }
            set
            {
                if (_InchesRatio != value)
                {


                    _InchesRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -AspectRatio- property
        private AspectRatioModel _AspectRatio;
        public AspectRatioModel AspectRatio
        {
            get { return _AspectRatio; }
            set
            {
                if (_AspectRatio != value)
                {
                    _AspectRatio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //Non saving props
        #region Context Menu
        [JsonIgnore]
        bool isContextMenuInitialized = false;
        [JsonIgnore]
        public ContextMenu smartWallContextMenu = new ContextMenu();
        [JsonIgnore]
        public MenuItem miAddMonitor = new MenuItem()
        {
            Header = "Add Monitor",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.add_monitor_b), }
        };
        [JsonIgnore]
        public MenuItem miAddPreset = new MenuItem()
        {
            Header = "Add Preset",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.add_presets_b), }
        };
        [JsonIgnore]
        public MenuItem miRemoveSmartWall = new MenuItem()
        {
            Header = "Remove Smartwall",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.trash_screenwall_b), }
        };
        [JsonIgnore]
        public MenuItem miEditSmartWall = new MenuItem()
        {
            Header = "Edit Smartwall",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.settings), }
        };
        [JsonIgnore]
        public MenuItem miEditSmartWallLayout = new MenuItem()
        {
            Header = "Monitor Positions",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.layout), }
        };
        #endregion

        public DtoSmartWall()
        {
            InitializeClass();
            InitializeContextMenu();
        }

        public DtoSmartWall(string inName, string inDescription = null)
        {
            Name = inName;
            Description = inDescription;
            InitializeClass();
            InitializeContextMenu();
        }

        //Initialize
        #region Initialize Class
        private void InitializeClass()
        {
            if (MonitorsList == null)
            {
                MonitorsList = new ObservableCollection<DtoMonitor>();
            }
            if (PresetsList == null)
            {
                PresetsList = new ObservableCollection<DtoPreset>();
            }
            if (AspectRatio == null)
            {
                AspectRatio = AspectRatioModel.Ar1x1Model; //Default AspectRatio of SmartWall is 1:1
            }
            InchesRatio = 0.05; //default value
        }
        #endregion

        #region Initialize Context Menu  & Context Menu Events
        private void InitializeContextMenu()
        {
            miAddMonitor.Click += MiAddMonitor_Click;
            miAddPreset.Click += MiAddPreset_Click;
            miRemoveSmartWall.Click += MiRemoveSmartWall_Click;
            miEditSmartWall.Click += MiEditSmartWall_Click;
            miEditSmartWallLayout.Click += MiEditSmartWallLayout_Click;

            smartWallContextMenu.Items.Add(miAddMonitor);
            smartWallContextMenu.Items.Add(miAddPreset);
            smartWallContextMenu.Items.Add(new Separator());
            smartWallContextMenu.Items.Add(miEditSmartWall);
            smartWallContextMenu.Items.Add(miEditSmartWallLayout);
            smartWallContextMenu.Items.Add(miRemoveSmartWall);
            isContextMenuInitialized = true;
        }

        private void MiEditSmartWallLayout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.EditSmartWallLayout);
        }

        private void MiEditSmartWall_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.EditSmartWall);
        }

        private void MiRemoveSmartWall_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.RemoveSmartWall);
        }

        private void MiAddPreset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.AddPreset);
        }

        private void MiAddMonitor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.AddMonitor);
        }

        #endregion

        //Generate on load functions
        #region Generate all Presets and Monitors
        public void GenerateAllPresetsAndMonitors()
        {
            if (MonitorsList != null && MonitorsList.Count > 0) //Monitors
            {
                foreach (var item in MonitorsList)
                {
                    item.SetParentSmartWall(this);
                    item.GeneratePresetSettingsList();
                    item.RefreshPresetsListByParentSmartWall(this?.PresetsList);
                    item.EizoActionCalled += InMonitor_EizoActionCalled;
                }
            }
            if (PresetsList != null && PresetsList.Count > 0) // Presets
            {
                foreach (var item in PresetsList)
                {
                    item.SetParentSmartWall(this);
                    item.EizoActionCalled += InPreset_EizoActionCalled;

                }
            }
        }
        #endregion


        //Work With monitors and presets Lists functions
        #region Add & Remove Monitor functions
        public bool AddMonitor(DtoMonitor inMonitor)
        {
            if (MonitorsList == null)
            {
                MonitorsList = new ObservableCollection<DtoMonitor>();
            }
            if (MonitorsList != null && !MonitorsList.Contains(inMonitor))
            {
                inMonitor.ParentSmartWall = this;
                inMonitor.RefreshPresetsListByParentSmartWall(this?.PresetsList);
                MonitorsList.Add(inMonitor);
                inMonitor.EizoActionCalled += InMonitor_EizoActionCalled;
                return true;
            }
            return false;
        }

        public bool RemoveMonitor(DtoMonitor inMonitor)
        {
            if (MonitorsList != null && MonitorsList.Contains(inMonitor))
            {
                MonitorsList.Remove(inMonitor);
                inMonitor.EizoActionCalled -= InMonitor_EizoActionCalled;
                return true;
            }
            return false;
        }
        #endregion

        #region Add & Remove Preset functions
        public bool AddPreset(DtoPreset inPreset)
        {
            if (PresetsList == null)
            {
                PresetsList = new ObservableCollection<DtoPreset>();
            }
            if (PresetsList != null && !PresetsList.Contains(inPreset))
            {
                inPreset.ParentSmartWall = this;
                PresetsList.Add(inPreset);
                inPreset.EizoActionCalled += InPreset_EizoActionCalled;
                return true;
            }
            return false;
        }


        public bool RemovePreset(DtoPreset inPreset)
        {
            if (PresetsList != null && PresetsList.Contains(inPreset))
            {
                PresetsList.Remove(inPreset);
                inPreset.EizoActionCalled -= InPreset_EizoActionCalled;
                return true;
            }
            return false;
        }
        #endregion


        //Logical childredn properties get
        #region Logical name
        [JsonIgnore]
        public override string LogicalName
        {
            get
            {
                return Name;
            }
        }
        #endregion
        #region Logical type
        [JsonIgnore]
        public override ELogicalChildrenClassInstanceType Type
        {
            get
            {
                return ELogicalChildrenClassInstanceType.SmartWall;
            }
        }
        #endregion
        #region Logical context menu
        [JsonIgnore]
        public override ContextMenu LogicalContextMenu
        {
            get
            {
                if (isContextMenuInitialized)
                {
                    return smartWallContextMenu;
                }
                else
                {
                    InitializeContextMenu();
                    return null;
                }
            }
        }
        #endregion
        #region Logical children
        [JsonIgnore]
        private ObservableCollection<ALogicalChildrenClass> _LogicalChildren;
        [JsonIgnore]
        public override ObservableCollection<ALogicalChildrenClass> LogicalChildren
        {
            get
            {
                if (_LogicalChildren == null)
                {
                    _LogicalChildren = new ObservableCollection<ALogicalChildrenClass>()
                    {

                        new LogicalChildrenClassInstance("Presets",this,ELogicalChildrenClassInstanceType.PresetInstance),
                        new LogicalChildrenClassInstance("Monitors",this,ELogicalChildrenClassInstanceType.MonitorInstance),

                    };
                }
                return _LogicalChildren;
            }
        }

        #endregion
        #region LogicalTreviewBackground
        [JsonIgnore]
        public override SolidColorBrush LogicalTreviewBackground
        {
            get
            {
                return yellowColor;
                //return fullTransparent;//white full transparency (no color)
            }
        }
        #endregion


        //Work with LayoutProeprties
        #region Increase & Decrease Inches Ratio
        public void IncreaseInchesRatio(double inIncreasmentValue = 0.005)
        {
            InchesRatio += inIncreasmentValue;
        }

        public void DecreaseInchesRatio(double inIncreasmentValue = 0.005)
        {
            InchesRatio -= inIncreasmentValue;
        }
        #endregion


        //Events
        #region Monitors & Presets Events
        private void InMonitor_EizoActionCalled(object sender, MVW_UserActionEventArgument<DtoMonitor, EMVWActions> e) // Monitors EIZO ACTION CALLED EVENT
        {
            NotifyEizoActionCalled(e.ObjectCaller, this, e.ActionType);
        }

        private void InPreset_EizoActionCalled(object sender, MVW_UserActionEventArgument<DtoPreset, EMVWActions> e) // Presets EIZO ACTION CALLED EVENT
        {
            NotifyEizoActionCalled(e.ObjectCaller, this, e.ActionType);
        }
        #endregion

        //Actions Called
        #region EizoAction Called Event
        public event EventHandler<MVW_UserActionEventArgument<DtoSmartWall, EMVWActions>> EizoActionCalled;

        public void NotifyEizoActionCalled(object inObjectCaller, DtoSmartWall inSelectedObjectInstance, EMVWActions inActionType)
        {
            EizoActionCalled?.Invoke(this, new MVW_UserActionEventArgument<DtoSmartWall, EMVWActions>(inObjectCaller, inSelectedObjectInstance, inActionType));
        }
        #endregion

        //Propery changer
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
