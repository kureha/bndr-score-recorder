using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common.tesseract
{
    class ImageMagickBridge : bridge.BridgeBase
    {
        private static readonly string OPTION_CROP = "-crop";

        /// <summary>
        /// ImageMagickを使用した画像切り出しを実施
        /// </summary>
        /// <param name="pathImageMagickExe">ImageMagickのconvert.extのパス</param>
        /// <param name="pathInputImageFile">切り出しを実施する画像パス</param>
        /// <param name="pathOutputImageFile">出力画像パス</param>
        /// <param name="cropOption">有効なImageMagick Cropオプション値</param>
        /// <returns>標準出力</returns>
        public static string CropExecute(string pathImageMagickExe, string pathInputImageFile, string pathOutputImageFile, string cropOption)
        {
            logger.Info("ImageMagick extcution start");

            string standardOutputMessage = string.Empty;

            using(Process process = new Process())
            {
                // create process
                logger.Info("ImageMagick ext path = " + pathImageMagickExe);
                process.StartInfo.FileName = pathImageMagickExe;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                // arguments
                process.StartInfo.Arguments = 
                    @"""" + pathInputImageFile + @"""" + ARG_SPLIT_CHAR
                    + ImageMagickBridge.OPTION_CROP + ARG_SPLIT_CHAR
                    + @"""" + cropOption + @"""" + ARG_SPLIT_CHAR
                    + @"""" + pathOutputImageFile + @"""";
                process.StartInfo.CreateNoWindow = false;

                logger.Info("ImageMagick arguments = " + process.StartInfo.Arguments);

                // execute
                process.Start();

                // get starndard output
                standardOutputMessage = process.StandardOutput.ReadToEnd();

                // wait
                process.WaitForExit();

                logger.Info(standardOutputMessage);
            }

            logger.Info("ImageMagick extcution end");

            // return stardard output
            return standardOutputMessage;
        }

    }
}
