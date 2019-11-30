using BndrScoreRecorder.common;
using BndrScoreRecorder.common.entity;
using BndrScoreRecorder.common.tesseract;
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

namespace BndrScoreRecorder
{
    public partial class Form1 : Form
    {
        // logger
        private log4net.ILog logger;

        // screenshot data folder path
        private string dataFolderPath;

        // sqlite database path
        private string databaseFilePath;

        public Form1()
        {
            // Create log4net instance
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            // Initialize log4net
            log4net.Config.BasicConfigurator.Configure();

            // create screenshot data folder path
            dataFolderPath = System.Windows.Forms.Application.StartupPath
                + Path.DirectorySeparatorChar 
                + "data";

            databaseFilePath = dataFolderPath 
                + Path.DirectorySeparatorChar 
                + "bndr-score-recorder.db";

            // enable development mode
            BndrImageReader.DEBUG_MODE = true;

            // Initialize component
            InitializeComponent();
        }

        private void AnalyzeScoreButton_Click(object sender, EventArgs e)
        {
            // Select target folder
            string selectedPath;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()) {
                folderBrowserDialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = folderBrowserDialog.SelectedPath;
                    logger.Info("Selected folder = " + selectedPath);
                }
                else
                {
                    return;
                }
            };

            // Collect file list
            IEnumerable<string> screenshotFilePathList = Directory.EnumerateFiles(selectedPath);

            logger.Info("Target file list ... ");
            foreach (string filePath in screenshotFilePathList)
            {
                logger.Info(filePath);
            }
            logger.Info(string.Empty);

            // Parse and get music infos
            logger.Info("Analyze start.");
            List<Music> analyzedMusicList = new List<Music>();
            foreach (string filePath in screenshotFilePathList)
            {
                logger.Info("Analyze target file = " + filePath);
                analyzedMusicList.Add(BndrImageReader.AnalyzeBndrImage(filePath, dataFolderPath));
            }
            logger.Info("Analyze end.");

            // Show result
            StringBuilder resultStringBuilder = new StringBuilder();
            analyzedMusicList.ForEach(music => {
                resultStringBuilder.AppendLine(Music.ToJsonString(music));
            });

            AnalyzeResultTextBox.Text = resultStringBuilder.ToString();

            // Insert to database
            logger.Info("Open database path = " + databaseFilePath);
            MusicDao musicDao = new MusicDao(databaseFilePath);

            analyzedMusicList.ForEach(music => {
                musicDao.InsertOrReplace(music);
            });
        }
    }
}
