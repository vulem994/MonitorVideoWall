using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVW_ClassLibrary.Common.EventHandlers
{
    public class MVW_UserActionEventArgument<TObjectInstanceType, TActionType>
    {
        public MVW_UserActionEventArgument(object inObjectCaller, TObjectInstanceType inSelectedObjectInstance, TActionType inActionType, List<TObjectInstanceType> inMultiSelectedObjectInstance = null)
        {
            ObjectCaller = inObjectCaller;
            SelectedObjectInstance = inSelectedObjectInstance;
            ActionType = inActionType;
            MultiSelectedObjectInstance = inMultiSelectedObjectInstance;

        }
        public TActionType ActionType { get; set; }
        public TObjectInstanceType SelectedObjectInstance { get; set; }
        public List<TObjectInstanceType> MultiSelectedObjectInstance { get; set; }
        public object ObjectCaller { get; set; } 
    }
}
