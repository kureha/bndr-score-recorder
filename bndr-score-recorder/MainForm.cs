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

        /// <summary>
        /// 初期化。
        /// </summary>
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

            // Build music tree
            BuildMusicTreeView();
        }

        /// <summary>
        /// 楽曲リストを表示します
        /// </summary>
        private void BuildMusicTreeView()
        {
            logger.Info("Build tree view start.");

            // Initialize component
            MusicTreeView.Nodes.Clear();

            // Get music list
            logger.Info("Load music data from database start.");

            MusicDao musicDao = new MusicDao(databaseFilePath);
            List<Music> musicList = musicDao.selectMusicList();

            logger.Info("Load music data from database end.");

            // TreeNode(Level) object
            TreeNode targetLevelNode = null;
            foreach (Music music in musicList)
            {
                // if level object is null, create.
                if (levelList.Contains(music.level) == false)
                {
                    logger.Info("Insert level list = " + music.level);
                    targetLevelNode = new TreeNode(music.level.ToString());
                    targetLevelNode.Tag = music.level;
                    MusicTreeView.Nodes.Add(targetLevelNode);
                }

                // MusicNode object
                TreeNode musicNode = new TreeNode("[" + music.difficult + "] " + music.title);
                musicNode.Tag = music.id;

                // Insert music data to tree node
                targetLevelNode.Nodes.Add(musicNode);
            }

            logger.Info("Build tree view end.");
        }

        /// <summary>
        /// 特定フォルダ内の画像すべてに対して解析を行う。
        /// </summary>
        private void AnalyzeScore()
        {
            logger.Info("Analyze score start.");

            // Select target folder
            string selectedPath;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
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

                // Try to get registerd music
                Music registeredMusic = musicDao.selectById(analyzedMusic.id);
                if (registeredMusic == null)
                {
                    logger.Info("This is new regist music.");
                }
                else
                {
                    logger.Info("This is registered music.");
                    // Load title from DB
                    analyzedMusic.title = registeredMusic.title;
                    analyzedMusic.difficult = registeredMusic.difficult;
                    analyzedMusic.level = registeredMusic.level;
                }

                // Confirm data
                using (RegistScoreConfirmForm confirmForm = new RegistScoreConfirmForm(ref analyzedMusic, ref registeredMusic))
                {
                    if (DialogResult.OK == confirmForm.ShowDialog())
                    {
                        logger.Info("DialogResult is ok, regist score data.");
                        musicDao.InsertOrReplace(analyzedMusic);
                    }
                    else
                    {
                        logger.Info("DialogResult is cancel, skip score data.");
                    }
                }

            }
            logger.Info("Analyze end.");
            logger.Info("Analyze score end.");
        }

        /// <summary>
        /// スコア解析実行呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyzeScore();
        }
    }
}
