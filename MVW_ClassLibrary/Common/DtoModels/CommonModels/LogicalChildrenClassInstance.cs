using MVW_ClassLibrary.Common.Converters;
using MVW_ClassLibrary.Common.DtoModels.CommonModels;
using MVW_ClassLibrary.Common.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVW_ClassLibrary.Common.DtoModels.CommonModels
{
    public class LogicalChildrenClassInstance : ALogicalChildrenClass, INotifyPropertyChanged
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

        #region -Children- property
        private readonly ObservableCollection<ALogicalChildrenClass> _Children;
        public ObservableCollection<ALogicalChildrenClass> Children
        {
            get { return _Children; }
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
                    _ConnectedSmartWall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region InstanceType -prop-
        private ELogicalChildrenClassInstanceType InstanceType;
        #endregion

        #region Context Menu
        [JsonIgnore]
        bool isContextMenuInitialized = false;
        [JsonIgnore]
        public ContextMenu logicalInstanceContextMenu = new ContextMenu();
        [JsonIgnore]
        public MenuItem miAddPreset = new MenuItem()
        {
            Header = "Add Preset",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.add_presets_b), }
        };
        [JsonIgnore]
        public MenuItem miAddMonitor = new MenuItem()
        {
            Header = "Add Monitor",
            Icon = new Image() { Source = SharedConverters.ConvertBitmap2BitmapImage(Properties.Resources.add_monitor_b), }
        };
        #endregion

        public LogicalChildrenClassInstance(string inName, DtoSmartWall inConnectedArea, ELogicalChildrenClassInstanceType inType)
        {
            Name = inName;

            ConnectedSmartWall = inConnectedArea;
            InstanceType = inType;

            _Children = new ObservableCollection<ALogicalChildrenClass>();

            #region Children list collection changed initialization
            if (inType == ELogicalChildrenClassInstanceType.MonitorInstance)
            {
                if (inConnectedArea.MonitorsList != null)
                {
                    foreach (var item in inConnectedArea.MonitorsList)
                    {
                        _Children.Add(item);
                    }
                    ConnectedSmartWall.MonitorsList.CollectionChanged += ChildrenList_CollectionChanged;
                }
            }
            else if (inType == ELogicalChildrenClassInstanceType.PresetInstance)
            {
                if (inConnectedArea.PresetsList != null)
                {
                    _Children = new ObservableCollection<ALogicalChildrenClass>(inConnectedArea.PresetsList.Cast<ALogicalChildrenClass>().ToList());
                    ConnectedSmartWall.PresetsList.CollectionChanged += ChildrenList_CollectionChanged;
                }
            }
            #endregion

            InitializeContextMenu();
        }

        //Initialize
        #region Initialize Context menu
        private void InitializeContextMenu()
        {
            if (InstanceType == ELogicalChildrenClassInstanceType.MonitorInstance || InstanceType == ELogicalChildrenClassInstanceType.Monitor)
            {
                miAddMonitor.Click += MiAddMonitor_Click;
                logicalInstanceContextMenu.Items.Add(miAddMonitor);
                isContextMenuInitialized = true;
            }
            else if (InstanceType == ELogicalChildrenClassInstanceType.PresetInstance || InstanceType == ELogicalChildrenClassInstanceType.Preset)
            {
                miAddPreset.Click += MiAddPreset_Click;
                logicalInstanceContextMenu.Items.Add(miAddPreset);
                isContextMenuInitialized = true;
            }
        }
        private void MiAddMonitor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ConnectedSmartWall != null)
            {
                ConnectedSmartWall.NotifyEizoActionCalled(ConnectedSmartWall, ConnectedSmartWall, EMVWActions.AddMonitor);
            }
        }

        private void MiAddPreset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ConnectedSmartWall != null)
            {
                ConnectedSmartWall.NotifyEizoActionCalled(ConnectedSmartWall, ConnectedSmartWall, EMVWActions.AddPreset);
            }
        }

        #endregion

        #region Collection changed event
        [field: NonSerialized]
        private void ChildrenList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RefreshChildren(e.Action, e.NewItems, e.OldItems);
        }
        #endregion

        #region Refresh children
        private void RefreshChildren(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            if (action == NotifyCollectionChangedAction.Add && newItems != null && newItems.Count > 0)
            {
                foreach (var item in newItems)
                {
                    if (item.GetType() == typeof(DtoMonitor)) //For monitors
                    {
                        var typedItem = item as DtoMonitor;
                        if (typedItem != null && !_Children.Contains(typedItem))
                        {
                            _Children.Add(typedItem);
                        }
                    }
                    else if (item.GetType() == typeof(DtoPreset)) //For Presets
                    {
                        var typedItem = item as DtoPreset;
                        if (typedItem != null && !_Children.Contains(typedItem))
                        {
                            _Children.Add(typedItem);
                        }
                    }
                }
            }
            else if (action == NotifyCollectionChangedAction.Remove && oldItems != null && oldItems.Count > 0)
            {
                foreach (var item in oldItems)
                {
                    if (item.GetType() == typeof(DtoMonitor))//For monitors
                    {
                        var typedItem = item as DtoMonitor;
                        if (typedItem != null && _Children.Contains(typedItem))
                        {
                            _Children.Remove(typedItem);
                        }
                    }
                    else if (item.GetType() == typeof(DtoPreset)) //For Presets
                    {
                        var typedItem = item as DtoPreset;
                        if (typedItem != null && _Children.Contains(typedItem))
                        {
                            _Children.Remove(typedItem);
                        }
                    }
                }
            }
        }
        #endregion


        #region Logical name
        public override string LogicalName
        {
            get
            {
                return Name;
            }
        }
        #endregion
        #region Logical type
        public override ELogicalChildrenClassInstanceType Type
        {
            get
            {
                return InstanceType;
            }
        }
        #endregion
        #region Logical context menu
        public override ContextMenu LogicalContextMenu
        {
            get
            {
                if (isContextMenuInitialized)
                {
                    return logicalInstanceContextMenu;
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
        public override ObservableCollection<ALogicalChildrenClass> LogicalChildren
        {
            get
            {
                return Children;
            }
        }
        #endregion
        #region LogicalTreviewBackground
        [JsonIgnore]
        public override SolidColorBrush LogicalTreviewBackground
        {
            get
            {
                if (InstanceType == ELogicalChildrenClassInstanceType.MonitorInstance || InstanceType == ELogicalChildrenClassInstanceType.Monitor)
                {
                    return lightBlueColor;
                }
                else if (InstanceType == ELogicalChildrenClassInstanceType.PresetInstance || InstanceType == ELogicalChildrenClassInstanceType.Preset)
                {
                    return darkGreenColor;
                }
                return fullTransparent;//white full transparency (no color)
            }
        }
        #endregion



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
