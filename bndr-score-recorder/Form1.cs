using bndr_score_recorder.common.tesseract;
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

namespace bndr_score_recorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AnalyzeScoreButton_Click(object sender, EventArgs e)
        {
            // image path variable
            string screenshotImageFilePath;
            string titleImageFilePath;
            string scoreImageFilePath;

            // read from dialog
            OpenFileDialog imagePathFileDialog = new OpenFileDialog();
            if (imagePathFileDialog.ShowDialog() == DialogResult.OK)
            {
                screenshotImageFilePath = imagePathFileDialog.FileName;
            }
            else
            {
                return;
            }

            // create path
            titleImageFilePath = screenshotImageFilePath + ".title.png";
            scoreImageFilePath = screenshotImageFilePath + ".score.png";

            // crop title
            ImageMagickBridge.CropExecute(
                @"C:\ImageMagick\convert.exe",
                screenshotImageFilePath,
                titleImageFilePath,
                "470x25+345+35");

            // crop score
            ImageMagickBridge.CropExecute(
                @"C:\ImageMagick\convert.exe", 
                screenshotImageFilePath,
                scoreImageFilePath,
                "230x150+650+250");

            // read title
            TesseractBridge.Execute(
                @"C:\Program Files\Tesseract-OCR\tesseract.exe",
                titleImageFilePath,
                titleImageFilePath
                );

            // score file path
            string titleTextImageFilePath = titleImageFilePath + TesseractBridge.SUFFIX_OUTPUT_FILE_NAME;
            string titleText = string.Empty;

            // file read
            using (StreamReader streamWriter = new StreamReader(titleTextImageFilePath))
            {
                titleText = streamWriter.ReadToEnd();
            }

            // read score
            TesseractBridge.Execute(
                @"C:\Program Files\Tesseract-OCR\tesseract.exe",
                scoreImageFilePath,
                scoreImageFilePath
                );

            string scoreTextImageFilePath = scoreImageFilePath + TesseractBridge.SUFFIX_OUTPUT_FILE_NAME;
            string scoreText = string.Empty;

            // file read
            using (StreamReader streamWriter = new StreamReader(scoreTextImageFilePath))
            {
                scoreText = streamWriter.ReadToEnd();
            }

            // result
            MessageBox.Show(titleText + "\n" + scoreText);
        }
    }
}
