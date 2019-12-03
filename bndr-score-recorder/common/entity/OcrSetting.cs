using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common.entity
{
    [DataContract]
    public class OcrSetting
    {
        // Crop Position X
        [DataMember]
        public int positionX;

        // Crop Position Y
        [DataMember]
        public int positionY;

        // Crop Width
        [DataMember]
        public int width;

        // Crop Height
        [DataMember]
        public int height;

        // Is default flag
        [DataMember]
        public bool isDefault;

        // ImageMagick's option separater
        private const string OPTION_IMAGEMAGICK_RANGE_SEPARATOR = "x";
        private const string OPTION_IMAGEMAGICK_POSITION_SEPARATOR = "+";

        /// <summary>
        /// ImageMagick用のCropOptionを作成する。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string ImageMagickCropOption()
        {
            return
               width.ToString()
               + OPTION_IMAGEMAGICK_RANGE_SEPARATOR + height.ToString()
               + OPTION_IMAGEMAGICK_POSITION_SEPARATOR + positionX.ToString()
               + OPTION_IMAGEMAGICK_POSITION_SEPARATOR + positionY.ToString();
        }
    }
}
