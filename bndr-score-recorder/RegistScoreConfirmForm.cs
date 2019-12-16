using BndrScoreRecorder.common.entity;
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
    public partial class RegistScoreConfirmForm : Form
    {
        // logger
        private log4net.ILog logger;

        // screenshot data folder path
        private string dataFolderPath;

        // sqlite database path
        private string databaseFilePath;

        // music object
        public Music music;

        // registered music object
        public Music registeredMusic;

        // Initializing frag
        private bool isInitializing = true;

        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="refMusic">確認フォームに表示するMusicオブジェクト</param>
        /// <param name="refRegisteredMusic">整合性チェックに使用するMusicオブジェクト</param>
        public RegistScoreConfirmForm(ref Music refMusic, ref Music refRegisteredMusic)
        {
            // Initializing start
            isInitializing = true;

            // Create log4net instance
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // create screenshot data folder path
            dataFolderPath = System.Windows.Forms.Application.StartupPath
                + Path.DirectorySeparatorChar
                + "data";

            databaseFilePath = dataFolderPath
                + Path.DirectorySeparatorChar
                + "bndr-score-recorder.db";

            // null check
            if (refMusic == null)
            {
                string errorMessage = "Registered music is null.";
                logger.Error(errorMessage);
                throw new ArgumentException(errorMessage);
            }

            // copy value
            music = refMusic;
            registeredMusic = refRegisteredMusic;

            InitializeComponent();

            // Data attached to screen
            DataAttach(music);

            // Initialize complete
            isInitializing = false;
        }

        /// <summary>PerfectNumericUpDown
        /// 画面にMusicオブジェクトを表示し、必要に応じて警告を発生させる。
        /// </summary>
        /// <param name="music">表示対象のMusicオブジェクト</param>
        private void DataAttach(Music music)
        {
            // message text
            StringBuilder musicMessageBuilder = new StringBuilder();
            StringBuilder scoreResultMessageBuilder = new StringBuilder();

            // initialize component
            TitleTextBox.ForeColor = Color.Black;
            TitleTextBox.BackColor = Color.White;
            DifficultTextBox.ForeColor = Color.Black;
            DifficultTextBox.BackColor = Color.White;
            LevelTextBox.ForeColor = Color.Black;
            LevelTextBox.BackColor = Color.White;

            PerfectNumericUpDown.ForeColor = Color.Black;
            PerfectNumericUpDown.BackColor = Color.White;
            GreatNumericUpDown.ForeColor = Color.Black;
            GreatNumericUpDown.BackColor = Color.White;
            GoodNumericUpDown.ForeColor = Color.Black;
            GoodNumericUpDown.BackColor = Color.White;
            BadNumericUpDown.ForeColor = Color.Black;
            BadNumericUpDown.BackColor = Color.White;
            MissNumericUpDown.ForeColor = Color.Black;
            MissNumericUpDown.BackColor = Color.White;

            MaxComboNumericUpDown.ForeColor = Color.Black;
            MaxComboNumericUpDown.BackColor = Color.White;
            ScoreNumericUpDown.ForeColor = Color.Black;
            ScoreNumericUpDown.BackColor = Color.White;

            // attache data (music)
            TitleTextBox.Text = music.title;
            DifficultTextBox.Text = music.difficult;
            LevelTextBox.Text = music.level.ToString();
            IdTextBox.Text = music.id.ToString();

            // attache data (score record)
            if (music.scoreResultList.Count > 0)
            {
                ScoreResult scoreResult = music.scoreResultList[0];
                PerfectNumericUpDown.Value = scoreResult.perfect;
                GreatNumericUpDown.Value = scoreResult.great;
                GoodNumericUpDown.Value = scoreResult.good;
                BadNumericUpDown.Value = scoreResult.bad;
                MissNumericUpDown.Value = scoreResult.miss;
                MaxComboNumericUpDown.Value = scoreResult.maxCombo;
                ScoreNumericUpDown.Value = scoreResult.score;

                ExScoreNumericUpDown.Value = scoreResult.exScore;
                TotalNotesNumericUpDown.Value = scoreResult.totalNotes;

                ScreenshotPictureBox.ImageLocation =
                    dataFolderPath
                    + Path.DirectorySeparatorChar
                    + scoreResult.imageFilePath;
            }

            // check music is registered?
            if (registeredMusic == null)
            {
                // Initial check
                musicMessageBuilder.AppendLine("新規登録の楽曲マスタデータです。読み取り異常がないか注意して確認してください。");
                scoreResultMessageBuilder.AppendLine("新規登録の楽曲マスタデータです。読み取り異常がないか注意して確認してください。");
                PerfectNumericUpDown.BackColor = Color.Pink;
                GreatNumericUpDown.BackColor = Color.Pink;
                GoodNumericUpDown.BackColor = Color.Pink;
                BadNumericUpDown.BackColor = Color.Pink;
                MissNumericUpDown.BackColor = Color.Pink;

                string readWarningMessage = "{0}が3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。";
                string readErrorMessage = "{0}が正常に読み取れませんでした。データを手動入力で修正してください。";

                if (music.scoreResultList.Count > 0)
                {
                    ScoreResult scoreResult = music.scoreResultList[0];

                    // error detect
                    if (scoreResult.perfect == 8 || scoreResult.perfect == 6 || scoreResult.perfect == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Perfect"));
                        PerfectNumericUpDown.BackColor = Color.Red;
                        PerfectNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.perfect < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Perfect"));
                        PerfectNumericUpDown.BackColor = Color.Red;
                        PerfectNumericUpDown.ForeColor = Color.White;
                    }

                    if (scoreResult.great == 8 || scoreResult.great == 6 || scoreResult.great == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Great"));
                        GreatNumericUpDown.BackColor = Color.Red;
                        GreatNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.great < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Great"));
                        GreatNumericUpDown.BackColor = Color.Red;
                        GreatNumericUpDown.ForeColor = Color.White;
                    }

                    if (scoreResult.good == 8 || scoreResult.good == 6 || scoreResult.good == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Good"));
                        GoodNumericUpDown.BackColor = Color.Red;
                        GoodNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.good < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Good"));
                        GoodNumericUpDown.BackColor = Color.Red;
                        GoodNumericUpDown.ForeColor = Color.White;
                    }

                    if (scoreResult.bad == 8 || scoreResult.bad == 6 || scoreResult.bad == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Bad"));
                        BadNumericUpDown.BackColor = Color.Red;
                        BadNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.bad < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Bad"));
                        BadNumericUpDown.BackColor = Color.Red;
                        BadNumericUpDown.ForeColor = Color.White;
                    }

                    if (scoreResult.miss == 8 || scoreResult.miss == 6 || scoreResult.miss == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Miss"));
                        MissNumericUpDown.BackColor = Color.Red;
                        MissNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.miss < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Miss"));
                        MissNumericUpDown.BackColor = Color.Red;
                        MissNumericUpDown.ForeColor = Color.White;
                    }

                    if (scoreResult.maxCombo == 8 || scoreResult.maxCombo == 6 || scoreResult.maxCombo == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Max Combo"));
                        MaxComboNumericUpDown.BackColor = Color.Red;
                        MaxComboNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.maxCombo < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Max Combo"));
                        MaxComboNumericUpDown.BackColor = Color.Red;
                        MaxComboNumericUpDown.ForeColor = Color.White;
                    }

                    if (scoreResult.score == 8 || scoreResult.score == 6 || scoreResult.score == 3)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readWarningMessage, "Score"));
                        ScoreNumericUpDown.BackColor = Color.Red;
                        ScoreNumericUpDown.ForeColor = Color.White;
                    }
                    else if (scoreResult.score < 0)
                    {
                        scoreResultMessageBuilder.AppendLine(String.Format(readErrorMessage, "Score"));
                        ScoreNumericUpDown.BackColor = Color.Red;
                        ScoreNumericUpDown.ForeColor = Color.White;
                    }
                }
            }
            else
            {
                musicMessageBuilder.AppendLine("楽曲マスタデータが既に存在します。データを更新した場合、今までの全データが書き換わります。");

                // This check is exists registered score result
                if (music.scoreResultList.Count > 0)
                {
                    if (registeredMusic != null)
                    {
                        ScoreResult scoreResult = music.scoreResultList[0];
                        foreach (ScoreResult registeredScoreResult in registeredMusic.scoreResultList)
                        {
                            if (scoreResult.totalNotes != registeredScoreResult.totalNotes)
                            {
                                scoreResultMessageBuilder.AppendLine("登録済みデータと合計ノーツ数が異なっています。読み取り異常がないか確認してください。");
                                PerfectNumericUpDown.BackColor = Color.Red;
                                PerfectNumericUpDown.ForeColor = Color.White;
                                GreatNumericUpDown.BackColor = Color.Red;
                                GreatNumericUpDown.ForeColor = Color.White;
                                GoodNumericUpDown.BackColor = Color.Red;
                                GoodNumericUpDown.ForeColor = Color.White;
                                BadNumericUpDown.BackColor = Color.Red;
                                BadNumericUpDown.ForeColor = Color.White;
                                MissNumericUpDown.BackColor = Color.Red;
                                MissNumericUpDown.ForeColor = Color.White;
                                break;
                            }
                        }
                    }
                }
            }

            // show message
            MusicMessageTextBox.Text = musicMessageBuilder.ToString();
            ScoreMessageTextBox.Text = scoreResultMessageBuilder.ToString();
        }

        /// <summary>
        /// 登録ボタンを押したときの処理。入力データを内部オブジェクトに反映し、ウインドウを閉じる。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            // update music instance
            music.title = TitleTextBox.Text;
            music.difficult = DifficultTextBox.Text;
            music.level = int.Parse(LevelTextBox.Text);

            try
            {
                if (music.scoreResultList.Count > 0)
                {
                    music.scoreResultList[0].perfect = (int)PerfectNumericUpDown.Value;
                    music.scoreResultList[0].great = (int)GreatNumericUpDown.Value;
                    music.scoreResultList[0].good = (int)GoodNumericUpDown.Value;
                    music.scoreResultList[0].bad = (int)BadNumericUpDown.Value;
                    music.scoreResultList[0].miss = (int)MissNumericUpDown.Value;
                    music.scoreResultList[0].maxCombo = (int)MaxComboNumericUpDown.Value;
                    music.scoreResultList[0].score = (int)ScoreNumericUpDown.Value;

                    music.scoreResultList[0].CalculateInfos();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("数字項目に数字以外が入力されています。");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// キャンセルボタンを押したときの処理。何もせずにウインドウを閉じる。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// EX ScoreとTotal Notesを再計算し、画面上に再表示＆警告チェックする。
        /// </summary>
        private void RecalculateAttachedData()
        {
            try
            {
                if (music.scoreResultList.Count > 0)
                {
                    music.scoreResultList[0].perfect = (int)PerfectNumericUpDown.Value;
                    music.scoreResultList[0].great = (int)GreatNumericUpDown.Value;
                    music.scoreResultList[0].good = (int)GoodNumericUpDown.Value;
                    music.scoreResultList[0].bad = (int)BadNumericUpDown.Value;
                    music.scoreResultList[0].miss = (int)MissNumericUpDown.Value;

                    // calculate ex score
                    music.scoreResultList[0].exScore = music.scoreResultList[0].perfect * 2 + music.scoreResultList[0].great;

                    // calculate total notes
                    music.scoreResultList[0].totalNotes = music.scoreResultList[0].perfect
                        + music.scoreResultList[0].great
                        + music.scoreResultList[0].good
                        + music.scoreResultList[0].bad
                        + music.scoreResultList[0].miss;

                    // attach
                    TotalNotesNumericUpDown.Value = music.scoreResultList[0].totalNotes;
                    ExScoreNumericUpDown.Value = music.scoreResultList[0].exScore;

                    DataAttach(music);
                }
            }
            catch (FormatException)
            {

            }
        }

        /// <summary>
        /// EX ScoreとTotal Notesを再計算し、画面上に再表示＆警告チェックする。
        /// </summary>
        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (isInitializing == false)
            {
                RecalculateAttachedData();
            }
        }
    }
}
