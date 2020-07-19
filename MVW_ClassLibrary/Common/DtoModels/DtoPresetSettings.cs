
using MVW_ClassLibrary.Common.Converters;
using MVW_ClassLibrary.Common.DtoModels.CommonModels;
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
    public class DtoPresetSettings : ALogicalChildrenClass, INotifyPropertyChanged
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

        #region -ConnectedPresetID- property
        private String _ConnectedPresetID;
        public String ConnectedPresetID
        {
            get { return _ConnectedPresetID; }
            set
            {
                if (_ConnectedPresetID != value)
                {
                    _ConnectedPresetID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //Not Saving properties 
        #region -ParentMonitor- property
        [JsonIgnore]
        private DtoMonitor _ParentMonitor;
        [JsonIgnore]
        public DtoMonitor ParentMonitor
        {
            get { return _ParentMonitor; }
            set
            {
                if (_ParentMonitor != value)
                {
                    _ParentMonitor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ConnectedPreset- property
        [JsonIgnore]
        private DtoPreset _ConnectedPreset;
        [JsonIgnore]
        public DtoPreset ConnectedPreset
        {
            get { return _ConnectedPreset; }
            set
            {
                if (_ConnectedPreset != value)
                {
                    _ConnectedPreset = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Context menu
        [JsonIgnore]
        bool isContextMenuInitialized = false;
        [JsonIgnore]
        public ContextMenu presetSettingsContextMenu = new ContextMenu();
        [JsonIgnore]
        public MenuItem miEditSettings = new MenuItem()
        {
            Header = "Edit Settings",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.settings), }
        };
        [JsonIgnore]
        public MenuItem miOption1 = new MenuItem() { Header = "opt1" }; //not implemented
        [JsonIgnore]
        public MenuItem miOption2 = new MenuItem() { Header = "opt2" };
        #endregion

        public DtoPresetSettings()
        {
            InitializeContextMenu();
        }

        public DtoPresetSettings(DtoPreset inConnectedPreset)
        {
            Name = inConnectedPreset.Name;
            ConnectedPresetID = inConnectedPreset.PresetIDstring;
            ConnectedPreset = inConnectedPreset;
            InitializeContextMenu();
        }

        //Initialize
        #region Initialize Context menu
        private void InitializeContextMenu()
        {

            miEditSettings.Click += MiEditSettings_Click;
            //miOption1.Click += MiOption1_Click;
            //miOption2.Click += MiOption2_Click;


            presetSettingsContextMenu.Items.Add(miEditSettings);
            isContextMenuInitialized = true;
        }

        private void MiOption2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MiOption1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MiEditSettings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.EditPresetSettings);
        }


        #endregion

        //OnLoad Functions
        #region Set Parent Monitor function
        public void SetParentMonitor(DtoMonitor inMonitor)
        {
            if (inMonitor != null)
            {
                ParentMonitor = inMonitor;
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
                return Name + " settings";
            }
        }
        #endregion
        #region Logical type
        [JsonIgnore]
        public override ELogicalChildrenClassInstanceType Type
        {
            get
            {
                return ELogicalChildrenClassInstanceType.PresetSettings;
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
                    return presetSettingsContextMenu;
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
                return null;
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


        //Actions Called
        #region EizoAction Called Event
        public event EventHandler<MVW_UserActionEventArgument<DtoPresetSettings, EMVWActions>> EizoActionCalled;

        public void NotifyEizoActionCalled(object inObjectCaller, DtoPresetSettings inSelectedObjectInstance, EMVWActions inActionType)
        {
            EizoActionCalled?.Invoke(this, new MVW_UserActionEventArgument<DtoPresetSettings, EMVWActions>(inObjectCaller, inSelectedObjectInstance, inActionType));
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
