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

            // Attach to variables
            this.imageFilePath = imageFilePath;
            this.setting = setting;

            // Show image
            CropPictureBox.ImageLocation = imageFilePath;
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
            WidthNumericUpDown.Value = e.X - PositionXNumericUpDown.Value;
            HeightNumericUpDown.Value = e.Y - PositionYNumericUpDown.Value;
        }
    }
}
