using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common.tesseract
{
    class TesseractBridge : bridge.BridgeBase
    {
        public static readonly string SUFFIX_OUTPUT_FILE_NAME = ".txt";

        public static readonly string PARAM_NAME_TESSERACT_OPTION_PSM = "--psm";
        public static readonly string PARAM_VALUE_TESSERACT_OPTION_PSM = "6";

        public static string Execute(string pathTesseractExe, string pathInputImageFile, string pathOutputTxtFile)
        {
            string standardOutputMessage = string.Empty;

            using(Process process = new Process())
            {
                // create process
                process.StartInfo.FileName = pathTesseractExe;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                // arguments
                process.StartInfo.Arguments = 
                    PARAM_NAME_TESSERACT_OPTION_PSM + ARG_SPLIT_CHAR
                    + PARAM_VALUE_TESSERACT_OPTION_PSM + ARG_SPLIT_CHAR
                    + @"""" + pathInputImageFile + @"""" + ARG_SPLIT_CHAR
                    + @"""" + pathOutputTxtFile + @"""";
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
