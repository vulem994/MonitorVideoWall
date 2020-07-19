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
    public class DtoMonitor : ALogicalChildrenClass, INotifyPropertyChanged
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

        #region -PresetSettingsList- property
        private ObservableCollection<DtoPresetSettings> _PresetSettingsList;
        public ObservableCollection<DtoPresetSettings> PresetSettingsList
        {
            get { return _PresetSettingsList; }
            set
            {
                if (_PresetSettingsList != value)
                {
                    if (_PresetSettingsList != null)
                    {
                        _PresetSettingsList.CollectionChanged -= PresetSettingsList_CollectionChanged;
                    }
                    _PresetSettingsList = value;
                    _PresetSettingsList.CollectionChanged += PresetSettingsList_CollectionChanged;
                    NotifyPropertyChanged();
                }
            }
        }


        #endregion

        //EIZO properties
        #region -IPAdress- property
        private String _IPAdress;
        public String IPAdress
        {
            get { return _IPAdress; }
            set
            {
                if (_IPAdress != value)
                {
                    _IPAdress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -UserName- property
        private String _UserName;
        public String UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -Password- property
        private String _Password;
        public String Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -HTTPport- property
        private String _HTTPport;
        public String HTTPport
        {
            get { return _HTTPport; }
            set
            {
                if (_HTTPport != value)
                {
                    _HTTPport = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //LayoutTest modelks 
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

        #region -StartPointInchesX- property
        private double _StartPointInchesX;
        public double StartPointInchesX
        {
            get { return _StartPointInchesX; }
            set
            {
                if (_StartPointInchesX != value)
                {
                    _StartPointInchesX = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -StartPointInchesY- property
        private double _StartPointInchesY;
        public double StartPointInchesY
        {
            get { return _StartPointInchesY; }
            set
            {
                if (_StartPointInchesY != value)
                {
                    _StartPointInchesY = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -InchesDiagonalSize- property
        private double _InchesDiagonalSize;
        public double InchesDiagonalSize
        {
            get { return _InchesDiagonalSize; }
            set
            {
                if (_InchesDiagonalSize != value)
                {
                    _InchesDiagonalSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //Not saving props.
        #region -toRetChildrenList- property
        //Za sad se ne koristi. predvidjena za prisetove ukoliko budu potrebni u hijerarhiji ispod monitora
        [JsonIgnore] // READONLY lista koja vraca listu outputa kao AlogicalChildrenClass!!!!
        private readonly ObservableCollection<ALogicalChildrenClass> _toRetChildrenList = new ObservableCollection<ALogicalChildrenClass>();
        [JsonIgnore]
        public ObservableCollection<ALogicalChildrenClass> toRetChildrenList
        {
            get { return _toRetChildrenList; }
        }
        #endregion 

        #region -ParentSmartWall- property
        [JsonIgnore]
        private DtoSmartWall _ParentSmartWall;
        [JsonIgnore]
        public DtoSmartWall ParentSmartWall
        {
            get { return _ParentSmartWall; }
            set
            {
                if (_ParentSmartWall != value)
                {
                    if (_ParentSmartWall != null)
                    {
                        _ParentSmartWall.PresetsList.CollectionChanged -= PrisetsList_CollectionChanged;
                    }
                    _ParentSmartWall = value;
                    _ParentSmartWall.PresetsList.CollectionChanged += PrisetsList_CollectionChanged;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region -Context menu- property
        [JsonIgnore]
        bool isContextMenuInitialized = false;
        [JsonIgnore]
        public ContextMenu monitorContextMenu = new ContextMenu();
        [JsonIgnore]
        public MenuItem miRemoveMonitor = new MenuItem()
        {
            Header = "Remove Monitor",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.trash_monitor_b), }
        };
        [JsonIgnore]
        public MenuItem miEditMonitor = new MenuItem()
        {
            Header = "Edit Monitor",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.settings), }
        };

        [JsonIgnore]
        public MenuItem miOptions = new MenuItem()
        {
            Header = "Options Test",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.settings), }
        };
        #endregion


        public DtoMonitor()
        {
            InitializeContextMenu();
            //InitializeClass();
        }

        public DtoMonitor(string inName, string inDescription = "")
        {
            Name = inName;
            Description = inDescription;

            InitializeContextMenu();
            InitializeClass();
        }


        //Initialize
        #region Initialize Context menu
        private void InitializeContextMenu()
        {

            miRemoveMonitor.Click += MiRemoveMonitor_Click;
            miEditMonitor.Click += MiEditMonitor_Click;
            miOptions.Click += MiOptions_Click;

            monitorContextMenu.Items.Add(miEditMonitor);
            monitorContextMenu.Items.Add(miRemoveMonitor);
            monitorContextMenu.Items.Add(new Separator());
            monitorContextMenu.Items.Add(miOptions);
            isContextMenuInitialized = true;
        }

        private void MiOptions_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.Options1);
        }

        private void MiEditMonitor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.EditMonitor);
        }

        private void MiRemoveMonitor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.RemoveMonitor);
        }

        #endregion

        #region Initialize Class
        private void InitializeClass()
        {
            if (AspectRatio == null)
            {
                AspectRatio = AspectRatioModel.Ar4x3Model; //Default aspect ratio of monitor i 4:3
            }
            InchesDiagonalSize = 20; // default;
        }
        #endregion

        //Functions
        #region Set Parent Smart Wall
        public void SetParentSmartWall(DtoSmartWall inSmartWall)
        {
            ParentSmartWall = inSmartWall;

            //toRetChildrenList = ParentSmartWall.PrisetsList.Cast<ALogicalChildrenClass>().ToList();
        }
        #endregion

        #region Remove Me function
        public bool RemoveMeFromParentSmartWall()
        {
            if (ParentSmartWall != null)
            {
                return ParentSmartWall.RemoveMonitor(this);
            }
            return false;
        }
        #endregion


        //PresetSettings functions
        #region Add & Remove PresetSettings from list functions
        private bool AddPresetSettings(DtoPreset inConnectedPreset)
        {
            if (PresetSettingsList == null)
            {
                PresetSettingsList = new ObservableCollection<DtoPresetSettings>();
            }
            if (PresetSettingsList != null && !CheckIfPressetSettingsForGivenPresetExistInList(inConnectedPreset))
            {
                DtoPresetSettings presetSettingsToAdd = new DtoPresetSettings(inConnectedPreset);
                if (!PresetSettingsList.Contains(presetSettingsToAdd))
                {
                    PresetSettingsList.Add(presetSettingsToAdd);
                    presetSettingsToAdd.EizoActionCalled += Preset_EizoActionCalled;
                    return true;
                }

            }
            return false;
        }

        private bool RemovePresetSettings(DtoPreset inConnectedPresetForRemove)
        {
            if (PresetSettingsList != null)
            {
                foreach (var presetSettingsItem in PresetSettingsList)
                {
                    if (presetSettingsItem?.ConnectedPresetID == inConnectedPresetForRemove.PresetIDstring )
                    {
                        PresetSettingsList.Remove(presetSettingsItem);
                        presetSettingsItem.EizoActionCalled -= Preset_EizoActionCalled;
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Check if PressetSettings Exist for Given Preset in PresetSettingsList
        private bool CheckIfPressetSettingsForGivenPresetExistInList(DtoPreset inConnectedPreset)
        {
            if (PresetSettingsList != null && PresetSettingsList.Count > 0)
            {
                //var resaultPreset = PresetSettingsList.Where(i => i.ConnectedPreset.PresetIDstring == inConnectedPreset.PresetIDstring).FirstOrDefault();
                //if (resaultPreset != null)
                //{
                //    return true;
                //}
                foreach (var presetSettings in PresetSettingsList)
                {
                    if (presetSettings.ConnectedPresetID == inConnectedPreset.PresetIDstring)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Set all parent presets in monitor preset settings if need 
        public void RefreshPresetsListByParentSmartWall(IList<DtoPreset> inPresetList) // prilikom podesavanja parent SmartWall, funkcija proverava da li neki od Presetova iz liste u smart wall u nije podesena u monitoru. i ukoliko nije kreirace novi
        {
            if (PresetSettingsList == null)
            {
                PresetSettingsList = new ObservableCollection<DtoPresetSettings>();
            }
            if (PresetSettingsList != null)
            {
                foreach (var item in inPresetList)
                {
                    if (!CheckIfPressetSettingsForGivenPresetExistInList(item))
                    {
                        AddPresetSettings(item);
                    }
                }
            }
        }
        #endregion


        //On onload Generate functions
        #region Generate PresetSettings list function
        public void GeneratePresetSettingsList()
        {
            if (PresetSettingsList != null && PresetSettingsList.Count > 0)
            {
                foreach (var preset in PresetSettingsList)
                {
                    toRetChildrenList.Add(preset);
                    preset.SetParentMonitor(this);
                    preset.EizoActionCalled += Preset_EizoActionCalled;
                }
            }
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
                return ELogicalChildrenClassInstanceType.Monitor;
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
                    return monitorContextMenu;
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
        public override ObservableCollection<ALogicalChildrenClass> LogicalChildren
        {
            get
            {
                return _toRetChildrenList;
            }
        }

        #endregion
        #region LogicalTreviewBackground
        [JsonIgnore]
        public override SolidColorBrush LogicalTreviewBackground
        {
            get
            {
                return lightBlueColor;
                //return fullTransparent;
            }
        }
        #endregion


        //Events
        #region Preset Settings events
        private void PresetSettingsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) //List collection changed
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var item in e.NewItems)
                {
                    var typedNewItem = item as ALogicalChildrenClass;
                    if (typedNewItem != null && !toRetChildrenList.Contains(typedNewItem))
                    {
                        toRetChildrenList.Add(typedNewItem);
                    }
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove && e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var item in e.OldItems)
                {
                    var typedOldItem = item as ALogicalChildrenClass;
                    if (typedOldItem != null && toRetChildrenList.Contains(typedOldItem))
                    {
                        toRetChildrenList.Remove(typedOldItem);
                    }
                }
            }
        }


        private void Preset_EizoActionCalled(object sender, MVW_UserActionEventArgument<DtoPresetSettings, EMVWActions> e) //PresetSettings action called
        {
            NotifyEizoActionCalled(e.ObjectCaller, this, e.ActionType);
        }
        #endregion

        #region Parent SmartWall events
        private void PrisetsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) // PRESET LIST Collection Changed
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (var item in e.NewItems)
                {
                    var typedNewItem = item as DtoPreset;
                    if (typedNewItem != null)
                    {
                        AddPresetSettings(typedNewItem);
                    }
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove && e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (var item in e.OldItems)
                {
                    var typedOldItem = item as DtoPreset;
                    if (typedOldItem != null)
                    {
                        RemovePresetSettings(typedOldItem);
                    }
                }
            }
        }
        #endregion


        //Actions Called
        #region EizoAction Called Event
        public event EventHandler<MVW_UserActionEventArgument<DtoMonitor, EMVWActions>> EizoActionCalled;

        public void NotifyEizoActionCalled(object inObjectCaller, DtoMonitor inSelectedObjectInstance, EMVWActions inActionType)
        {
            EizoActionCalled?.Invoke(this, new MVW_UserActionEventArgument<DtoMonitor, EMVWActions>(inObjectCaller, inSelectedObjectInstance, inActionType));
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
