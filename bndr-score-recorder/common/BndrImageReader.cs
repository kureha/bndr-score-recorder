using BndrScoreRecorder.common.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BndrScoreRecorder.common
{
    class BndrImageReader
    {
        // logger
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // crop path suffix
        private const string SUFFIX_CROPNAME_TITLE = ".title";
        private const string SUFFIX_CROPNAME_SCORE = ".score";
        private const string SUFFIX_CROPNAME_MAXCOMBO = ".maxcombo";
        private const string SUFFIX_CROPNAME_LEVEL = ".level";

        // debug mode (dry run for files)
        public static bool DEBUG_MODE = false;

        /// <summary>
        /// 画像ファイルを解析し、Musicオブジェクトを返却する。
        /// </summary>
        /// <param name="setting">Exeのパス等が格納されたSettingおbジェクト</param>
        /// <param name="screenshotImageFilePath">解析したい画像ファイルのパス</param>
        /// <param name="destDirPath">アプリケーションのデータ格納先ディレクトリパス</param>
        /// <param name="analyzeAllFile">true:全ファイルを解析、false:既に解析済みファイルがあればスキップ</param>
        /// <returns>Musicオブジェクト</returns>
        internal static Music AnalyzeBndrImage(Setting setting, string screenshotImageFilePath, string destDirPath, bool analyzeAllFile)
        {
            // Check setting object
            if (setting == null)
            {
                logger.Error("Setting object is null!");
                return null;
            }

            // File path check
            logger.Info("Screenshot image file = " + screenshotImageFilePath);
            if (File.Exists(screenshotImageFilePath) == false)
            {
                string errorMessage = "Screenshot Image File is not exists. FilePath = " + screenshotImageFilePath;
                logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            // If dest directory is not exists, create directory
            string screenshotDestDirPath = destDirPath 
                + Path.DirectorySeparatorChar 
                + Path.GetFileNameWithoutExtension(screenshotImageFilePath);
            logger.Info("Screenshot dest directory path = " + screenshotDestDirPath);
            if (Directory.Exists(screenshotDestDirPath) == true)
            {
                if (analyzeAllFile == true)
                {
                    logger.Info("Target directory is still exists, delete derectory.");
                    Directory.Delete(screenshotDestDirPath, true);
                }
                else
                {
                    logger.Info("Target directory is still exists, skip regist.");
                    return null;
                }
            }
            Directory.CreateDirectory(screenshotDestDirPath);

            // Move image file to dest directory
            string scrennShotImageFileDestPath = screenshotDestDirPath + Path.DirectorySeparatorChar + Path.GetFileName(screenshotImageFilePath);
            if (DEBUG_MODE == true)
            {
                logger.Info("Copy screen shot file. Destination path = " + scrennShotImageFileDestPath);
                if (File.Exists(scrennShotImageFileDestPath) == true)
                {
                    if (analyzeAllFile == true)
                    {
                        logger.Info("Overwrite copy file.");
                        File.Delete(scrennShotImageFileDestPath);
                    }
                    else
                    {
                        logger.Info("Target file is still exists, skip regist.");
                        return null;
                    }
                }
                File.Copy(screenshotImageFilePath, scrennShotImageFileDestPath);
            } else
            {
                logger.Info("Move screen shot file. Destination path = " + scrennShotImageFileDestPath);
                File.Move(screenshotImageFilePath, scrennShotImageFileDestPath);
            }

            // Dest file path check
            logger.Info("OCR read screenshot image file = " + scrennShotImageFileDestPath);
            if (File.Exists(scrennShotImageFileDestPath) == false)
            {
                string errorMessage = "OCR read creenshot Image File is not exists. FilePath = " + scrennShotImageFileDestPath;
                logger.Error(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            logger.Info("OCR read section start.");

            // Title read
            string titleString = OcrReader.ReadFromImageFileJapaneseLang(
                setting.pathImageMagickConvertExe,
                setting.pathTesseractExe,
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_TITLE,
                "840x40+615+70");
            logger.Info("Title = " + titleString);

            // Difficult code read
            string difficultString = OcrReader.ReadFromImageFileJapaneseLang(
                setting.pathImageMagickConvertExe,
                setting.pathTesseractExe,
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_TITLE,
                "140x35+470+70");
            logger.Info("Difficult = " + difficultString);

            // Score read
            string scoreString = OcrReader.ReadFromImageFileOnlyNumber(
                setting.pathImageMagickConvertExe,
                setting.pathTesseractExe,
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_SCORE,
                "125x250+1440+450");
            logger.Info("Score = " + scoreString);

            // Max combo read
            string maxComboString = OcrReader.ReadFromImageFileOnlyNumber(
                setting.pathImageMagickConvertExe,
                setting.pathTesseractExe,
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_MAXCOMBO,
                "120x40+1650+595");
            logger.Info("Max combo = " + maxComboString);

            // Level read
            string levelString = OcrReader.ReadFromImageFileOnlyNumber(
                setting.pathImageMagickConvertExe,
                setting.pathTesseractExe,
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_LEVEL,
                "70x45+1615+65");
            logger.Info("Level = " + levelString);

            logger.Info("OCR read section end.");

            // Create return value start
            logger.Info("Music score result creation start.");
            Music analyzedMusic = new Music
            {
                title = titleString,
                difficult = difficultString,
                level = int.Parse(levelString)
            };

            // Create Hashed OCR Data
            analyzedMusic.CreateHashedOcrDataFromTitleAndDifficult();

            analyzedMusic.scoreResultList.Add(ScoreResult.Parse(scoreString, scrennShotImageFileDestPath.Replace(destDirPath, string.Empty)));
            analyzedMusic.scoreResultList[0].maxCombo = int.Parse(maxComboString);
            logger.Info("Musc score result creation end.");

            return analyzedMusic;
        }
    }
}
