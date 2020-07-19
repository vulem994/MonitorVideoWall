
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVW_ControlsAndFormsLibrary.Forms
{
    /// <summary>
    /// Interaction logic for AddSmartWall_Form.xaml
    /// </summary>
    public partial class AddSmartWall_Form : Window, INotifyPropertyChanged
    {
        public AddSmartWall_Form(DtoSmartWall inSmartWall, EFormInitializeType type = EFormInitializeType.New)
        {
            this.DataContext = this;

            InitializeComponent();
            InitializeForm();
            if (inSmartWall != null && addSmartWall_uc != null)
            {
                addSmartWall_uc.SetSmartWallAndControlUIType(inSmartWall, type);
            }
            else
            {
                MessageBox.Show($"Problem with SmartWall");
                this.DialogResult = false;
                this.Close();
            }
        }

        //Initialize
        #region Initialize Form
        private void InitializeForm()
        {

        }
        #endregion

        //Events
        #region Buttons events
        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            var resault = addSmartWall_uc.SaveChanges();
            if (resault)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show($"SmartWall creation was not successful.");
                this.DialogResult = false;
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            var resault = MessageBox.Show($"You are about cancel creating new area. Are you sure you want to cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resault == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
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
