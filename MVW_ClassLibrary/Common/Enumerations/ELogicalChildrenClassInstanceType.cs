using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVW_ClassLibrary.Common.Enumerations
{
    public enum ELogicalChildrenClassInstanceType
    {
        None = 0,

        SmartWall = 10,

        Monitor = 20,
        MonitorInstance = 21,

        Preset = 30,
        PresetInstance = 31,
        PresetSettings = 32,

        Camera = 40,

    }
}