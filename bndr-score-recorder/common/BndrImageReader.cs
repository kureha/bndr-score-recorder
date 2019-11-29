﻿using bndr_score_recorder.common.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bndr_score_recorder.common
{
    class BndrImageReader
    {
        // logger
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // crop path suffix
        private static readonly string SUFFIX_CROPNAME_TITLE = ".title";
        private static readonly string SUFFIX_CROPNAME_SCORE = ".score";
        private static readonly string SUFFIX_CROPNAME_MAXCOMBO = ".maxcombo";
        private static readonly string SUFFIX_CROPNAME_LEVEL = ".level";

        internal static Music AnalyzeBndrImage(string screenshotImageFilePath, string destDirPath)
        {
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
                logger.Info("Target directory is still exists, delete derectory.");
                Directory.Delete(screenshotDestDirPath, true);
            }
            Directory.CreateDirectory(screenshotDestDirPath);

            // Move image file to dest directory
            string scrennShotImageFileDestPath = screenshotDestDirPath + Path.DirectorySeparatorChar + Path.GetFileName(screenshotImageFilePath);
            logger.Info("Move screen shot file. Destination path = " + scrennShotImageFileDestPath);
            File.Move(screenshotImageFilePath, scrennShotImageFileDestPath);

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
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_TITLE,
                "840x40+615+70");
            logger.Info("Title = " + titleString);

            // Score read
            string scoreString = OcrReader.ReadFromImageFileOnlyNumber(
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_SCORE,
                "125x250+1440+450");
            logger.Info("Score = " + scoreString);

            // Max combo read
            string maxComboString = OcrReader.ReadFromImageFileOnlyNumber(
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_MAXCOMBO,
                "120x40+1650+595");
            logger.Info("Max combo = " + maxComboString);

            // Level read
            string levelString = OcrReader.ReadFromImageFileOnlyNumber(
                scrennShotImageFileDestPath,
                SUFFIX_CROPNAME_LEVEL,
                "70x45+1615+65");
            logger.Info("Level = " + levelString);

            logger.Info("OCR read section end.");

            // create return value start
            logger.Info("Music score result creation start.");
            Music analyzedMusic = new Music
            {
                title = titleString,
                level = int.Parse(levelString)
            };
            analyzedMusic.CreateIdFromTitle();

            analyzedMusic.scoreResultList.Add(ScoreResult.Parse(scoreString));
            analyzedMusic.scoreResultList[0].maxCombo = int.Parse(maxComboString);
            logger.Info("Musc score result creation end.");

            return analyzedMusic;
        }
    }
}
