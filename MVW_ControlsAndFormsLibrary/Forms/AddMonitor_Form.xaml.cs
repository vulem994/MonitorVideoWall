
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ClassLibrary.Common.DtoModels.HelperModels;
using MVW_ControlsAndFormsLibrary.Common.Enumerations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVW_ControlsAndFormsLibrary.Forms
{
    /// <summary>
    /// Interaction logic for AddMonitor_Form.xaml
    /// </summary>
    public partial class AddMonitor_Form : Window, INotifyPropertyChanged
    {
        //Helper properties

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

        public AddMonitor_Form(DtoMonitor inMonitor, EFormInitializeType type = EFormInitializeType.New)
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeForm();

            if (inMonitor != null)
            {
                newMonitor_uc.SetMonitorAndControlUIType(inMonitor, type);
            }
            else
            {
                MessageBox.Show($"Problem with Monitor");
                this.DialogResult = false;
                this.Close();
            }
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
        }
        #endregion


        //Events
        #region Buttons Events
        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (newMonitor_uc != null)
            {
                var resault = newMonitor_uc.SaveChanges();
                if (resault)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Monitor is not created corectly.");
                    this.DialogResult = false;
                    this.Close();
                }
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
