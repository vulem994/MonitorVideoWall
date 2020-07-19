
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls.CommonUserControls
{
    /// <summary>
    /// Interaction logic for AddEditSmartWall_UserControl.xaml
    /// </summary>
    public partial class AddEditSmartWall_UserControl : UserControl, INotifyPropertyChanged
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

        #region -tmpName- property
        private String _tmpName;
        public String tmpName
        {
            get { return _tmpName; }
            set
            {
                if (_tmpName != value)
                {
                    _tmpName = value;
                    NotifyPropertyChanged();
                    if (!initialize)
                    {
                        NotifyThatSavingsNeed();
                    }
                }
            }
        }
        #endregion

        #region -tmpDescription- property
        private String _tmpDescription;
        public String tmpDescription
        {
            get { return _tmpDescription; }
            set
            {
                if (_tmpDescription != value)
                {
                    _tmpDescription = value;
                    NotifyPropertyChanged();
                    if (!initialize)
                    {
                        NotifyThatSavingsNeed();
                    }
                }
            }
        }
        #endregion


        //help props
        #region -initializeChangeText- property
        private bool _initialize = false;
        public bool initialize
        {
            get { return _initialize; }
            set
            {
                if (_initialize != value)
                {
                    _initialize = value;
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

        public AddEditSmartWall_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeControl();
        }
        public AddEditSmartWall_UserControl(DtoSmartWall inSmartWall)
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeControl();
        }

        //Initialize
        #region Initialize Control function
        private void InitializeControl()
        {
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }
        }
        #endregion

        #region Set SmartWall function
        public void SetSmartWallAndControlUIType(DtoSmartWall inSmartWall, EFormInitializeType inControlUIType = EFormInitializeType.New)
        {
            initialize = true;
            SmartWall = inSmartWall;
            tmpName = inSmartWall.Name;
            tmpDescription = inSmartWall.Description;
            initialize = false;
        }
        #endregion

        #region Save Changes function
        public bool SaveChanges()
        {
            if (SmartWall != null)
            {
                SmartWall.Name = tmpName;
                SmartWall.Description = tmpDescription;
                return true;
            }
            return false;
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
    }
}
