using BndrScoreRecorder.common;
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
    public partial class ImageCropRangeSettingForm : Form
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

        // ListBox's item define
        private static readonly string LIST_ITEM_OCR_TITLE = "1.タイトル切り抜き座標設定 [{0}]";
        private static readonly string LIST_ITEM_OCR_DIFFICULT = "2.難易度切り抜き座標設定 [{0}]";
        private static readonly string LIST_ITEM_OCR_RESULT_NOTES = "3.ノーツ結果切り抜き座標設定 [{0}]";
        private static readonly string LIST_ITEM_OCR_MAX_COMBO = "4.MAX COMBO切り抜き座標設定 [{0}]";
        private static readonly string LIST_ITEM_OCR_LEVEL = "5.LEVEL切り抜き座標設定 [{0}]";
        private static readonly string LIST_ITEM_OCR_SCORE = "6.Score切り抜き座標設定 [{0}]";

        // ListBox's description define
        private static readonly string DESCRIPTION_OCR_TITLE = "曲名そのものの文字列。日本語を含む。読み取り結果例：てすと曲名";
        private static readonly string DESCRIPTION_OCR_DIFFICULT = "難易度の文字列。例えばBASIC、ADVANCE、EXPERT、SPECIAL等。アルファベットのみ。読み取り結果例：EXPERT";
        private static readonly string DESCRIPTION_OCR_RESULT_NOTES = "ノーツ結果の数字5行。上の行からPerfect,Great,Good,Bad,Miss。読み取り結果例：\r\n500\r\n100\r\n0005\r\n0003\r\n1";
        private static readonly string DESCRIPTION_OCR_MAX_COMBO = "MAX COMBOの数字。読み取り結果例：0500";
        private static readonly string DESCRIPTION_OCR_LEVEL = "LEVELの数字。読み取り結果例：25";
        private static readonly string DESCRIPTION_OCR_SCORE = "Scoreの数字。読み取り結果例：1000000";

        // ListBox's item index define
        private const int LIST_INDEX_OCR_TITLE = 0;
        private const int LIST_INDEX_OCR_DIFFICULT = 1;
        private const int LIST_INDEX_OCR_RESULT_NOTES = 2;
        private const int LIST_INDEX_OCR_MAX_COMBO = 3;
        private const int LIST_INDEX_OCR_LEVEL = 4;
        private const int LIST_INDEX_OCR_SCORE = 5;

        // Use for drag flag
        private bool isMouseDown = false;

        // Crop range point
        private Point mouseDownPoint;
        private Point mouseCurrentPoint;

        public ImageCropRangeSettingForm(string imageFilePath, ref Setting setting)
        {
            // Create log4net instance
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // Attach setting
            this.setting = setting;

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

            // Initialize select list box
            InitializeSelectOcrSettingListBox();

            // Show image
            //CropPictureBox.ImageLocation = imageFilePath;
            CropPictureBox.Image = Image.FromFile(imageFilePath);
        }

        /// <summary>
        /// SettingOcrSelectListBoxを初期化する。
        /// </summary>
        private void InitializeSelectOcrSettingListBox()
        {
            // Add items
            SelectOcrSettingListBox.Items.Add(String.Format(LIST_ITEM_OCR_TITLE, 
                setting.defaultBndrOcrSetting.TitleOcrSetting.ImageMagickCropOption()));
            SelectOcrSettingListBox.Items.Add(String.Format(LIST_ITEM_OCR_DIFFICULT, 
                setting.defaultBndrOcrSetting.DifficultOcrSetting.ImageMagickCropOption()));
            SelectOcrSettingListBox.Items.Add(String.Format(LIST_ITEM_OCR_RESULT_NOTES, 
                setting.defaultBndrOcrSetting.ResultNotesOcrSetting.ImageMagickCropOption()));
            SelectOcrSettingListBox.Items.Add(String.Format(LIST_ITEM_OCR_MAX_COMBO, 
                setting.defaultBndrOcrSetting.MaxComboOcrSetting.ImageMagickCropOption()));
            SelectOcrSettingListBox.Items.Add(String.Format(LIST_ITEM_OCR_LEVEL, 
                setting.defaultBndrOcrSetting.LevelOcrSetting.ImageMagickCropOption()));
            SelectOcrSettingListBox.Items.Add(String.Format(LIST_ITEM_OCR_SCORE,
                setting.defaultBndrOcrSetting.ScoreOcrSetting.ImageMagickCropOption()));

            // Init
            SelectOcrSettingListBox.SelectedIndex = 0;
        }

        /// <summary>
        /// SettingOcrSelectListBoxに設定の内容を反映する。
        /// </summary>
        private void ChangeApplyToSelectOcrSettingListBox()
        {
            // Change items
            SelectOcrSettingListBox.Items[LIST_INDEX_OCR_TITLE] = 
                String.Format(LIST_ITEM_OCR_TITLE,
                setting.defaultBndrOcrSetting.TitleOcrSetting.ImageMagickCropOption());

            SelectOcrSettingListBox.Items[LIST_INDEX_OCR_DIFFICULT] = 
                String.Format(LIST_ITEM_OCR_DIFFICULT,
                setting.defaultBndrOcrSetting.DifficultOcrSetting.ImageMagickCropOption());

            SelectOcrSettingListBox.Items[LIST_INDEX_OCR_RESULT_NOTES] = 
                String.Format(LIST_ITEM_OCR_RESULT_NOTES,
                setting.defaultBndrOcrSetting.ResultNotesOcrSetting.ImageMagickCropOption());

            SelectOcrSettingListBox.Items[LIST_INDEX_OCR_MAX_COMBO] = 
                String.Format(LIST_ITEM_OCR_MAX_COMBO,
                setting.defaultBndrOcrSetting.MaxComboOcrSetting.ImageMagickCropOption());

            SelectOcrSettingListBox.Items[LIST_INDEX_OCR_LEVEL] = 
                String.Format(LIST_ITEM_OCR_LEVEL,
                setting.defaultBndrOcrSetting.LevelOcrSetting.ImageMagickCropOption());

            SelectOcrSettingListBox.Items[LIST_INDEX_OCR_SCORE] =
                String.Format(LIST_ITEM_OCR_SCORE,
                setting.defaultBndrOcrSetting.ScoreOcrSetting.ImageMagickCropOption());
        }

        /// <summary>
        /// SettingOcrSelectListBoxを選択した際、対応する内容を画面に設定する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadSettingButton_Click(object sender, EventArgs e)
        {
            OcrSetting targetOcrSettiong;
            switch (SelectOcrSettingListBox.SelectedIndex)
            {
                case LIST_INDEX_OCR_TITLE:
                    // Title
                    targetOcrSettiong = setting.defaultBndrOcrSetting.TitleOcrSetting;
                    break;

                case LIST_INDEX_OCR_DIFFICULT:
                    //Difficult
                    targetOcrSettiong = setting.defaultBndrOcrSetting.DifficultOcrSetting;
                    break;

                case LIST_INDEX_OCR_MAX_COMBO:
                    // Max combo
                    targetOcrSettiong = setting.defaultBndrOcrSetting.MaxComboOcrSetting;
                    break;

                case LIST_INDEX_OCR_RESULT_NOTES:
                    // Result notes
                    targetOcrSettiong = setting.defaultBndrOcrSetting.ResultNotesOcrSetting;
                    break;

                case LIST_INDEX_OCR_LEVEL:
                    // Level
                    targetOcrSettiong = setting.defaultBndrOcrSetting.LevelOcrSetting;
                    break;

                case LIST_INDEX_OCR_SCORE:
                    // Score
                    targetOcrSettiong = setting.defaultBndrOcrSetting.ScoreOcrSetting;
                    break;

                default:
                    // Default
                    targetOcrSettiong = new OcrSetting();
                    break;
            }
            PositionXNumericUpDown.Value = targetOcrSettiong.positionX;
            PositionYNumericUpDown.Value = targetOcrSettiong.positionY;
            WidthNumericUpDown.Value = targetOcrSettiong.width;
            HeightNumericUpDown.Value = targetOcrSettiong.height;
        }

        /// <summary>
        /// 画面で設定した内容を、対象のOcrSettingオブジェクトに戻す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSettingButton_Click(object sender, EventArgs e)
        {
            OcrSetting targetOcrSettiong;
            int targetIndex;
            switch (SelectOcrSettingListBox.SelectedIndex)
            {
                case LIST_INDEX_OCR_TITLE:
                    // Title
                    targetOcrSettiong = setting.defaultBndrOcrSetting.TitleOcrSetting;
                    break;

                case LIST_INDEX_OCR_DIFFICULT:
                    //Difficult
                    targetOcrSettiong = setting.defaultBndrOcrSetting.DifficultOcrSetting;
                    break;

                case LIST_INDEX_OCR_MAX_COMBO:
                    // Max combo
                    targetOcrSettiong = setting.defaultBndrOcrSetting.MaxComboOcrSetting;
                    break;

                case LIST_INDEX_OCR_RESULT_NOTES:
                    // Result notes
                    targetOcrSettiong = setting.defaultBndrOcrSetting.ResultNotesOcrSetting;
                    break;

                case LIST_INDEX_OCR_LEVEL:
                    // Level
                    targetOcrSettiong = setting.defaultBndrOcrSetting.LevelOcrSetting;
                    break;

                case LIST_INDEX_OCR_SCORE:
                    // Score
                    targetOcrSettiong = setting.defaultBndrOcrSetting.ScoreOcrSetting;
                    break;

                default:
                    // Default
                    targetOcrSettiong = new OcrSetting();
                    break;
            }
            targetOcrSettiong.positionX = (int) PositionXNumericUpDown.Value;
            targetOcrSettiong.positionY = (int) PositionYNumericUpDown.Value;
            targetOcrSettiong.width = (int) WidthNumericUpDown.Value;
            targetOcrSettiong.height = (int) HeightNumericUpDown.Value;

            // Apply to control
            ChangeApplyToSelectOcrSettingListBox();
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
            switch (SelectOcrSettingListBox.SelectedIndex)
            {
                case LIST_INDEX_OCR_TITLE:
                    // Title
                    CropResultTextBox.Text = OcrReader.ReadFromImageFileJapaneseLang(
                        setting.pathImageMagickConvertExe, 
                        setting.pathTesseractExe, 
                        workImageFilePath, 
                        SUFFIX_CROPNAME_TEST, 
                        CreateCropString());
                    break;

                case LIST_INDEX_OCR_DIFFICULT:
                    //Difficult
                    CropResultTextBox.Text = OcrReader.ReadFromImageFile(
                        setting.pathImageMagickConvertExe, 
                        setting.pathTesseractExe, 
                        workImageFilePath, 
                        SUFFIX_CROPNAME_TEST, 
                        CreateCropString());
                    break;

                case LIST_INDEX_OCR_MAX_COMBO:
                    // Max Combo
                    CropResultTextBox.Text = OcrReader.ReadFromImageFileOnlyNumber(
                        setting.pathImageMagickConvertExe,
                        setting.pathTesseractExe,
                        workImageFilePath,
                        SUFFIX_CROPNAME_TEST,
                        CreateCropString());
                    break;

                case LIST_INDEX_OCR_RESULT_NOTES:
                    // Result notes
                    CropResultTextBox.Text = OcrReader.ReadFromImageFileOnlyNumber(
                        setting.pathImageMagickConvertExe,
                        setting.pathTesseractExe,
                        workImageFilePath,
                        SUFFIX_CROPNAME_TEST,
                        CreateCropString());
                    break;

                case LIST_INDEX_OCR_LEVEL:
                    // Level
                    CropResultTextBox.Text = OcrReader.ReadFromImageFileOnlyNumber(
                        setting.pathImageMagickConvertExe,
                        setting.pathTesseractExe,
                        workImageFilePath,
                        SUFFIX_CROPNAME_TEST,
                        CreateCropString());
                    break;

                case LIST_INDEX_OCR_SCORE:
                    // Score
                    CropResultTextBox.Text = OcrReader.ReadFromImageFileOnlyNumber(
                        setting.pathImageMagickConvertExe,
                        setting.pathTesseractExe,
                        workImageFilePath,
                        SUFFIX_CROPNAME_TEST,
                        CreateCropString());
                    break;

                default:
                    // Default
                    break;
            }
            // Change line separater
            CropResultTextBox.Text = CropResultTextBox.Text.Replace("\n", Environment.NewLine);
            // Attach to picture box
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
        /// 切り取りの始点を指定した場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            PositionXNumericUpDown.Value = e.X;
            PositionYNumericUpDown.Value = e.Y;

            mouseDownPoint = new Point(e.X, e.Y);
            mouseCurrentPoint = new Point();
        }

        /// <summary>
        /// 切り取り中にマウスを動かした場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == false)
            {
                return;
            }

            // Draw rectangle
            mouseCurrentPoint.X = e.X;
            mouseCurrentPoint.Y = e.Y;

            DrawRectangleInCropPictureBox(mouseDownPoint, mouseCurrentPoint);
        }

        /// <summary>
        /// イメージ上に四角を描画する
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        private void DrawRectangleInCropPictureBox(Point startPoint, Point endPoint)
        {
            using (Pen pen = new Pen(Color.Red))
            using (Graphics graphics = CropPictureBox.CreateGraphics())
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                CropPictureBox.Refresh();
                graphics.DrawRectangle(pen, startPoint.X, startPoint.Y, Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
            }
        }

        /// <summary>
        /// 切り取りの終点を指定した場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                try
                {
                    WidthNumericUpDown.Value = e.X - PositionXNumericUpDown.Value;
                    HeightNumericUpDown.Value = e.Y - PositionYNumericUpDown.Value;
                }
                catch (Exception)
                {

                }
            }

            isMouseDown = false;
        }

        /// <summary>
        /// SelectOcrSettingListが変更されたとき、その説明を欄に表示する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOcrSettingListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Message text
            string message;

            // Get message by selected index
            switch (SelectOcrSettingListBox.SelectedIndex)
            {
                case LIST_INDEX_OCR_TITLE:
                    // Title
                    message = DESCRIPTION_OCR_TITLE;
                    break;

                case LIST_INDEX_OCR_DIFFICULT:
                    //Difficult
                    message = DESCRIPTION_OCR_DIFFICULT;
                    break;

                case LIST_INDEX_OCR_MAX_COMBO:
                    // Max combo
                    message = DESCRIPTION_OCR_MAX_COMBO;
                    break;

                case LIST_INDEX_OCR_RESULT_NOTES:
                    // Result notes
                    message = DESCRIPTION_OCR_RESULT_NOTES;
                    break;

                case LIST_INDEX_OCR_LEVEL:
                    // Level
                    message = DESCRIPTION_OCR_LEVEL;
                    break;

                case LIST_INDEX_OCR_SCORE:
                    // Score
                    message = DESCRIPTION_OCR_SCORE;
                    break;

                default:
                    // Default
                    message = string.Empty;
                    break;
            }

            // Set text
            DescriptionTextBox.Text = message;
        }

        /// <summary>
        /// NumericUpDownが変更されたときに、四角形を再描画する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DrawRectangleInCropPictureBox(
                new Point((int)PositionXNumericUpDown.Value, (int)PositionYNumericUpDown.Value), 
                new Point(
                    (int)(PositionXNumericUpDown.Value + WidthNumericUpDown.Value), 
                    (int)(PositionYNumericUpDown.Value + HeightNumericUpDown.Value))
                );
        }
    }
}
