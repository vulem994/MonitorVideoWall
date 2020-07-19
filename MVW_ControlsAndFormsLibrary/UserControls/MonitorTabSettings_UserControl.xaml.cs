using MVW_ClassLibrary.Common.DtoModels;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for MonitorTabSettings_UserControl.xaml
    /// </summary>
    public partial class MonitorTabSettings_UserControl : UserControl, INotifyPropertyChanged
    {
        #region -Monitor- property
        private DtoMonitor _Monitor;
        public DtoMonitor Monitor
        {
            get { return _Monitor; }
            set
            {
                if (_Monitor != value)
                {
                    _Monitor = value;
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


        public MonitorTabSettings_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeControl();

        }

        //Initialize
        #region Initialize Control
        private void InitializeControl()
        {
            if (monitorSettings_UserControl != null)
            {
                monitorSettings_UserControl.SavingsNeed += MonitorSettings_UserControl_SavingsNeed;
            }
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }
        }
        #endregion

        //Work with monitor function
        #region Set Monitor function
        public void SetMonitor(DtoMonitor inMonitor)
        {
            if (Monitor != null)
            {
                //SaveSettingsChanges(); //za sad se iz Eizo maina poziva.
            }
            Monitor = inMonitor;
            monitorSettings_UserControl.SetMonitorAndControlUIType(inMonitor, Common.Enumerations.EFormInitializeType.Edit);
        }
        #endregion


        #region Save Sttings changes
        internal void SaveSettingsChanges()
        {
            if (monitorSettings_UserControl != null)
            {
                monitorSettings_UserControl.SaveChanges();
            }
        }
        #endregion


        //Events
        #region Monitor Settings UC events
        private void MonitorSettings_UserControl_SavingsNeed(object sender, EventArgs e)
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

        //ProperyChanger
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
    }//[Class]
}//[Namespace]
