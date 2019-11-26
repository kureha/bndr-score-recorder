using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.tesseract
{
    class ImageMagickBridge : bridge.BridgeBase
    {
        private static readonly string OPTION_CROP = "-crop";

        public static string CropExecute(string pathImageMagickExe, string pathInputImageFile, string pathOutputImageFile, string cropOption)
        {
            string standardOutputMessage = string.Empty;

            using(Process process = new Process())
            {
                // create process
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

                // execute
                process.Start();

                // get starndard output
                standardOutputMessage = process.StandardOutput.ReadToEnd();

                // wait
                process.WaitForExit();
            }

            // return stardard output
            return standardOutputMessage;
        }

    }
}
