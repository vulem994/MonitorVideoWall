
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using MVW_ControlsAndFormsLibrary.Forms;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls.CommonUserControls
{
    /// <summary>
    /// Interaction logic for SmartWall_PresetTab_UserControl.xaml
    /// </summary>
    public partial class SmartWall_PresetTab_UserControl : UserControl, INotifyPropertyChanged
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

        #region -SelectedPreset- property
        private DtoPreset _SelectedPreset;
        public DtoPreset SelectedPreset
        {
            get { return _SelectedPreset; }
            set
            {
                if (_SelectedPreset != value)
                {
                    _SelectedPreset = value;
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


        public SmartWall_PresetTab_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeControl();
        }

        //Initialize
        #region Initialize Control
        private void InitializeControl()
        {
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }
        }
        #endregion

        public void SetSmartWall(DtoSmartWall inSmartWall)
        {
            SmartWall = inSmartWall;
        }

        //Work with presets
        #region Add & Remove Presets functions
        private bool AddPreset(DtoPreset inNewPreset)
        {
            if (SmartWall != null)
            {
                SmartWall.AddPreset(inNewPreset);
                return true;
            }
            return false;
        }

        private bool RemovePreset(DtoPreset inPreset)
        {
            if (SmartWall != null)
            {
                SmartWall.RemovePreset(inPreset);
                return true;
            }
            return false;
        }
        #endregion




        //Events
        #region Buttons Events
        private void button_AddNewPreset_Click(object sender, RoutedEventArgs e)
        {
            DtoPreset presetToAdd = new DtoPreset("New Preset");
            AddPreset_Form createPresetForm = new AddPreset_Form(presetToAdd);
            var resault = createPresetForm.ShowDialog();
            if (resault.HasValue && resault == true)
            {
                var presetAdded = AddPreset(presetToAdd);
                if (presetAdded == true)
                {
                    NotifyThatSavingsNeed();
                }
            }
        }

        private void button_EditPreset_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPreset != null)
            {
                AddPreset_Form editPresetForm = new AddPreset_Form(SelectedPreset, EFormInitializeType.Edit);
                var resault = editPresetForm.ShowDialog();
                if (resault == true)
                {
                    NotifyThatSavingsNeed();
                }
            }
        }

        private void button_DeletePreset_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPreset != null)
            {
                var resault = MessageBox.Show($"Are you sure want delete {SelectedPreset.Name}", "Delete Preset", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resault == MessageBoxResult.Yes)
                {
                    var removedPreset = RemovePreset(SelectedPreset);
                    if (removedPreset == true)
                    {
                        NotifyThatSavingsNeed();
                    }
                }
            }
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
