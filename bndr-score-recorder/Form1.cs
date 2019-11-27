using bndr_score_recorder.common;
using bndr_score_recorder.common.entity;
using bndr_score_recorder.common.tesseract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bndr_score_recorder
{
    public partial class Form1 : Form
    {
        // logger
        private log4net.ILog logger;

        public Form1()
        {
            // Create log4net instance
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            // Initialize log4net
            log4net.Config.BasicConfigurator.Configure();
            // Initialize component
            InitializeComponent();
        }

        private void AnalyzeScoreButton_Click(object sender, EventArgs e)
        {
            // image path variable
            string screenshotImageFilePath;
            string scoreString = string.Empty;
            string titleString = string.Empty;
            string maxComboString = string.Empty;
            string levelString = string.Empty;

            // read from dialog
            OpenFileDialog imagePathFileDialog = new OpenFileDialog();
            if (imagePathFileDialog.ShowDialog() == DialogResult.OK)
            {
                screenshotImageFilePath = imagePathFileDialog.FileName;
            }
            else
            {
                return;
            }

            // title read
            titleString = OcrReader.readFromImageFile(
                screenshotImageFilePath,
                ".title",
                "470x25+345+35");

            // score read
            scoreString = OcrReader.readFromImageFile(
                screenshotImageFilePath,
                ".score",
                "230x150+650+250");

            // max combo read
            maxComboString = OcrReader.readFromImageFileOnlyNumber(
                screenshotImageFilePath,
                ".maxcombo",
                "80x25+915+330");

            // level read
            levelString = OcrReader.readFromImageFileOnlyNumber(
                screenshotImageFilePath,
                ".level",
                "40x25+900+35");

            logger.Info("title = " + titleString);
            logger.Info("level = " + levelString);
            logger.Info("score = " + scoreString);
            logger.Info("max combo = " + maxComboString);

            Music musicAnalyzed = new Music();
            musicAnalyzed.title = titleString;
            musicAnalyzed.level = int.Parse(levelString);

            musicAnalyzed.scoreResultList.Add(ScoreResult.Parse(scoreString));
        }
    }
}
