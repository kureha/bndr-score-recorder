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

        // screenshot data folder path
        private string dataFolderPath;

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

            // Initialize component
            InitializeComponent();
        }

        private void AnalyzeScoreButton_Click(object sender, EventArgs e)
        {
            // Select target folder
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = System.Windows.Forms.Application.StartupPath;
            string selectedPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
                logger.Info("Selected folder = " + selectedPath);
            }
            else
            {
                return;
            }

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
        }
    }
}
