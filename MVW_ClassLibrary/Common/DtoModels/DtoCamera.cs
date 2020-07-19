
using MVW_ClassLibrary.Common.DtoModels.CommonModels;
using MVW_ClassLibrary.Common.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVW_ClassLibrary.Common.DtoModels
{
    public class DtoCamera : ALogicalChildrenClass, INotifyPropertyChanged
    {
        #region -Name- property
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -ConnectedCameraGUID- property
        private Guid _ConnectedCameraGUID;
        public Guid ConnectedCameraGUID
        {
            get { return _ConnectedCameraGUID; }
            set
            {
                if (_ConnectedCameraGUID != value)
                {
                    _ConnectedCameraGUID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        //NotSavingProps

        #region -ConnectedCamera- property
        //[JsonIgnore]
        //private Item _ConnectedCamera;
        //[JsonIgnore]
        //public Item ConnectedCamera
        //{
        //    get { return _ConnectedCamera; }
        //    set
        //    {
        //        if (_ConnectedCamera != value)
        //        {
        //            _ConnectedCamera = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        #endregion

        #region -ParentPresetSettings- property
        [JsonIgnore]
        private DtoPresetSettings ParentPresetSettings;
        #endregion

        #region Context menu
        [JsonIgnore]
        bool isContextMenuInitialized = false;
        [JsonIgnore]
        public ContextMenu cameraContextMenu = new ContextMenu();
        [JsonIgnore]
        public MenuItem miEditCamera = new MenuItem() { Header = "Edit camera" };
        [JsonIgnore]
        public MenuItem miRemoveCamera = new MenuItem() { Header = "Remove camera" };
        [JsonIgnore]
        public MenuItem miOptions2 = new MenuItem() { Header = "Options2" };
        #endregion



        public DtoCamera()
        {
            InitializeContextMenu();
        }

        public DtoCamera(string inName,Guid inGuid, DtoPresetSettings inParentPresetSettings)
        {
            Name = inName;
            ConnectedCameraGUID = inGuid;
            //ConnectedCamera = inCamera;


            SetParentPresetSettings(inParentPresetSettings);
            //ParentArea = inParentArea;
            //ParentList = inParentList;
            InitializeContextMenu();
        }

        //Initialize

        #region Initialize Context menu
        private void InitializeContextMenu()
        {
            //miOptions2.Click += MiAddOutput_Click;
            miEditCamera.Click += MiEditCamera_Click;
            miRemoveCamera.Click += MiRemoveCamera_Click;

            //lprCameraContextMenu.Items.Add(miEditCamera);
            cameraContextMenu.Items.Add(miRemoveCamera);
            cameraContextMenu.Items.Add(new Separator());
            cameraContextMenu.Items.Add(miOptions2);
            isContextMenuInitialized = true;
        }

        private void MiRemoveCamera_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void MiEditCamera_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }


        #endregion


        //work with this class     
        #region Remove this camera from parent list
        public void RemoveMeFromParentList()
        {
            if (ParentPresetSettings != null)
            {
                //if (ParentArea.EntranceLPRCamerasList != null && ParentArea.EntranceLPRCamerasList == ParentList)
                //{
                //    ParentArea.RemoveCameraFromEntranceCamerasList(this);
                //}
                //else if (ParentArea.ExitLPRCamerasList != null && ParentArea.ExitLPRCamerasList == ParentList)
                //{
                //    ParentArea.RemoveCameraFromExitCamerasList(this);
                //}
            }
        }
        #endregion



        //Generate on load functions
        #region Generate connected Camera Item
        public void GenerateConnectedCameraItem()
        {
            if (ConnectedCameraGUID != Guid.Empty)
            {
                try
                {
                    //ConnectedCamera = Configuration.Instance?.GetItem(ConnectedCameraGUID, Kind.Camera);
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        #region Set parent list
        public void SetParentPresetSettings(DtoPresetSettings inParentPresetSettings)
        {
            ParentPresetSettings = inParentPresetSettings;
        }
        #endregion



        // Logical (base class) properties
        #region Logical Name
        [JsonIgnore]
        public override string LogicalName
        {
            get
            {
                return Name;
            }
        }
        #endregion
        #region Logical type
        [JsonIgnore]
        public override ELogicalChildrenClassInstanceType Type
        {
            get
            {
                return ELogicalChildrenClassInstanceType.Camera;
            }
        }
        #endregion
        #region Logical context menu
        [JsonIgnore]
        public override ContextMenu LogicalContextMenu
        {
            get
            {
                if (isContextMenuInitialized)
                {
                    return cameraContextMenu;
                }
                else
                {
                    InitializeContextMenu();
                    return null;
                }
            }
        }
        #endregion
        #region Logical children
        [JsonIgnore]
        public override ObservableCollection<ALogicalChildrenClass> LogicalChildren
        {
            get
            {
                return null;
            }
        }
        #endregion
        #region LogicalTreviewBackground
        [JsonIgnore]
        public override SolidColorBrush LogicalTreviewBackground
        {
            get
            {
                return fullTransparent;//white full transparency (no color)
            }
        }
        #endregion



        //TODO: Ubaciti Actions executed!!!

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
