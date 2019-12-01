using BndrScoreRecorder.common.tesseract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common
{
    class OcrReader
    {
        // logger
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string MODE_ALL = "ALL";
        private static readonly string MODE_ONLY_NUMBER = "ONLY_NUMBER";
        private static readonly string MODE_JAPANESE_LANG = "JAPANESE_LANG";

        /// <summary>
        /// 画像から文字を読み取る。
        /// 対象文字はすべての文字範囲。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="outputSuffix">画像出力ファイル名の末尾追加文字列</param>
        /// <param name="cropValue">有効なImageMagick Cropオプション値</param>
        /// <returns>画像から読み取った文字列</returns>
        internal static string ReadFromImageFile(string imageMagickConvertExePath, string tesseractExePath, string filePath, string outputSuffix, string cropValue)
        {
            return ReadFromImageFile(imageMagickConvertExePath, tesseractExePath, filePath, outputSuffix, cropValue, MODE_ALL);
        }

        /// <summary>
        /// 画像から文字を読み取る。
        /// 対象文字はすべての文字範囲。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="outputSuffix">画像出力ファイル名の末尾追加文字列</param>
        /// <param name="cropValue">有効なImageMagick Cropオプション値</param>
        /// <returns>画像から読み取った文字列</returns>
        internal static string ReadFromImageFileJapaneseLang(string imageMagickConvertExePath, string tesseractExePath, string filePath, string outputSuffix, string cropValue)
        {
            return ReadFromImageFile(imageMagickConvertExePath, tesseractExePath, filePath, outputSuffix, cropValue, MODE_JAPANESE_LANG);
        }

        /// <summary>
        /// 画像から文字を読み取る。
        /// 対象文字は数字のみ。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="outputSuffix">画像出力ファイル名の末尾追加文字列</param>
        /// <param name="cropValue">有効なImageMagick Cropオプション値</param>
        /// <returns>画像から読み取った文字列</returns>
        internal static string ReadFromImageFileOnlyNumber(string imageMagickConvertExePath, string tesseractExePath, string filePath, string outputSuffix, string cropValue)
        {
            return ReadFromImageFile(imageMagickConvertExePath, tesseractExePath, filePath, outputSuffix, cropValue, MODE_ONLY_NUMBER);
        }

        /// <summary>
        /// 画像から文字を読み取る内部メソッド。直接呼出し不可。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="outputSuffix">画像出力ファイル名の末尾追加文字列</param>
        /// <param name="cropValue">有効なImageMagick Cropオプション値</param>
        /// <param name="mode">Mode値</param>
        /// <returns>画像から読み取った文字列</returns>
        private static string ReadFromImageFile(string imageMagickConvertExePath, string tesseractExePath, string filePath, string outputSuffix, string cropValue, string mode)
        {
            // return value
            string result = string.Empty;

            // standard output
            string standardOutput = string.Empty;

            // check file
            if (File.Exists(filePath) == false)
            {
                string errorMessage = "Convert file is not exists. FilePath =" + filePath;
                logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            // create text from argument filepath
            string imageMagickOutputFilePath = 
                Path.GetDirectoryName(filePath) +
                Path.DirectorySeparatorChar + 
                Path.GetFileNameWithoutExtension(filePath) +
                outputSuffix +
                Path.GetExtension(filePath);

            logger.Info("ImageMagick 0utput file path = " + imageMagickOutputFilePath);

            // crop image
            logger.Info("ImageMagick convert start");
            standardOutput = ImageMagickBridge.CropExecute(
                imageMagickConvertExePath,
                filePath,
                imageMagickOutputFilePath,
                cropValue);

            logger.Info(standardOutput);
            logger.Info("ImageMagick convert end");

            // check ImageMagick output file
            if (File.Exists(imageMagickOutputFilePath) == false)
            {
                string errorMessage = "ImageMagick output file is not exists. FilePath =" + imageMagickOutputFilePath;
                logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            // create ocr read file name
            string tesseractOutputFilePath =
                Path.GetDirectoryName(imageMagickOutputFilePath) + 
                Path.DirectorySeparatorChar + 
                Path.GetFileNameWithoutExtension(imageMagickOutputFilePath);

            logger.Info("Tesseract Output file path = " + tesseractOutputFilePath + TesseractBridge.SUFFIX_OUTPUT_FILE_NAME);

            // ocr read
            logger.Info("Tesseract OCR read start");
            if (mode == MODE_ALL)
            {
                standardOutput = TesseractBridge.ReadExecute(
                    tesseractExePath,
                    imageMagickOutputFilePath,
                    tesseractOutputFilePath
                    );
            }
            else if (mode == MODE_JAPANESE_LANG) {
                standardOutput = TesseractBridge.ReadJapaneseLangExecute(
                    tesseractExePath,
                    imageMagickOutputFilePath,
                    tesseractOutputFilePath
                    );
            } 
            else if (mode == MODE_ONLY_NUMBER)
            {
                standardOutput = TesseractBridge.ReadOnlyNumberExecute(
                    tesseractExePath,
                    imageMagickOutputFilePath,
                    tesseractOutputFilePath
                    );
            }


            logger.Info(standardOutput);
            logger.Info("Tesseract OCR read end");

            tesseractOutputFilePath = tesseractOutputFilePath + TesseractBridge.SUFFIX_OUTPUT_FILE_NAME;

            if (File.Exists(tesseractOutputFilePath) == false)
            {
                string errorMessage = "Tesseract output file is not exists. FilePath =" + tesseractOutputFilePath;
                logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            // read file
            using (StreamReader streamWriter = new StreamReader(tesseractOutputFilePath))
            {
                result = streamWriter.ReadToEnd();
            }

            return result.Trim();
        }
    }
}
