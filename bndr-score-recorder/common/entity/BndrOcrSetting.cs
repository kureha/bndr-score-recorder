using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common.entity
{
    [DataContract]
    public class BndrOcrSetting
    {
        // Title用設定
        [DataMember]
        public OcrSetting TitleOcrSetting;

        // Difficult用設定
        [DataMember]
        public OcrSetting DifficultOcrSetting;

        // Result Notes用設定
        [DataMember]
        public OcrSetting ResultNotesOcrSetting;

        // Max Combo用設定
        [DataMember]
        public OcrSetting MaxComboOcrSetting;

        // Level用設定
        [DataMember]
        public OcrSetting LevelOcrSetting;

        // Score用設定
        [DataMember]
        public OcrSetting ScoreOcrSetting;

        // Is default flag
        [DataMember]
        public bool isDefault;

        /// <summary>
        /// コンストラクタ。すべての設定を初期化する。
        /// </summary>
        public BndrOcrSetting()
        {
            TitleOcrSetting = new OcrSetting();
            DifficultOcrSetting = new OcrSetting();
            ResultNotesOcrSetting = new OcrSetting();
            MaxComboOcrSetting = new OcrSetting();
            LevelOcrSetting = new OcrSetting();
            ScoreOcrSetting = new OcrSetting();
            isDefault = false;
        }

        /// <summary>
        /// Title OCR Optionを取得。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string getTitleOcrOption()
        {
            if (TitleOcrSetting == null)
            {
                return string.Empty;
            }
            else
            {
                return TitleOcrSetting.ImageMagickCropOption();
            }
        }

        /// <summary>
        /// Difficult OCR Optionを取得。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string getDifficultOcrOption()
        {
            if (DifficultOcrSetting == null)
            {
                return string.Empty;
            }
            else
            {
                return DifficultOcrSetting.ImageMagickCropOption();
            }
        }

        /// <summary>
        /// Result Notes OCR Optionを取得。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string getResultNotesOcrOption()
        {
            if (ResultNotesOcrSetting == null)
            {
                return string.Empty;
            }
            else
            {
                return ResultNotesOcrSetting.ImageMagickCropOption();
            }
        }

        /// <summary>
        /// Max Combo OCR Optionを取得。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string getMaxComboOcrOption()
        {
            if (MaxComboOcrSetting == null)
            {
                return string.Empty;
            }
            else
            {
                return MaxComboOcrSetting.ImageMagickCropOption();
            }
        }

        /// <summary>
        /// Level OCR Optionを取得。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string getLevelOcrOption()
        {
            if (LevelOcrSetting == null)
            {
                return string.Empty;
            }
            else
            {
                return LevelOcrSetting.ImageMagickCropOption();
            }
        }

        /// <summary>
        /// Score OCR Optionを取得。
        /// </summary>
        /// <returns>有効なImageMagick Cropオプション値</returns>
        public string getScoreOcrOption()
        {
            if (ScoreOcrSetting == null)
            {
                return string.Empty;
            }
            else
            {
                return ScoreOcrSetting.ImageMagickCropOption();
            }
        }
    }
}
