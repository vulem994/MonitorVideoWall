
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVW_ControlsAndFormsLibrary.Forms
{
    /// <summary>
    /// Interaction logic for AddPreset_Form.xaml
    /// </summary>
    public partial class AddPreset_Form : Window, INotifyPropertyChanged
    {
        #region type 
        EFormInitializeType type;
        #endregion

        #region usingWord 
        string messageBoxText = "You are about cancel creating new preset. Are you sure you want to cancel?";
        #endregion

        public AddPreset_Form(DtoPreset inPreset, EFormInitializeType inType = EFormInitializeType.New)
        {
            this.DataContext = this;
            type = inType;
            InitializeComponent();
            InitializeForm();
            if (inPreset != null && addPreset_uc != null)
            {
                addPreset_uc.SetPresetAndControlUIType(inPreset, inType);
            }
            else
            {
                MessageBox.Show($"Problem with Preset");
                this.DialogResult = false;
                this.Close();
            }
        }

        //Initialize
        #region Initialize Form
        private void InitializeForm()
        {
            if (type == EFormInitializeType.New)
            {
                messageBoxText = "You are about cancel creating new preset. Are you sure you want to cancel?";
            }
            else if (type == EFormInitializeType.Edit)
            {
                messageBoxText = "You are about cancel editing preset. Changes will be lost. Are you sure you want to cancel?";
            }
        }
        #endregion

        //Events
        #region Buttons events
        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            var resault = addPreset_uc.SaveChanges();
            if (resault)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Preset creation was not successful.");
                this.DialogResult = false;
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            var resault = MessageBox.Show($"{messageBoxText}", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resault == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
            }
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
