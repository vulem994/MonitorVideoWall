
using MVW_ClassLibrary.Common.DtoModels;
using MVW_ClassLibrary.Common.DtoModels.HelperModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Mockup
        #region Whole Configuration
        DtoMainConfiguration config = new DtoMainConfiguration()
        {
            SmartWallsList = new ObservableCollection<DtoSmartWall>()
            {
                new DtoSmartWall()
                {
                     Name = "SmartWall 1",
            MonitorsList = new ObservableCollection<DtoMonitor>()
            {
                new DtoMonitor(){Name = "Monitor 1", InchesDiagonalSize= 20, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 2", InchesDiagonalSize= 20, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 3", InchesDiagonalSize= 20, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},
                 new DtoMonitor(){Name = "Monitor 4", InchesDiagonalSize= 25, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 5", InchesDiagonalSize= 30, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 6", InchesDiagonalSize= 35, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},
                 new DtoMonitor(){Name = "Monitor 7", InchesDiagonalSize= 20, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 8", InchesDiagonalSize= 20, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 9", InchesDiagonalSize= 20, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},  
                new DtoMonitor(){Name = "Monitor 10", InchesDiagonalSize= 20, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 11", InchesDiagonalSize= 20, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 12", InchesDiagonalSize= 20, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},
            },
            PresetsList = new ObservableCollection<DtoPreset>()
            {
                 new DtoPreset("Preset 1"),
                 new DtoPreset("Preset 2"),
                 new DtoPreset("Preset 3"),
            },
            InchesRatio = 0.05,
            AspectRatio = AspectRatioModel.Ar1x1Model,
                }
            }
        };
        #endregion
        #region Just SmartWall
        DtoSmartWall SmartWallMockup = new DtoSmartWall()
        {
            Name = "SmartWall 1",
            MonitorsList = new ObservableCollection<DtoMonitor>()
            {
                new DtoMonitor(){Name = "Monitor 1", InchesDiagonalSize= 20, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 2", InchesDiagonalSize= 20, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 3", InchesDiagonalSize= 20, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},
                 new DtoMonitor(){Name = "Monitor 4", InchesDiagonalSize= 25, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 5", InchesDiagonalSize= 30, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 6", InchesDiagonalSize= 35, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},
                 new DtoMonitor(){Name = "Monitor 7", InchesDiagonalSize= 20, StartPointInchesX=1,StartPointInchesY=2, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 8", InchesDiagonalSize= 20, StartPointInchesX=3,StartPointInchesY=4, AspectRatio = AspectRatioModel.Ar4x3Model},
                new DtoMonitor(){Name = "Monitor 9", InchesDiagonalSize= 20, StartPointInchesX=5,StartPointInchesY=6, AspectRatio = AspectRatioModel.Ar16x9Model},
            },
            PresetsList = new ObservableCollection<DtoPreset>()
            {
                 new DtoPreset(){Name = "Priset 1"},
                 new DtoPreset(){Name = "Priset 2"},
            },
            InchesRatio = 0.05,
            AspectRatio = AspectRatioModel.Ar1x1Model,
        };
        #endregion



        public MainWindow()
        {
            InitializeComponent();
            //MonitorPositionOrganisation.SetSmartWall(SmartWallMockup);
            //testControl.SetSmartWall(SmartWallMockup);
            mainMockup_UserControl.SetConfig(config);
        }

    }
}
