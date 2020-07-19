using MVW_ClassLibrary.Common.DtoModels;
using MVW_ClassLibrary.Common.DtoModels.CommonModels;
using MVW_ClassLibrary.Common.Enumerations;
using MVW_ClassLibrary.Common.EventHandlers;
using MVW_ControlsAndFormsLibrary.Forms;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for EizoMain_UserControl.xaml
    /// </summary>
    public partial class MVWMain_UserControl : UserControl, INotifyPropertyChanged
    {
        //Config
        #region -PluginConfig- property
        private DtoMainConfiguration _PluginConfig;
        public DtoMainConfiguration PluginConfig
        {
            get { return _PluginConfig; }
            set
            {
                if (_PluginConfig != value)
                {
                    if (_PluginConfig != null)
                    {
                        _PluginConfig.EizoActionCalled -= PluginConfig_EizoActionCalled;
                    }
                    _PluginConfig = value;
                    _PluginConfig.EizoActionCalled += PluginConfig_EizoActionCalled;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -PluginId- property
        private Guid _PluginId;
        public Guid PluginId
        {
            get { return _PluginId; }
            set
            {
                if (_PluginId != value)
                {
                    _PluginId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ActiveEditingSmartWallLayout- property
        private DtoSmartWall _ActiveEditingSmartWallLayout;
        public DtoSmartWall ActiveEditingSmartWallLayout
        {
            get { return _ActiveEditingSmartWallLayout; }
            set
            {
                if (_ActiveEditingSmartWallLayout != value)
                {
                    _ActiveEditingSmartWallLayout = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        //ContextMenu
        #region Context Menu
        ContextMenu treeView_ContextMenu = new ContextMenu();

        MenuItem miAddSmartWall = new MenuItem();
        #endregion

        //Help properties
        #region -SelectedTreeviewObject- property
        private object _SelectedTreeviewObject;
        public object SelectedTreeviewObject
        {
            get { return _SelectedTreeviewObject; }
            set
            {
                if (_SelectedTreeviewObject != value)
                {
                    _SelectedTreeviewObject = value;
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

        public MVWMain_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();

            InitializeControl();
            InitializeContextMenu();
        }

        public MVWMain_UserControl(Guid inPluginID)
        {
            PluginId = inPluginID;
            this.DataContext = this;
            InitializeComponent();

            InitializeControl();
            InitializeContextMenu();
            LoadEizoConfiguration();
        }

        //Initialize
        #region Initialize Control
        private void InitializeControl()
        {
            if (PluginConfig == null)
            {
                PluginConfig = new DtoMainConfiguration();
            }
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }

            smartWallTabbedSettings_UC.SavingsNeed += SmartWallSettings_UC_SavingsNeed;
            monitorTabbedSettings_UC.SavingsNeed += MonitorTabbedSettings_UC_SavingsNeed;
        }
        #endregion

        #region Initialize Context Menu
        private void InitializeContextMenu()
        {
            miAddSmartWall.Header = "Add SmartWall";
            miAddSmartWall.Click += MiAddSmartWall_Click;

            treeView_ContextMenu.Items.Add(miAddSmartWall);
            treeView_SmartWalls.ContextMenu = treeView_ContextMenu;
        }
        #endregion

        //Mockup
        #region Set Config Function
        public void SetConfig(DtoMainConfiguration inconfig)
        {
            PluginConfig = inconfig;
            PluginConfig.GenerateAllSmartWalls();
        }
        #endregion


        //Work with SmartWalls, Monitors and Presets
        #region Add, Edit & Remove SmartWall functions
        public bool AddSmartWall(DtoSmartWall inNewSmartWall)
        {
            if (PluginConfig != null)
            {
                return PluginConfig.AddSmartWall(inNewSmartWall);
            }
            return false;
        }

        public bool EditSmartWall(DtoSmartWall inSmartWallForEdit)
        {
            return false;
        }

        public bool RemoveSmartWall(DtoSmartWall inSmartWallForRemove)
        {
            if (PluginConfig != null)
            {
                return PluginConfig.RemoveSmartWall(inSmartWallForRemove);
            }
            return false;
        }
        #endregion

        #region Add, Edit & Remove Monitor functions
        public bool AddMonitor(DtoMonitor inMonitor, DtoSmartWall inParentSmartWall)
        {
            if (inParentSmartWall != null && inMonitor != null)
            {
                return inParentSmartWall.AddMonitor(inMonitor);
            }
            return false;
        }

        public bool EditMonitor(DtoMonitor inMonitor)
        {
            return false;
        }

        public bool RemoveMonitor(DtoMonitor inMonitor)
        {
            if (inMonitor != null)
            {
                return inMonitor.RemoveMeFromParentSmartWall();
            }
            return false;
        }
        #endregion

        #region Add, Edit & Remove Preset functions
        public bool AddPreset(DtoPreset inPreset, DtoSmartWall inParentSmartWall)
        {
            if (inParentSmartWall != null && inPreset != null)
            {
                return inParentSmartWall.AddPreset(inPreset);
            }
            return false;
        }

        public bool EditPreset(DtoPreset inPreset)
        {
            return false;
        }

        public bool RemovePreset(DtoPreset inPreset)
        {
            if (inPreset != null)
            {
                return inPreset.RemoveMeFromParentSmartWall();
            }
            return false;
        }
        #endregion

        //Work with Options part of window functions
        #region Set Tabbed User Controls functions
        private void SetSelectedSmartWallSettings(DtoSmartWall inSmartWall)
        {
            smartWallTabbedSettings_UC.SetSmartWall(inSmartWall);
        }

        private void SetSelectedMonitorSettings(DtoMonitor inMonitor)
        {
            monitorTabbedSettings_UC.SetMonitor(inMonitor);
        }

        private void SetSelectedPresetSettings(DtoPreset inPreset)
        {
            //smartWallSettings_UC.SetSmartWall(inSmartWall);
        }
        #endregion

        #region Save Changes On Tabbed UserControls function
        private void SaveTabbedSettingsOnChange()
        {
            smartWallTabbedSettings_UC.SaveSettingsChanges();
            monitorTabbedSettings_UC.SaveSettingsChanges();
            //preset_UC.Save..
        }
        #endregion


        //Work With  Configuration
        #region Load & Save  Configuration functions
        private void LoadEizoConfiguration()
        {
            if (PluginId != Guid.Empty)
            {
                //EizoPluginConfig = GlobalConfigHelpers.LoadGlobalEizoConfiguration(PluginId);
                //EizoPluginConfig?.GenerateAllSmartWalls();
            }
        }

        public void SaveEizoConfiguration()
        {
            if (PluginConfig != null)
            {
                //if (smartWallSettings_UC != null) //Izbaceno odave cuvanje podataka. Cuva se na button Save u kontroli!!!
                //{
                //    smartWallSettings_UC.SaveSettingsChanges();
                //}
                //GlobalConfigHelpers.SaveGlobalEizoConfiguration(EizoPluginConfig);
            }
        }
        #endregion


        //Events
        #region Eizo Config Events
        private void PluginConfig_EizoActionCalled(object sender, MVW_UserActionEventArgument<DtoMainConfiguration, EMVWActions> e)
        {
            #region SmartWall calls
            if (e.ActionType == EMVWActions.EditSmartWall && (e.ObjectCaller.GetType() == typeof(DtoSmartWall))) //Edit SmartWall
            {
                var tmpSmartWall = e.ObjectCaller as DtoSmartWall;
                if (tmpSmartWall != null)
                {
                    var resault = EditSmartWall(tmpSmartWall);
                    if (resault == true)
                    {
                        NotifyThatSavingsNeed();
                    }
                }
            }

            else if (e.ActionType == EMVWActions.RemoveSmartWall && (e.ObjectCaller.GetType() == typeof(DtoSmartWall))) //Remove SmartWall
            {
                var tmpSmartWall = e.ObjectCaller as DtoSmartWall;
                if (tmpSmartWall != null)
                {
                    var resault = MessageBox.Show($"Are you sure want delete {tmpSmartWall.Name}", "Delete SmartWall", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resault == MessageBoxResult.Yes)
                    {
                        var removedSmartWall = RemoveSmartWall(tmpSmartWall);
                        if (removedSmartWall == true)
                        {
                            NotifyThatSavingsNeed();
                        }
                    }
                }
            }
            else if (e.ActionType == EMVWActions.EditSmartWallLayout && (e.ObjectCaller.GetType() == typeof(DtoSmartWall))) //Remove SmartWall
            {
                var tmpSmartWall = e.ObjectCaller as DtoSmartWall;
                if (tmpSmartWall != null)
                {
                    //SetActiveEditLayoutSmartWall(tmpSmartWall);
                }
            }
            #endregion

            #region  Monitor calls
            else if (e.ActionType == EMVWActions.AddMonitor && (e.ObjectCaller.GetType() == typeof(DtoSmartWall))) //ADD Monitor
            {
                var tmpSmartWall = e.ObjectCaller as DtoSmartWall;
                if (tmpSmartWall != null)
                {
                    DtoMonitor monitorToAdd = new DtoMonitor("New Monitor");
                    AddMonitor_Form createMonitorForm = new AddMonitor_Form(monitorToAdd);
                    var resault = createMonitorForm.ShowDialog();
                    if (resault.HasValue && resault == true)
                    {
                        var monitorAdded = AddMonitor(monitorToAdd, tmpSmartWall);
                        if (monitorAdded == true)
                        {
                            NotifyThatSavingsNeed();
                        }
                    }
                }
            }

            else if (e.ActionType == EMVWActions.EditMonitor && (e.ObjectCaller.GetType() == typeof(DtoMonitor))) //EDIT Monitor
            {
                var tmpMonitor = e.ObjectCaller as DtoMonitor;
                if (tmpMonitor != null)
                {
                    var resault = EditMonitor(tmpMonitor);
                    if (resault == true)
                    {
                        NotifyThatSavingsNeed();
                    }
                }
            }

            else if (e.ActionType == EMVWActions.RemoveMonitor && (e.ObjectCaller.GetType() == typeof(DtoMonitor))) //REMOVE Monitor
            {
                var tmpMonitor = e.ObjectCaller as DtoMonitor;
                if (tmpMonitor != null)
                {
                    var resault = MessageBox.Show($"Are you sure want delete {tmpMonitor.Name}", "Delete Monitor", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resault == MessageBoxResult.Yes)
                    {
                        var removedMonitor = RemoveMonitor(tmpMonitor);
                        if (removedMonitor == true)
                        {
                            NotifyThatSavingsNeed();
                        }
                    }
                }
            }
            #endregion

            #region Preset calls
            else if (e.ActionType == EMVWActions.AddPreset && (e.ObjectCaller.GetType() == typeof(DtoSmartWall))) //Add Preset
            {
                var tmpSmartWall = e.ObjectCaller as DtoSmartWall;
                if (tmpSmartWall != null)
                {
                    DtoPreset presetToAdd = new DtoPreset("New Preset");
                    AddPreset_Form createPresetForm = new AddPreset_Form(presetToAdd);
                    var resault = createPresetForm.ShowDialog();
                    if (resault.HasValue && resault == true)
                    {
                        var monitorAdded = AddPreset(presetToAdd, tmpSmartWall);
                        if (monitorAdded == true)
                        {
                            NotifyThatSavingsNeed();
                        }
                    }
                }
            }
            else if (e.ActionType == EMVWActions.EditPreset && (e.ObjectCaller.GetType() == typeof(DtoPreset))) //Edit Preset
            {
                var tmpPreset = e.ObjectCaller as DtoPreset;
                if (tmpPreset != null)
                {
                    var resault = EditPreset(tmpPreset);
                    if (resault == true)
                    {
                        NotifyThatSavingsNeed();
                    }
                }
            }
            else if (e.ActionType == EMVWActions.RemovePreset && (e.ObjectCaller.GetType() == typeof(DtoPreset))) //Remove Preset
            {
                var tmpPreset = e.ObjectCaller as DtoPreset;
                if (tmpPreset != null)
                {
                    var resault = MessageBox.Show($"Are you sure want delete {tmpPreset.Name}", "Delete Preset", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resault == MessageBoxResult.Yes)
                    {
                        var removedMonitor = RemovePreset(tmpPreset);
                        if (removedMonitor == true)
                        {
                            NotifyThatSavingsNeed();
                        }
                    }
                }
            }
            #endregion

            else if (e.ActionType == EMVWActions.EditPresetSettings && (e.ObjectCaller.GetType() == typeof(DtoPresetSettings))) //Add Preset
            {

            }
        }
        #endregion

        #region Context Menu Events
        private void MiAddSmartWall_Click(object sender, RoutedEventArgs e)
        {
            DtoSmartWall smartWallToAdd = new DtoSmartWall("New SmartWall");
            AddSmartWall_Form newSmartWallForm = new AddSmartWall_Form(smartWallToAdd);
            var resault = newSmartWallForm.ShowDialog();
            if (resault.HasValue && resault == true)
            {
                var monitorAdded = AddSmartWall(smartWallToAdd);
                if (monitorAdded == true)
                {
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region Treeview Events
        private void treeView_SmartWalls_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) //SELECTED ITEM CHANGED
        {
            SaveTabbedSettingsOnChange();
            if (e.NewValue != null)
            {
                var type = e.NewValue.GetType();
                if (type == typeof(DtoSmartWall))
                {
                    var typedSmartWall = e.NewValue as DtoSmartWall;
                    if (typedSmartWall != null)
                    {
                        SelectedTreeviewObject = typedSmartWall;
                        SetSelectedSmartWallSettings(typedSmartWall);
                    }
                }
                else if (type == typeof(LogicalChildrenClassInstance))
                {
                    var typedInstance = e.NewValue as LogicalChildrenClassInstance;
                    if (typedInstance != null)
                    {
                        SelectedTreeviewObject = typedInstance?.ConnectedSmartWall;
                        SetSelectedSmartWallSettings(typedInstance?.ConnectedSmartWall);
                    }
                }
                else if (type == typeof(DtoMonitor))
                {
                    var typedMonitor = e.NewValue as DtoMonitor;
                    if (typedMonitor != null)
                    {
                        SelectedTreeviewObject = typedMonitor;
                        SetSelectedMonitorSettings(typedMonitor);
                    }
                }
                else if (type == typeof(DtoPreset))
                {
                    var typedPreset = e.NewValue as DtoPreset;
                    if (typedPreset != null)
                    {
                        SelectedTreeviewObject = typedPreset;
                    }
                }
                else if (type == typeof(DtoPresetSettings))
                {
                    var typedPreset = e.NewValue as DtoPresetSettings;
                    if (typedPreset != null)
                    {
                        SelectedTreeviewObject = typedPreset;
                    }
                }

            }
        }
        #endregion

        #region Tabed Settings event
        private void SmartWallSettings_UC_SavingsNeed(object sender, EventArgs e) //SmartWall
        {
            NotifyThatSavingsNeed();
        }

        private void MonitorTabbedSettings_UC_SavingsNeed(object sender, EventArgs e) //Monitor
        {
            NotifyThatSavingsNeed();
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


        //Language Change
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var comboboxItem = e.AddedItems[0] as ComboBoxItem;
                if (comboboxItem != null && MultiLanguageImp != null)
                {
                    if (comboboxItem.Name == "English")
                    {
                        MultiLanguageImp.UpdateResources("en");
                    }
                    else if (comboboxItem.Name == "Serbian")
                    {
                        MultiLanguageImp.UpdateResources("sr");
                    }
                }
            }

        }
    }
}
