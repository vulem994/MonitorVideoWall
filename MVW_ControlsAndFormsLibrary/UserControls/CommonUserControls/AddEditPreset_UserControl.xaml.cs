using MVW_ClassLibrary.Common.DtoModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MVW_ControlsAndFormsLibrary.UserControls.CommonUserControls
{
    /// <summary>
    /// Interaction logic for AddEditPreset_UserControl.xaml
    /// </summary>
    public partial class AddEditPreset_UserControl : UserControl, INotifyPropertyChanged
    {

        #region -Preset- property
        private DtoPreset _Preset;
        public DtoPreset Preset
        {
            get { return _Preset; }
            set
            {
                if (_Preset != value)
                {
                    _Preset = value;
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
                }
            }
        }
        #endregion

        public AddEditPreset_UserControl()
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeForm();
        }
        public AddEditPreset_UserControl(DtoPreset inPreset)
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeForm();
        }

        //Initialize
        #region Initialize Form
        private void InitializeForm()
        {

        }
        #endregion

        #region Set Preset function
        public void SetPresetAndControlUIType(DtoPreset inPreset, EFormInitializeType inControlUIType = EFormInitializeType.New)
        {
            Preset = inPreset;
            tmpName = inPreset.Name;
            tmpDescription = inPreset.Description;
        }
        #endregion

        #region Save Changes function
        public bool SaveChanges()
        {
            if (Preset != null)
            {
                Preset.Name = tmpName;
                Preset.Description = tmpDescription;
                return true;
            }
            return false;
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
