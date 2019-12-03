using BndrScoreRecorder.common.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder
{
    [DataContract]
    public class Setting
    {
        // Tesseract Exe path
        [DataMember]
        public string pathTesseractExe;

        // ImageMagick Convert Exe path
        [DataMember]
        public string pathImageMagickConvertExe;

        // OCR Reader Settings
        [DataMember]
        public List<OcrSetting> ocrSettingList = new List<OcrSetting>();

        // Default ocr setting
        public OcrSetting defaultOcrSetting;

        /// <summary>
        /// JSON形式のファイルから設定を読み込み格納する。エラー時はNullが返却。
        /// </summary>
        /// <param name="jsonFilePath">JSONファイルのパス</param>
        /// <returns>JSONから読み込んだ結果のオブジェクト、エラー時はNull。</returns>
        public static Setting ParseFromFile(string jsonFilePath)
        {
            Setting setting = null;

            // Check file exists
            if (File.Exists(jsonFilePath) == false)
            {
                // If file is not exists, return null
                return setting;
            }

            // Execute deserialize
            try
            {
                using (FileStream fileStream = new FileStream(jsonFilePath, FileMode.Open))
                {
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Setting));
                    setting = (Setting)jsonSerializer.ReadObject(fileStream);
                }
            } catch (Exception)
            {

            }

            // Setting default OCR setting
            if (setting.ocrSettingList != null)
            {
                foreach (OcrSetting ocrSetting in setting.ocrSettingList)
                {
                    if (ocrSetting.isDefault == true)
                    {
                        setting.defaultOcrSetting = ocrSetting;
                        break;
                    }
                }
            }

            return setting;
        }

        /// <summary>
        /// 対象のオブジェクトを設定としてファイルに保存する。
        /// </summary>
        /// <param name="setting">設定オブジェクト</param>
        /// <param name="jsonFilePath">保存先パス</param>
        public static void SaveToFile(Setting setting, string jsonFilePath)
        {
            // Execute deserialize
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(jsonFilePath))
                using (MemoryStream memoryStream = new MemoryStream())
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Setting));
                    jsonSerializer.WriteObject(memoryStream, setting);
                    memoryStream.Position = 0;

                    string jsonString = streamReader.ReadToEnd();
                    streamWriter.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
