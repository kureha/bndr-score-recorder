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
        private const string PREFIX_LEVEL_TREE_NODE = "LEVEL_";

        // Exception message
        private static readonly string EXCEPTION_MESSAGE_FORMAT = "予期せぬ例外が発生したため、処理を中断しました。" 
            + Environment.NewLine + "[Message]" 
            + Environment.NewLine  + "{0}"
            + Environment.NewLine + "[StackTrace]"
            + Environment.NewLine + "{1}";

        // Normal exit code
        private const int EXIT_CODE_NORMAL = 0;

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

                using (ExePathSettingForm settingForm = new ExePathSettingForm(ref setting))
                {
                    if (settingForm.ShowDialog() == DialogResult.OK)
                    {
                        setting.defaultBndrOcrSetting = new BndrOcrSetting();
                        Setting.SaveToFile(setting, settingFilePath);
                        MessageBox.Show("設定を保存しました。");
                    } else
                    {
                        MessageBox.Show("初期設定が実施されませんでした。終了します。");
                        Environment.Exit(EXIT_CODE_NORMAL);
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

            // Clear Type
            column = new DataGridViewTextBoxColumn();
            column.Name = "clearType";
            column.HeaderText = "Clear Type";
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
                TreeNode musicNode = new TreeNode(String.Format("[{0}] {1}", music.difficult, music.title));
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
        private void AnalyzeScoreFromDirectory(bool analyzeAllFile)
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
                logger.Info("Try to get music by Hashed OCR Data.");
                Music registeredMusic = musicDao.selectByHashedOcrData(analyzedMusic.hashedOcrData);
                if (registeredMusic == null)
                {
                    logger.Info("Try to get OCR readed music by title & level & difficult");
                    registeredMusic = musicDao.selectByTitleDifficultLevel(analyzedMusic.title, analyzedMusic.level, analyzedMusic.difficult);
                }

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
                        logger.Info("DialogResult is ok.");

                        if (registeredMusic == null)
                        {
                            logger.Info("Registered music is null. final check user inputed date (update or insert).");
                            // Check db master from title, difficult, level - if matched, update data.
                            registeredMusic = musicDao.selectByTitleDifficultLevel(analyzedMusic.title, analyzedMusic.level, analyzedMusic.difficult);
                            if (registeredMusic == null)
                            {
                                // Insert
                                logger.Info("User inputed date is not matched to database. Execute insert.");
                            }
                            else
                            {
                                // Update
                                logger.Info("User inputed date is matched to database. Execute update.");
                                // Attach data value
                                analyzedMusic.id = registeredMusic.id;
                                analyzedMusic.hashedOcrDataList = registeredMusic.hashedOcrDataList;
                            }
                        } else
                        {
                            logger.Info("Registered music is not null, update execute.");
                        }

                        musicDao.InsertOrReplace(analyzedMusic);
                    }
                    else
                    {
                        // If canceled, delete work file
                        string workfileOutputPath = dataFolderPath
                            + Path.DirectorySeparatorChar
                            + Path.GetFileNameWithoutExtension(filePath);
                        logger.Info("DialogResult is cancel, cleanup score data. Directory path = " + workfileOutputPath);
                        Directory.Delete(workfileOutputPath, true);
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
            try
            {
                if (MessageBox.Show("対象フォルダ内の追加ファイルのみ対象に解析を行います。よろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    // Analyze score
                    AnalyzeScoreFromDirectory(false);
                    // Refresh music tree view
                    BuildMusicTreeView();
                    // Show complete message
                    MessageBox.Show("楽曲の解析がすべて完了しました。");
                }
                else
                {
                    MessageBox.Show("実行をキャンセルしました。");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(String.Format(EXCEPTION_MESSAGE_FORMAT, ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// 全曲のスコア解析実行を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("対象フォルダ内の全ファイルに対し、強制的に全データの解析を行います。場合によっては二重にスコアが登録される可能性もあります。よろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    // Analyze score
                    AnalyzeScoreFromDirectory(true);
                    // Refresh music tree view
                    BuildMusicTreeView();
                    // Show complete message
                    MessageBox.Show("楽曲の解析がすべて完了しました。");
                }
                else
                {
                    MessageBox.Show("実行をキャンセルしました。");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(EXCEPTION_MESSAGE_FORMAT, ex.Message, ex.StackTrace));
            }

        }

        /// <summary>
        /// TreeViewで選択されているMusicIDを取得する。
        /// </summary>
        /// <returns>TreeViewで選択されているMusicID。Music以外が選択されているとき、Null。</returns>
        private int? GetTreeViewSelectedMusicId()
        {
            // Get selected node
            TreeNode selectedNode = MusicTreeView.SelectedNode;

            // Extract music id from tree node tag
            int? musicId;
            try
            {
                musicId = (int?)selectedNode.Tag;
            }
            catch (Exception)
            {
                musicId = null;
            }

            return musicId;
        }

        /// <summary>
        /// 楽曲が選択された際、そのスコアを隣のViewに表示する。
        /// </summary>
        private void RefreshScoreDataGridView()
        {
            logger.Info("DataGridView attach start.");

            // Extract music id from tree node tag
            int? musicId = GetTreeViewSelectedMusicId();

            // If null or empty, abort this function
            if (musicId == null)
            {
                logger.Error("Music id is null, selected non music element. Clear data grid view.");
                ScoreDataGridView.DataSource = null;
                return;
            }
            else
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
        /// 楽曲が選択された際、そのスコアを隣のViewに表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshScoreDataGridView();
        }

        /// <summary>
        /// 環境設定を行うフォームを開く。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExePathSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ExePathSettingForm settingForm = new ExePathSettingForm(ref setting))
            {
                if (settingForm.ShowDialog() == DialogResult.OK)
                {
                    Setting.SaveToFile(setting, settingFilePath);
                    MessageBox.Show("設定を保存しました。");
                }
            }
        }

        /// <summary>
        /// 画像切り抜き設定を行うフォームを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropImageRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // File name
                string fileName;

                // Select file
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                    openFileDialog.Title = "切り取りの設定でプレビューに使用するファイルを選択してください";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        fileName = openFileDialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                // Open ImageCropPoint form
                using (ImageCropRangeSettingForm imageCropPointForm = new ImageCropRangeSettingForm(fileName, ref setting))
                {
                    if (imageCropPointForm.ShowDialog() == DialogResult.OK)
                    {
                        Setting.SaveToFile(setting, settingFilePath);
                        MessageBox.Show("設定を保存しました。");
                        logger.Info("画像切り取りの設定が完了。");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(EXCEPTION_MESSAGE_FORMAT, ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// DataGridViewでダブルクリックされた要素を編集する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoreDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get selected item
            ScoreResult scoreResult = null;
            int selectedRow = 0;

            // Get selected row
            foreach(DataGridViewCell cell in ScoreDataGridView.SelectedCells)
            {
                selectedRow = cell.RowIndex;
                scoreResult = (ScoreResult)ScoreDataGridView.Rows[selectedRow].DataBoundItem;
                break;
            }

            // If can't selected, no action
            if (scoreResult == null)
            {
                return;
            }

            // Load id and open window
            int? musicId = GetTreeViewSelectedMusicId();

            // If can't get music id, no action
            if (musicId == null)
            {
                return;
            }

            // Get music object from Music ID
            MusicDao musicDao = new MusicDao(databaseFilePath);
            Music music = musicDao.selectByMusicId(musicId);

            music.scoreResultList = new List<ScoreResult>();
            music.scoreResultList.Add(scoreResult);

            using(RegistScoreConfirmForm registScoreConfirmForm = new RegistScoreConfirmForm(ref music, ref music))
            {
                if (DialogResult.OK == registScoreConfirmForm.ShowDialog())
                {
                    // Regist data
                    musicDao.InsertOrReplace(music);

                    // Reflesh view
                    RefreshScoreDataGridView();

                    MessageBox.Show("スコアデータの修正が完了しました。");
                } else
                {
                    // cancel
                    return;
                }
            }

        }

        /// <summary>
        /// アプリケーションを終了ボタンを押された場合の処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(EXIT_CODE_NORMAL);
        }

        /// <summary>
        /// アプリケーション情報を押された場合の処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(ApplicationInfoForm applicationInfoForm = new ApplicationInfoForm())
            {
                applicationInfoForm.ShowDialog();
            }
        }
    }
}
