using MVW_ClassLibrary.Common.Enumerations;
using MVW_ClassLibrary.Common.EventHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVW_ClassLibrary.Common.DtoModels
{
    public class DtoMainConfiguration : INotifyPropertyChanged
    {
        #region -SmartWallsList- property
        private ObservableCollection<DtoSmartWall> _SmartWallsList;
        public ObservableCollection<DtoSmartWall> SmartWallsList
        {
            get { return _SmartWallsList; }
            set
            {
                if (_SmartWallsList != value)
                {
                    _SmartWallsList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        public DtoMainConfiguration()
        {
            InitializeClass();
            //InitializeMockupInfo(); //MOCKUP
        }
        // Initialize
        #region InitializeClass
        private void InitializeClass()
        {
            if (SmartWallsList == null)
            {
                SmartWallsList = new ObservableCollection<DtoSmartWall>();
            }
        }
        #endregion

        //Work with list functions
        #region Add & Remove SmartWall functions
        public bool AddSmartWall(DtoSmartWall inSmartWall)
        {
            if (SmartWallsList == null)
            {
                SmartWallsList = new ObservableCollection<DtoSmartWall>();
            }
            if (SmartWallsList != null && !SmartWallsList.Contains(inSmartWall))
            {
                SmartWallsList.Add(inSmartWall);
                inSmartWall.EizoActionCalled += InSmartWall_EizoActionCalled;
                return true;
            }
            return false;
        }

        public bool RemoveSmartWall(DtoSmartWall inSmartWall)
        {
            if (SmartWallsList != null && SmartWallsList.Contains(inSmartWall))
            {
                SmartWallsList.Remove(inSmartWall);
                inSmartWall.EizoActionCalled -= InSmartWall_EizoActionCalled;
                return true;
            }
            return false;
        }
        #endregion

        //Generate on Load Functions
        #region Generate all smart walls function
        public void GenerateAllSmartWalls()
        {
            if (SmartWallsList != null && SmartWallsList.Count > 0)
            {
                foreach (var item in SmartWallsList)
                {
                    item.GenerateAllPresetsAndMonitors();
                    item.EizoActionCalled += InSmartWall_EizoActionCalled;
                }
            }
        }
        #endregion

        //Events
        #region Smart Wall List events
        private void InSmartWall_EizoActionCalled(object sender, EventHandlers.MVW_UserActionEventArgument<DtoSmartWall, Enumerations.EMVWActions> e)
        {
            NotifyEizoActionCalled(e.ObjectCaller, this, e.ActionType);
        }
        #endregion

        //Other
        #region Mockup
        private void InitializeMockupInfo()
        {
            if (SmartWallsList == null)
            {
                SmartWallsList = new ObservableCollection<DtoSmartWall>();
            }

            #region Prisets
            List<DtoPreset> tmpSW1Prisets = new List<DtoPreset>()
            {
                 new DtoPreset(){Name = "Priset 1"},
                 new DtoPreset(){Name = "Priset 2"},
            };

            List<DtoPreset> tmpSW2Prisets = new List<DtoPreset>()
            {
                 new DtoPreset(){Name = "Priset 3"},
                 new DtoPreset(){Name = "Priset 4"},
            };
            #endregion

            #region Monitors
            List<DtoMonitor> tmpSW1Monitors = new List<DtoMonitor>()
            {
                new DtoMonitor(){Name = "Monitor 1"},
                new DtoMonitor(){Name = "Monitor 2"},
                new DtoMonitor(){Name = "Monitor 3"},
            };
            List<DtoMonitor> tmpSW2Monitors = new List<DtoMonitor>()
            {
                new DtoMonitor(){Name = "Monitor 4"},
                new DtoMonitor(){Name = "Monitor 5"},
                new DtoMonitor(){Name = "Monitor 6"},
            };
            #endregion

            #region Smart Walls
            DtoSmartWall tmpSmartWall1 = new DtoSmartWall() { Name = "SmartWall 1", MonitorsList = new ObservableCollection<DtoMonitor>(tmpSW1Monitors), PresetsList = new ObservableCollection<DtoPreset>(tmpSW1Prisets) };
            DtoSmartWall tmpSmartWall2 = new DtoSmartWall() { Name = "SmartWall 2", MonitorsList = new ObservableCollection<DtoMonitor>(tmpSW2Monitors), PresetsList = new ObservableCollection<DtoPreset>(tmpSW2Prisets) };
            SmartWallsList.Add(tmpSmartWall1);
            SmartWallsList.Add(tmpSmartWall2);
            #endregion
        }
        #endregion

        //Actions Called
        #region EizoAction Called Event
        public event EventHandler<MVW_UserActionEventArgument<DtoMainConfiguration, EMVWActions>> EizoActionCalled;

        public void NotifyEizoActionCalled(object inObjectCaller, DtoMainConfiguration inSelectedObjectInstance, EMVWActions inActionType)
        {
            EizoActionCalled?.Invoke(this, new MVW_UserActionEventArgument<DtoMainConfiguration, EMVWActions>(inObjectCaller, inSelectedObjectInstance, inActionType));
        }
        #endregion

        //Property changer
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
