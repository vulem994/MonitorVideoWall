using MVW_ClassLibrary.Common.DtoModels;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for SmartWallSettings_UserControl.xaml
    /// </summary>
    public partial class SmartWallTabSettings_UserControl : UserControl, INotifyPropertyChanged
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


        //MultiLanguage
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

        //help props
        #region -applyNeed- property
        private bool _applyNeed = false;
        public bool applyNeed
        {
            get { return _applyNeed; }
            set
            {
                if (_applyNeed != value)
                {
                    _applyNeed = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        public SmartWallTabSettings_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();

            InitializeControl();
        }

        //Initialize
        #region Initialize Control
        private void InitializeControl()
        {
            if (smartWallSettings_UserControl != null)
            {
                smartWallSettings_UserControl.SavingsNeed += SmartWallSettings_UserControl_SavingsNeed;
            }
            if (layout_userControl != null)
            {
                layout_userControl.SavingsNeed += Layout_userControl_SavingsNeed;
            }
            if (presetTab_UserControl != null)
            {
                presetTab_UserControl.SavingsNeed += PresetTab_UserControl_SavingsNeed;
            }
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }
        }
        #endregion


        //Work with SmartWall functions
        #region Set SmartWall function
        public void SetSmartWall(DtoSmartWall inSmartWall)
        {
            if (SmartWall != null/* && applyNeed*/) // Zakomentarisan Apply
            {
                //SaveSettingsChanges(); ///cuva promene prethodnog .. - Zamena za apply

                //var resault = MessageBox.Show($"You have changes. Do you want to apply changes", "Changes", MessageBoxButton.YesNo, MessageBoxImage.Question); //na promenu selektovanog smart wall pitanje da li zeli da potvrdi izmene
                //if (resault == MessageBoxResult.Yes)
                //{
                //    SaveSettingsChanges();
                //    NotifyThatSavingsNeed();
                //}
            }
            SmartWall = inSmartWall;

            smartWallSettings_UserControl.SetSmartWallAndControlUIType(inSmartWall, Common.Enumerations.EFormInitializeType.Edit); //Settings
            layout_userControl.SetSmartWall(inSmartWall); //Layout
            presetTab_UserControl.SetSmartWall(inSmartWall);

            //applyNeed = false;
        }
        #endregion

        #region Save Sttings changes
        internal void SaveSettingsChanges()
        {
            if (smartWallSettings_UserControl != null)
            {
                smartWallSettings_UserControl.SaveChanges();
            }
        }
        #endregion


        //Events
        #region SmartWall Settings UC events
        private void SmartWallSettings_UserControl_SavingsNeed(object sender, EventArgs e)
        {
            //applyNeed = true;         
            NotifyThatSavingsNeed();
        }
        #endregion

        #region Prese tTab UC events
        private void PresetTab_UserControl_SavingsNeed(object sender, EventArgs e)
        {
            //applyNeed = true;         
            NotifyThatSavingsNeed();
        }
        #endregion

        #region Monitor Positioning Layout UC events
        private void Layout_userControl_SavingsNeed(object sender, EventArgs e)
        {
            //applyNeed = true;         
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

        //Propery Changer
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
