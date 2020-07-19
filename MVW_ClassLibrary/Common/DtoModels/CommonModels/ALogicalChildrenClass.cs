using MVW_ClassLibrary.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MVW_ClassLibrary.Common.DtoModels.CommonModels
{
    public abstract class ALogicalChildrenClass
    {
        #region -LogicalName- property
        public abstract String LogicalName
        {
            get;
        }
        #endregion

        #region -Type- property
        public abstract ELogicalChildrenClassInstanceType Type
        {
            get;
        }
        #endregion

        #region -LogicalContextMenu- property
        public abstract ContextMenu LogicalContextMenu
        {
            get;
        }
        #endregion

        #region -LogicalChildren- property
        public abstract ObservableCollection<ALogicalChildrenClass> LogicalChildren
        {
            get;
        }
        #endregion

        #region -LogicalTreviewBackground- property
        public abstract SolidColorBrush LogicalTreviewBackground
        {
            get;
        }
        #endregion



        //Background static colors
        #region Colors
        //aplha 50 colors
        public static SolidColorBrush redColor = new SolidColorBrush(Color.FromArgb(50, 170, 1, 1));
        public static SolidColorBrush greenColor = new SolidColorBrush(Color.FromArgb(50, 1, 170, 1));
        public static SolidColorBrush darkGreenColor = new SolidColorBrush(Color.FromArgb(50, 8, 107, 8));
        public static SolidColorBrush lightBlueColor = new SolidColorBrush(Color.FromArgb(50, 8, 170, 255));
        public static SolidColorBrush yellowColor = new SolidColorBrush(Color.FromArgb(50, 226, 170, 15));
        //transparent
        public static SolidColorBrush fullTransparent = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255)); // da ne bih vrtacao null 
        #endregion

    }
}
