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

        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="refMusic">確認フォームに表示するMusicオブジェクト</param>
        /// <param name="refRegisteredMusic">整合性チェックに使用するMusicオブジェクト</param>
        public RegistScoreConfirmForm(ref Music refMusic, ref Music refRegisteredMusic)
        {
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
        }

        /// <summary>
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

            PerfectTextBox.ForeColor = Color.Black;
            PerfectTextBox.BackColor = Color.White;
            GreatTextBox.ForeColor = Color.Black;
            GreatTextBox.BackColor = Color.White;
            GoodTextBox.ForeColor = Color.Black;
            GoodTextBox.BackColor = Color.White;
            BadTextBox.ForeColor = Color.Black;
            BadTextBox.BackColor = Color.White;
            MissTextBox.ForeColor = Color.Black;
            MissTextBox.BackColor = Color.White;

            // attache data (music)
            TitleTextBox.Text = music.title;
            DifficultTextBox.Text = music.difficult;
            LevelTextBox.Text = music.level.ToString();
            IdTextBox.Text = music.id;

            // attache data (score record)
            if (music.scoreResultList.Count > 0)
            {
                ScoreResult scoreResult = music.scoreResultList[0];
                PerfectTextBox.Text = scoreResult.perfect.ToString();
                GreatTextBox.Text = scoreResult.great.ToString();
                GoodTextBox.Text = scoreResult.good.ToString();
                BadTextBox.Text = scoreResult.bad.ToString();
                MissTextBox.Text = scoreResult.miss.ToString();
                MaxComboTextBox.Text = scoreResult.maxCombo.ToString();

                ExScoreTextBox.Text = scoreResult.exScore.ToString();
                TotalNotesTextBox.Text = scoreResult.totalNotes.ToString();

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
                PerfectTextBox.BackColor = Color.Pink;
                GreatTextBox.BackColor = Color.Pink;
                GoodTextBox.BackColor = Color.Pink;
                BadTextBox.BackColor = Color.Pink;
                MissTextBox.BackColor = Color.Pink;
                if (music.scoreResultList.Count > 0)
                {
                    ScoreResult scoreResult = music.scoreResultList[0];

                    // error detect
                    if (scoreResult.perfect == 8 || scoreResult.perfect == 6 || scoreResult.perfect == 3)
                    {
                        scoreResultMessageBuilder.AppendLine("Perfectが3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。");
                        PerfectTextBox.BackColor = Color.Red;
                        PerfectTextBox.ForeColor = Color.White;
                    }
                    else if (scoreResult.perfect < 0)
                    {
                        scoreResultMessageBuilder.AppendLine("Perfectが正常に読み取れませんでした。データを手動入力で修正してください。");
                        PerfectTextBox.BackColor = Color.Red;
                        PerfectTextBox.ForeColor = Color.White;
                    }

                    if (scoreResult.great == 8 || scoreResult.great == 6 || scoreResult.great == 3)
                    {
                        scoreResultMessageBuilder.AppendLine("Greatが3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。");
                        GreatTextBox.BackColor = Color.Red;
                        GreatTextBox.ForeColor = Color.White;
                    }
                    else if (scoreResult.great < 0)
                    {
                        scoreResultMessageBuilder.AppendLine("Greatが正常に読み取れませんでした。データを手動入力で修正してください。");
                        GreatTextBox.BackColor = Color.Red;
                        GreatTextBox.ForeColor = Color.White;
                    }

                    if (scoreResult.good == 8 || scoreResult.good == 6 || scoreResult.good == 3)
                    {
                        scoreResultMessageBuilder.AppendLine("Goodが3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。");
                        GoodTextBox.BackColor = Color.Red;
                        GoodTextBox.ForeColor = Color.White;
                    }
                    else if (scoreResult.good < 0)
                    {
                        scoreResultMessageBuilder.AppendLine("Goodが正常に読み取れませんでした。データを手動入力で修正してください。");
                        GoodTextBox.BackColor = Color.Red;
                        GoodTextBox.ForeColor = Color.White;
                    }

                    if (scoreResult.bad == 8 || scoreResult.bad == 6 || scoreResult.bad == 3)
                    {
                        scoreResultMessageBuilder.AppendLine("Badが3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。");
                        BadTextBox.BackColor = Color.Red;
                        BadTextBox.ForeColor = Color.White;
                    }
                    else if (scoreResult.bad < 0)
                    {
                        scoreResultMessageBuilder.AppendLine("Badが正常に読み取れませんでした。データを手動入力で修正してください。");
                        BadTextBox.BackColor = Color.Red;
                        BadTextBox.ForeColor = Color.White;
                    }

                    if (scoreResult.miss == 8 || scoreResult.miss == 6 || scoreResult.miss == 3)
                    {
                        scoreResultMessageBuilder.AppendLine("Missが3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。");
                        MissTextBox.BackColor = Color.Red;
                        MissTextBox.ForeColor = Color.White;
                    }
                    else if (scoreResult.miss < 0)
                    {
                        scoreResultMessageBuilder.AppendLine("Missが正常に読み取れませんでした。データを手動入力で修正してください。");
                        MissTextBox.BackColor = Color.Red;
                        MissTextBox.ForeColor = Color.White;
                    }

                    if (scoreResult.maxCombo == 8 || scoreResult.maxCombo == 6 || scoreResult.maxCombo == 3)
                    {
                        scoreResultMessageBuilder.AppendLine("MaxComboが3/6/8のいずれか１文字のため読み取りエラーの可能性があります。読み取り異常がないか確認してください。");
                        MaxComboTextBox.BackColor = Color.Red;
                        MaxComboTextBox.ForeColor = Color.White;
                    }
                    else if (scoreResult.maxCombo < 0)
                    {
                        scoreResultMessageBuilder.AppendLine("MaxComboが正常に読み取れませんでした。データを手動入力で修正してください。");
                        MaxComboTextBox.BackColor = Color.Red;
                        MaxComboTextBox.ForeColor = Color.White;
                    }
                }
            } else
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
                                PerfectTextBox.BackColor = Color.Pink;
                                GreatTextBox.BackColor = Color.Pink;
                                GoodTextBox.BackColor = Color.Pink;
                                BadTextBox.BackColor = Color.Pink;
                                MissTextBox.BackColor = Color.Pink;
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
                    music.scoreResultList[0].perfect = int.Parse(PerfectTextBox.Text);
                    music.scoreResultList[0].great = int.Parse(GreatTextBox.Text);
                    music.scoreResultList[0].good = int.Parse(GoodTextBox.Text);
                    music.scoreResultList[0].bad = int.Parse(BadTextBox.Text);
                    music.scoreResultList[0].miss = int.Parse(MissTextBox.Text);
                    music.scoreResultList[0].maxCombo = int.Parse(MaxComboTextBox.Text);

                    music.scoreResultList[0].exScore = music.scoreResultList[0].perfect * 2 + music.scoreResultList[0].great;
                    music.scoreResultList[0].totalNotes =
                        music.scoreResultList[0].perfect
                        + music.scoreResultList[0].great
                        + music.scoreResultList[0].good
                        + music.scoreResultList[0].bad
                        + music.scoreResultList[0].miss;
                }
            } catch (FormatException)
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
                if (music.scoreResultList.Count > 0) {
                    music.scoreResultList[0].perfect = int.Parse(PerfectTextBox.Text);
                    music.scoreResultList[0].great = int.Parse(GreatTextBox.Text);
                    music.scoreResultList[0].good = int.Parse(GoodTextBox.Text);
                    music.scoreResultList[0].bad = int.Parse(BadTextBox.Text);
                    music.scoreResultList[0].miss = int.Parse(MissTextBox.Text);
                    music.scoreResultList[0].maxCombo = int.Parse(MaxComboTextBox.Text);

                    // calculate ex score
                    music.scoreResultList[0].exScore = music.scoreResultList[0].perfect * 2 + music.scoreResultList[0].great;

                    // calculate total notes
                    music.scoreResultList[0].totalNotes = music.scoreResultList[0].perfect
                        + music.scoreResultList[0].great
                        + music.scoreResultList[0].good
                        + music.scoreResultList[0].bad
                        + music.scoreResultList[0].miss;

                    // attach
                    TotalNotesTextBox.Text = music.scoreResultList[0].totalNotes.ToString();
                    ExScoreTextBox.Text = music.scoreResultList[0].exScore.ToString();

                    DataAttach(music);
                }
            } catch (FormatException) {

            }
        }

        private void PerfectTextBox_TextChanged(object sender, EventArgs e)
        {
            RecalculateAttachedData();
        }

        private void GreatTextBox_TextChanged(object sender, EventArgs e)
        {
            RecalculateAttachedData();
        }

        private void GoodTextBox_TextChanged(object sender, EventArgs e)
        {
            RecalculateAttachedData();
        }

        private void BadTextBox_TextChanged(object sender, EventArgs e)
        {
            RecalculateAttachedData();
        }

        private void MissTextBox_TextChanged(object sender, EventArgs e)
        {
            RecalculateAttachedData();
        }
    }
}
