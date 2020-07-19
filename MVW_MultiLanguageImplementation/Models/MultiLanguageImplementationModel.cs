using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVW_MultiLanguageImplementation.Models
{
    public class MultiLanguageImplementationModel : INotifyPropertyChanged
    {
        #region Single Multilanguage Instance
        static private MultiLanguageImplementationModel singleMultiLanguageInstance = null;
        public static MultiLanguageImplementationModel SingleMultiLanguageInstance
        {
            get
            {
                if (singleMultiLanguageInstance == null)
                {
                    singleMultiLanguageInstance = new MultiLanguageImplementationModel();
                }
                return singleMultiLanguageInstance;
            }
        } 
        #endregion

        //Manager
        #region -Res_Manager- property
        private ResourceManager _Res_Manager;
        public ResourceManager Res_Manager
        {
            get { return _Res_Manager; }
            set
            {
                if (_Res_Manager != value)
                {
                    _Res_Manager = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //Culture //NOT IMPLEMENTED
        #region -Culture_Info- property
        private CultureInfo _Culture_Info;
        public CultureInfo Culture_Info
        {
            get { return _Culture_Info; }
            set
            {
                if (_Culture_Info != value)
                {
                    _Culture_Info = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion


        private MultiLanguageImplementationModel()
        {
            UpdateResources();
        }

        #region Update Resources function
        public void UpdateResources(string cultureName = "")
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Res_Manager = new ResourceManager(typeof(MVW_MultiLanguageImplementation.LanguageResources.Resource));
            NotifyResourcesUpdated();
        }
        #endregion

        #region Get String From Resources function
        public String GetStringFromResources(string inStringKey = "")
        {
            return Res_Manager.GetString(inStringKey, System.Threading.Thread.CurrentThread.CurrentUICulture);
            //return Res_Manager.GetString(inStringKey, System.Threading.Thread.CurrentThread.CurrentCulture);   
        } 
        #endregion



        //Savings Need Event
        #region ResourceUpdated Handler & Notification
        public event EventHandler ResourceUpdated;
        private void NotifyResourcesUpdated()
        {
            ResourceUpdated?.Invoke(this, new EventArgs());
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
