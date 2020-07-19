
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ClassLibrary.Common.DtoModels.HelperModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using MVW_MultiLanguageImplementation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls.CommonUserControls
{
    /// <summary>
    /// Interaction logic for AddEditMonitor_UserControl.xaml
    /// </summary>
    public partial class AddEditMonitor_UserControl : UserControl, INotifyPropertyChanged
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
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region -tmpUserName- property
        private String _tmpUserName;
        public String tmpUserName
        {
            get { return _tmpUserName; }
            set
            {
                if (_tmpUserName != value)
                {
                    _tmpUserName = value;
                    NotifyPropertyChanged();
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region -tmpPassword- property
        private String _tmpPassword;
        public String tmpPassword
        {
            get { return _tmpPassword; }
            set
            {
                if (_tmpPassword != value)
                {
                    _tmpPassword = value;
                    NotifyPropertyChanged();
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region -tmpIPAdress- property
        private String _tmpIPAdress;
        public String tmpIPAdress
        {
            get { return _tmpIPAdress; }
            set
            {
                if (_tmpIPAdress != value)
                {
                    _tmpIPAdress = value;
                    NotifyPropertyChanged();
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region -tmpHTTPPort- property
        private String _tmpHTTPPort;
        public String tmpHTTPPort
        {
            get { return _tmpHTTPPort; }
            set
            {
                if (_tmpHTTPPort != value)
                {
                    _tmpHTTPPort = value;
                    NotifyPropertyChanged();
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region -tmpDiagonalSizeInches- property
        private double _tmpDiagonalSizeInches;
        public double tmpDiagonalSizeInches
        {
            get { return _tmpDiagonalSizeInches; }
            set
            {
                if (_tmpDiagonalSizeInches != value)
                {
                    _tmpDiagonalSizeInches = value;
                    NotifyPropertyChanged();
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        #region -tmpAspectRatio- property
        private AspectRatioModel _tmpAspectRatio;
        public AspectRatioModel tmpAspectRatio
        {
            get { return _tmpAspectRatio; }
            set
            {
                if (_tmpAspectRatio != value)
                {
                    _tmpAspectRatio = value;
                    NotifyPropertyChanged();
                    NotifyThatSavingsNeed();
                }
            }
        }
        #endregion

        //Helper properties
        //combobox
        #region -AllAspectRatioModelsList- property
        private List<AspectRatioModel> _AllAspectRatioModelsList;
        public List<AspectRatioModel> AllAspectRatioModelsList
        {
            get { return _AllAspectRatioModelsList; }
            set
            {
                if (_AllAspectRatioModelsList != value)
                {
                    _AllAspectRatioModelsList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -AllInchesSizeList- property
        private List<double> _AllInchesSizeList;
        public List<double> AllInchesSizeList
        {
            get { return _AllInchesSizeList; }
            set
            {
                if (_AllInchesSizeList != value)
                {
                    _AllInchesSizeList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //isInitialization
        #region -initialize- property
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

        public AddEditMonitor_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeForm();
        }

        public AddEditMonitor_UserControl(DtoMonitor inMonitor, EFormInitializeType ininitializeType = EFormInitializeType.New)
        {
            this.DataContext = this;


            Monitor = inMonitor;
            tmpName = inMonitor.Name;
            tmpUserName = inMonitor.UserName;
            tmpPassword = inMonitor.Password;
            tmpIPAdress = inMonitor.IPAdress;
            tmpHTTPPort = inMonitor.HTTPport;

            tmpAspectRatio = inMonitor.AspectRatio;
            tmpDiagonalSizeInches = inMonitor.InchesDiagonalSize;

            InitializeComponent();
            InitializeForm();
        }

        //Initialize
        #region Initialize Form
        private void InitializeForm()
        {
            //Combo boxes item sources!!
            if (AllAspectRatioModelsList == null)
            {
                AllAspectRatioModelsList = AspectRatioModel.AllOptionsList;
            }
            if (AllInchesSizeList == null)
            {
                AllInchesSizeList = new List<double>() { 7, 12.1, 15, 15.6, 17, 18.5, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                    41, 42, 43, 44, 45, 46, 47, 48, 49, 50 ,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65};
            }
            if (MultiLanguageImp == null)
            {
                MultiLanguageImp = MultiLanguageImplementationModel.SingleMultiLanguageInstance;
            }
        }
        #endregion

        #region Set Monitor function
        public void SetMonitorAndControlUIType(DtoMonitor inMonitor, EFormInitializeType inControlUIType = EFormInitializeType.New)
        {
            initialize = true;
            Monitor = inMonitor;
            tmpName = inMonitor.Name;
            tmpUserName = inMonitor.UserName;
            tmpPassword = inMonitor.Password;
            tmpIPAdress = inMonitor.IPAdress;
            tmpHTTPPort = inMonitor.HTTPport;

            tmpAspectRatio = inMonitor.AspectRatio;
            tmpDiagonalSizeInches = inMonitor.InchesDiagonalSize;
            initialize = false;
        }
        #endregion

        #region Save Changes function
        public bool SaveChanges()
        {
            if (Monitor != null)
            {
                Monitor.Name = tmpName;
                Monitor.UserName = tmpUserName;
                Monitor.Password = tmpPassword;
                Monitor.IPAdress = tmpIPAdress;
                Monitor.HTTPport = tmpHTTPPort;
                Monitor.InchesDiagonalSize = tmpDiagonalSizeInches;
                Monitor.AspectRatio = tmpAspectRatio;
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
            if (!initialize)
            {
                SavingsNeed?.Invoke(this, new EventArgs());
            }
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
    }//[Class]
}//[Namespace]

