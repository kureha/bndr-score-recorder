using BndrScoreRecorder.common;
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
    public partial class ImageCropPointForm : Form
    {
        // logger
        private log4net.ILog logger;

        // Work directory path
        private string workDirectoryPath;

        // Work image file path
        private string workImageFilePath;

        // Using image filepath
        private string imageFilePath;

        // Setting object
        private Setting setting;

        // Crop output suffix
        private const string SUFFIX_CROPNAME_TEST = ".test";

        // ListBox's item Define
        private static readonly string LIST_ITEM_OCR_TITLE = "1.タイトル切り抜き座標設定";
        private static readonly string LIST_ITEM_OCR_DIFFICULT = "2.難易度切り抜き座標設定";
        private static readonly string LIST_ITEM_OCR_SCORE = "3.スコア切り抜き座標設定";
        private static readonly string LIST_ITEM_OCR_MAX_COMBO = "4.MAX COMBO切り抜き座標設定";
        private static readonly string LIST_ITEM_OCR_LEVEL = "5.LEVEL切り抜き座標設定";

        // ListBox's item index define
        private const int LIST_INDEX_OCR_TITLE = 0;
        private const int LIST_INDEX_OCR_DIFFICULT = 1;
        private const int LIST_INDEX_OCR_SCORE = 2;
        private const int LIST_INDEX_OCR_MAX_COMBO = 3;
        private const int LIST_INDEX_OCR_LEVEL = 4;

        public ImageCropPointForm(string imageFilePath, ref Setting setting)
        {
            // Create log4net instance
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // Create screenshot data folder path
            workDirectoryPath = System.Windows.Forms.Application.StartupPath
                + Path.DirectorySeparatorChar
                + "data"
                + Path.DirectorySeparatorChar
                + "Work";

            logger.Info("Work directory path = " + workDirectoryPath);

            // If work directory is not exists, create work directory.
            if (Directory.Exists(workDirectoryPath) == false)
            {
                Directory.CreateDirectory(workDirectoryPath);
                logger.Info("Create work directory complete.");
            }

            workImageFilePath = workDirectoryPath + Path.DirectorySeparatorChar + Path.GetFileName(imageFilePath);
            logger.Info("Copy target file to work directory. destination file path = " + imageFilePath);

            // If image file exists, delete image file.
            if (File.Exists(workImageFilePath) == true)
            {
                logger.Info("Delete work file path.");
                File.Delete(workImageFilePath);
            }
            File.Copy(imageFilePath, workImageFilePath);
            logger.Info("Copy target file complete.");

            InitializeComponent();

            // Initialize select list box
            InitializeSelectOcrSettingListBox();

            // Attach to variables
            this.imageFilePath = imageFilePath;
            this.setting = setting;

            // Show image
            CropPictureBox.ImageLocation = imageFilePath;
        }

        /// <summary>
        /// SettingOcrSelectListBoxを初期化する
        /// </summary>
        private void InitializeSelectOcrSettingListBox()
        {
            // Add items
            SelectOcrSettingListBox.Items.Add(LIST_ITEM_OCR_TITLE);
            SelectOcrSettingListBox.Items.Add(LIST_ITEM_OCR_DIFFICULT);
            SelectOcrSettingListBox.Items.Add(LIST_ITEM_OCR_SCORE);
            SelectOcrSettingListBox.Items.Add(LIST_ITEM_OCR_MAX_COMBO);
            SelectOcrSettingListBox.Items.Add(LIST_ITEM_OCR_LEVEL);

            // Init
            SelectOcrSettingListBox.SelectedIndex = 0;
        }

        /// <summary>
        /// SettingOcrSelectListBoxを選択した際、対応する内容を画面に設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOcrSettingListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int positionX;
            int positionY;
            int width;
            int height;
            switch(SelectOcrSettingListBox.SelectedIndex)
            {
                case LIST_INDEX_OCR_TITLE:
                    // Title
                    break;

                case LIST_INDEX_OCR_DIFFICULT:
                    //Difficult
                    break;

                case LIST_INDEX_OCR_MAX_COMBO:
                    // Max combo
                    break;

                case LIST_INDEX_OCR_SCORE:
                    // Score
                    break;

                case LIST_INDEX_OCR_LEVEL:
                    // Level
                    break;
            }
        }

        /// <summary>
        /// 切り抜きボタンを押下し、結果を表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TryCropButton_Click(object sender, EventArgs e)
        {
            // Initialize result text box
            CropResultTextBox.Text = string.Empty;
            CropResultPictureBox.ImageLocation = null;

            // Try to crop file
            CropResultTextBox.Text = OcrReader.ReadFromImageFile(setting.pathImageMagickConvertExe, setting.pathTesseractExe, workImageFilePath, SUFFIX_CROPNAME_TEST, CreateCropString());
            CropResultPictureBox.ImageLocation = OcrReader.CreateImageMagickOutputFilePath(workImageFilePath, SUFFIX_CROPNAME_TEST);
        }

        /// <summary>
        /// Crop Stringを作成し返却する。
        /// </summary>
        /// <returns></returns>
        private string CreateCropString()
        {
            return WidthNumericUpDown.Value.ToString() 
                + "x" + HeightNumericUpDown.Value.ToString() 
                + "+" + PositionXNumericUpDown.Value.ToString() 
                + "+" + PositionYNumericUpDown.Value.ToString();
        }

        /// <summary>
        /// 設定の保存を実施する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 設定のキャンセルを実施する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbortButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 切り取りの視点を指定した場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PositionXNumericUpDown.Value = e.X;
            PositionYNumericUpDown.Value = e.Y;
        }

        /// <summary>
        /// 切り取りの終点を指定した場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                WidthNumericUpDown.Value = e.X - PositionXNumericUpDown.Value;
                HeightNumericUpDown.Value = e.Y - PositionYNumericUpDown.Value;
            } catch (Exception)
            {

            }
        }
    }
}
