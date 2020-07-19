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
    public class DtoPreset : ALogicalChildrenClass, INotifyPropertyChanged
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

        #region -PresetIDstring- property
        private String _PresetIDstring;
        public String PresetIDstring
        {
            get { return _PresetIDstring; }
            set
            {
                if (_PresetIDstring != value)
                {
                    _PresetIDstring = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //Non saving props
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
                    _ParentSmartWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Context Menu
        [JsonIgnore]
        bool isContextMenuInitialized = false;
        [JsonIgnore]
        public ContextMenu presetContextMenu = new ContextMenu();
        [JsonIgnore]
        public MenuItem miRemovePrest = new MenuItem()
        {
            Header = "Remove Preset",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.trash_presets_b), }
        };
        [JsonIgnore]
        public MenuItem miEditPreset = new MenuItem()
        {
            Header = "Edit Preset",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.settings), }
        };
        #endregion

        public DtoPreset()
        {
            InitializeContextMenu();
        }

        public DtoPreset(string inName, string inDescription = "")
        {
            Name = inName;
            Description = inDescription;
            PresetIDstring = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();

            InitializeContextMenu();
          
        }

        //Initialize
        #region Initialize Context Menu  & Context Menu Events
        private void InitializeContextMenu()
        {
            miRemovePrest.Click += MiRemovePrest_Click;
            miEditPreset.Click += MiEditPreset_Click;

            presetContextMenu.Items.Add(miEditPreset);
            presetContextMenu.Items.Add(miRemovePrest);
            //presetContextMenu.Items.Add(new Separator());
            isContextMenuInitialized = true;
        }

        private void MiEditPreset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.EditPreset);
        }

        private void MiRemovePrest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NotifyEizoActionCalled(this, this, EMVWActions.RemovePreset);
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
                return ParentSmartWall.RemovePreset(this);
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
                return ELogicalChildrenClassInstanceType.Preset;
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
                    return presetContextMenu;
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
                return darkGreenColor;//white full transparency (no color)
            }
        }
        #endregion



        //Actions Called
        #region EizoAction Called Event
        public event EventHandler<MVW_UserActionEventArgument<DtoPreset, EMVWActions>> EizoActionCalled;

        public void NotifyEizoActionCalled(object inObjectCaller, DtoPreset inSelectedObjectInstance, EMVWActions inActionType)
        {
            EizoActionCalled?.Invoke(this, new MVW_UserActionEventArgument<DtoPreset, EMVWActions>(inObjectCaller, inSelectedObjectInstance, inActionType));
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
