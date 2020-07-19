using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVW_ClassLibrary.Common.Enumerations
{
    public enum EMVWActions
    {
        None = 0,

        //SmartWall actions
        AddSmartWall = 10,
        EditSmartWall = 11,
        RemoveSmartWall = 12,
        DisableSmartWall = 13,
        EditSmartWallLayout = 14,

        //Monitor actions
        AddMonitor = 20,
        EditMonitor = 21,
        RemoveMonitor = 22,
        DisableMonitor = 23,

        //Preset actions
        AddPreset = 30,
        EditPreset = 31,
        RemovePreset = 32,
        DisablePreset = 33,

        //PresetSettings actions
        EditPresetSettings= 35,

        //Other actions
        Options1 = 1001,
        Options2 = 1002,



        //Layout actions
        MonitorRectangleDraggingStarted = 2000,
        MonitorRectangleDraggingStoped = 2001,
        MonitorRectangleMoved = 2002,

        MonitorRectangleSelected = 2010,
        MonitorRectangleDeselected = 2011,

        MonitorRectangleEdited = 2020,

    }
}
