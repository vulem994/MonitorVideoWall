using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVW_ClassLibrary.Common.DtoModels.HelperModels
{
    public class AspectRatioModel : INotifyPropertyChanged
    {
        //Ova klasa predstavlja Aspect Ratio odnos i ima value za odnos sirine i visine  npr. 16:9. --> widthR = 16, heightR=9

        #region -Title- property
        private String _Title;
        public String Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -WidthR- property
        private int _WidthR;
        public int WidthR
        {
            get { return _WidthR; }
            set
            {
                if (_WidthR != value)
                {
                    _WidthR = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -HeightR- property
        private int _HeightR;
        public int HeightR
        {
            get { return _HeightR; }
            set
            {
                if (_HeightR != value)
                {
                    _HeightR = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region -RelativeValue- property
        [JsonIgnore]
        public double RelativeValue
        {
            get { return WidthR / HeightR; }
        }
        #endregion

        public AspectRatioModel()
        {

        }

        //Static Models Type
        #region Static models type
        [JsonIgnore]
        public static AspectRatioModel Ar1x1Model { get; set; } = new AspectRatioModel()
        {
            Title = "1:1",
            WidthR = 1,
            HeightR = 1,
        };
        [JsonIgnore]
        public static AspectRatioModel Ar4x3Model { get; set; } = new AspectRatioModel()
        {
            Title = "4:3",
            WidthR = 4,
            HeightR = 3,
        };
        [JsonIgnore]
        public static AspectRatioModel Ar16x9Model { get; set; } = new AspectRatioModel()
        {
            Title = "16:9",
            WidthR = 16,
            HeightR = 9,
        };
        #endregion

        //Static list and dictonary with model types
        #region Static list and dictonary of all model types
        [JsonIgnore]
        public static List<AspectRatioModel> AllOptionsList { get; set; } = new List<AspectRatioModel>() {
            Ar1x1Model,
            Ar4x3Model,
            Ar16x9Model,
        };
        [JsonIgnore]
        public static Dictionary<string, AspectRatioModel> AllOptionsDictonary { get; set; } = new Dictionary<string, AspectRatioModel> {
            { "1:1", Ar1x1Model },
            { "4:3", Ar4x3Model },
            { "16:9", Ar16x9Model },
        };
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

    }//[class]
} //[namespace]





