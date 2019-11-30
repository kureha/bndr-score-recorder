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
    public partial class MainForm : Form
    {
        // logger
        private log4net.ILog logger;

        // screenshot data folder path
        private string dataFolderPath;

        // sqlite database path
        private string databaseFilePath;

        public MainForm()
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

            StringBuilder resultStringBuilder = new StringBuilder();
            MusicDao musicDao = new MusicDao(databaseFilePath);

            foreach (string filePath in screenshotFilePathList)
            {
                logger.Info("Analyze target file = " + filePath);
                Music analyzedMusic = BndrImageReader.AnalyzeBndrImage(filePath, dataFolderPath);
                AnalyzeResultTextBox.AppendText(Music.ToJsonString(analyzedMusic));
                AnalyzeResultTextBox.AppendText("\r\n");

                // Try to get registerd music
                Music registeredMusic = musicDao.selectById(analyzedMusic.id);
                if (registeredMusic == null)
                {
                    logger.Info("This is new regist music.");
                } else
                {
                    logger.Info("This is registered music.");
                    // Load title from DB
                    analyzedMusic.title = registeredMusic.title;
                    analyzedMusic.difficult = registeredMusic.difficult;
                    analyzedMusic.level = registeredMusic.level;
                }

                //TODO: confirm regist data
                using (RegistScoreConfirmForm confirmForm = new RegistScoreConfirmForm(ref analyzedMusic, ref registeredMusic))
                {
                    if (DialogResult.OK == confirmForm.ShowDialog())
                    {
                        logger.Info("DialogResult is ok, regist score data.");
                        musicDao.InsertOrReplace(analyzedMusic);
                    } else
                    {
                        logger.Info("DialogResult is cancel, skip score data.");
                    }
                }
                
            }
            logger.Info("Analyze end.");
        }
    }
}
