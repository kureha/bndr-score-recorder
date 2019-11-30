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

        public RegistScoreConfirmForm(ref Music music)
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

            InitializeComponent();

            DataAttach(music);
        }

        /// <summary>
        /// 画面にMusicオブジェクトをアタッチします
        /// </summary>
        /// <param name="music"></param>
        private void DataAttach(Music music)
        {
            TitleTextBox.Text = music.title;
            LevelTextBox.Text = music.level.ToString();
            IdTextBox.Text = music.id;

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
            
        }

        private void RegistButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PerfectTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void GreatTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void GoodTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void BadTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MissTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
