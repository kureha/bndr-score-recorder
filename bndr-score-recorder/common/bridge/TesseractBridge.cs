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

        public static readonly string PARAM_OPTION_ONLY_NUMBER = "-c tessedit_char_whitelist=0123456789";


        /// <summary>
        /// Tesseractを使用した画像から数字を読んでファイル出力を実施。
        /// 対象文字はすべての文字範囲。
        /// </summary>
        /// <param name="pathTesseractExe">Tesseractのtesseract.extのパス</param>
        /// <param name="pathInputImageFile">読み取りを実施する画像パス</param>
        /// <param name="pathOutputTxtFile">出力テキストファイルパス</param>
        /// <returns></returns>
        public static string ReadExecute(string pathTesseractExe, string pathInputImageFile, string pathOutputTxtFile)
        {
            return ReadExecute(pathTesseractExe, pathInputImageFile, pathOutputTxtFile, string.Empty);
        }

        /// <summary>
        /// Tesseractを使用した画像から数字を読んでファイル出力を実施。
        /// 対象文字は数字のみ。
        /// </summary>
        /// <param name="pathTesseractExe">Tesseractのtesseract.extのパス</param>
        /// <param name="pathInputImageFile">読み取りを実施する画像パス</param>
        /// <param name="pathOutputTxtFile">出力テキストファイルパス</param>
        /// <returns></returns>
        public static string ReadOnlyNumberExecute(string pathTesseractExe, string pathInputImageFile, string pathOutputTxtFile)
        {
            return ReadExecute(pathTesseractExe, pathInputImageFile, pathOutputTxtFile, PARAM_OPTION_ONLY_NUMBER);
        }

        /// <summary>
        /// Tesseractを使用した画像から文字を読んでファイル出力を実施。直接呼出し付加。
        /// </summary>
        /// <param name="pathTesseractExe">Tesseractのtesseract.extのパス</param>
        /// <param name="pathInputImageFile">読み取りを実施する画像パス</param>
        /// <param name="pathOutputTxtFile">出力テキストファイルパス</param>
        /// <param name="extraOption">その他オプション</param>
        /// <returns></returns>
        private static string ReadExecute(string pathTesseractExe, string pathInputImageFile, string pathOutputTxtFile, string extraOption)
        {
            logger.Info("Tesseract extcution start");

            string standardOutputMessage = string.Empty;

            using(Process process = new Process())
            {
                // create process
                logger.Info("Tesseract ext path = " + pathTesseractExe);
                process.StartInfo.FileName = pathTesseractExe;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                // arguments
                process.StartInfo.Arguments = 
                    PARAM_NAME_TESSERACT_OPTION_PSM + ARG_SPLIT_CHAR
                    + PARAM_VALUE_TESSERACT_OPTION_PSM + ARG_SPLIT_CHAR
                    + extraOption + ARG_SPLIT_CHAR
                    + @"""" + pathInputImageFile + @"""" + ARG_SPLIT_CHAR
                    + @"""" + pathOutputTxtFile + @"""";
                process.StartInfo.CreateNoWindow = false;

                logger.Info("Tesseract arguments = " + process.StartInfo.Arguments);

                // execute
                process.Start();

                // get starndard output
                standardOutputMessage = process.StandardOutput.ReadToEnd();

                // wait
                process.WaitForExit();

                logger.Info(standardOutputMessage);
            }

            logger.Info("Tesseract extcution end");

            // return stardard output
            return standardOutputMessage;
        }

    }
}
