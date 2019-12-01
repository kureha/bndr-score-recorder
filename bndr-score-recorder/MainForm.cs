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

        // setting json file path
        private string settingFilePath;

        // setting object
        private Setting setting;

        // level tree node prefix
        private static readonly string PREFIX_LEVEL_TREE_NODE = "LEVEL_";

        /// <summary>
        /// 初期化。
        /// </summary>
        public MainForm()
        {
            // Create log4net instance
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            // Initialize log4net
            log4net.Config.BasicConfigurator.Configure();

            // Create screenshot data folder path
            dataFolderPath = System.Windows.Forms.Application.StartupPath
                + Path.DirectorySeparatorChar 
                + "data";

            databaseFilePath = dataFolderPath 
                + Path.DirectorySeparatorChar 
                + "bndr-score-recorder.db";

            settingFilePath = dataFolderPath
                + Path.DirectorySeparatorChar
                + "application-settings.json";

            // Read settings from file
            logger.Info("Read settings from file. setting file path = " + settingFilePath);
            setting = Setting.ParseFromFile(settingFilePath);

            if (setting == null)
            {
                logger.Info("Create new setting file.");
                MessageBox.Show("初期設定が必要です。");
                
                setting = new Setting();
                logger.Info("Initializing setting object complete.");

                using (SettingForm settingForm = new SettingForm(ref setting))
                {
                    if (settingForm.ShowDialog() == DialogResult.OK)
                    {
                        Setting.SaveToFile(setting, settingFilePath);
                        MessageBox.Show("設定を保存しました。");
                    } else
                    {
                        MessageBox.Show("初期設定が実施されませんでした。終了します。");
                        Application.Exit();
                    }
                }
            }

            // Enable development mode
            BndrImageReader.DEBUG_MODE = true;

            // Initialize component
            InitializeComponent();

            // Build music tree
            InitializeDataGridView();
            BuildMusicTreeView();
        }

        /// <summary>
        /// DataGridViewを初期化。
        /// </summary>
        private void InitializeDataGridView()
        {
            // Setup columns
            DataGridViewTextBoxColumn column;

            // DataSource clear
            ScoreDataGridView.DataSource = null;
            
            // EX Score
            column = new DataGridViewTextBoxColumn();
            column.Name = "exScore";
            column.HeaderText = "EX Score";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // MAX Combo
            column = new DataGridViewTextBoxColumn();
            column.Name = "maxCombo";
            column.HeaderText = "Max Combo";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // Perfect
            column = new DataGridViewTextBoxColumn();
            column.Name = "perfect";
            column.HeaderText = "Perfect";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // Perfect
            column = new DataGridViewTextBoxColumn();
            column.Name = "great";
            column.HeaderText = "Great";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // Perfect
            column = new DataGridViewTextBoxColumn();
            column.Name = "good";
            column.HeaderText = "Good";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // Perfect
            column = new DataGridViewTextBoxColumn();
            column.Name = "bad";
            column.HeaderText = "Bad";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // Perfect
            column = new DataGridViewTextBoxColumn();
            column.Name = "miss";
            column.HeaderText = "Miss";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);

            // Total Notes
            column = new DataGridViewTextBoxColumn();
            column.Name = "totalNotes";
            column.HeaderText = "Total Notes";
            column.DataPropertyName = column.Name;
            ScoreDataGridView.Columns.Add(column);
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
            List<int> levelList = new List<int>();

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
                    levelList.Add(music.level);
                    targetLevelNode.Tag = PREFIX_LEVEL_TREE_NODE + music.level;
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
        /// 特定フォルダ内の画像に対して解析を行う。
        /// </summary>
        /// <param name="analyzeAllFile">true:全ファイルを解析、false:既に解析済みファイルがあればスキップ</param>
        private void AnalyzeScore(bool analyzeAllFile)
        {
            logger.Info("Analyze score start. Analyze all file flag = " + analyzeAllFile);

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
                Music analyzedMusic = BndrImageReader.AnalyzeBndrImage(setting, filePath, dataFolderPath, analyzeAllFile);

                // If skip regist, goto next loop
                if (analyzedMusic == null)
                {
                    logger.Info("Regist music skipped, goto next file.");
                    continue;
                }

                // Try to get registerd music by Hashed OCR Data
                Music registeredMusic = musicDao.selectByHashedOcrData(analyzedMusic.hashedOcrData);
                if (registeredMusic == null)
                {
                    logger.Info("This is new regist music.");
                }
                else
                {
                    logger.Info("This is registered music.");
                    // Load title from DB
                    analyzedMusic.id = registeredMusic.id;
                    analyzedMusic.title = registeredMusic.title;
                    analyzedMusic.difficult = registeredMusic.difficult;
                    analyzedMusic.level = registeredMusic.level;
                    analyzedMusic.hashedOcrDataList = registeredMusic.hashedOcrDataList;
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
        /// スコア解析実行を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Analyze score
            AnalyzeScore(false);
            // Refresh music tree view
            BuildMusicTreeView();
            // Show complete message
            MessageBox.Show("楽曲の解析がすべて完了しました。");
        }

        /// <summary>
        /// 全曲のスコア解析実行を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("対象フォルダ内全てのファイルに対し、強制的に全データの解析を行います。場合によっては二重にスコアが登録される可能性もあります。よろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Analyze score
                AnalyzeScore(true);
                // Refresh music tree view
                BuildMusicTreeView();
                // Show complete message
                MessageBox.Show("楽曲の解析がすべて完了しました。");
            } else
            {
                MessageBox.Show("実行をキャンセルしました。");
            }
            
        }

        /// <summary>
        /// 楽曲が選択された際、そのスコアを隣のViewに表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            logger.Info("DataGridView attach start.");

            // Get selected node
            TreeNode selectedNode = MusicTreeView.SelectedNode;

            // Extract music id from tree node tag
            int? musicId;
            try
            {
                musicId = (int?)selectedNode.Tag;
            } catch (Exception)
            {
                musicId = null;
            }

            // If null or empty, abort this function
            if (musicId == null)
            {
                logger.Error("Music id is null.");
                return;
            } else
            {
                logger.Info("Music id = " + musicId);
            }

            // Load from database
            logger.Info("Load music data from database start.");
            MusicDao musicDao = new MusicDao(databaseFilePath);
            Music targetMusic = musicDao.selectByMusicId(musicId);
            logger.Info(Music.ToJsonString(targetMusic));
            logger.Info("Load music data from database end.");

            // Attach to datagridview
            ScoreDataGridView.DataSource = targetMusic.scoreResultList;

            logger.Info("DataGridView attach end.");
        }

        /// <summary>
        /// 設定を行うフォームを開く。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingForm settingForm = new SettingForm(ref setting))
            {
                if (settingForm.ShowDialog() == DialogResult.OK)
                {
                    Setting.SaveToFile(setting, settingFilePath);
                    MessageBox.Show("設定を保存しました。");
                }
            }
        }
    }
}
