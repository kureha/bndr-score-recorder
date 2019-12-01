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
    public partial class SettingForm : Form
    {
        // Setting object
        private Setting setting;

        public SettingForm(ref Setting setting)
        {
            // Insert ref setting object
            this.setting = setting;

            InitializeComponent();

            // Attach object to form control
            ImageMagickConvertPathTextBox.Text = setting.pathImageMagickConvertExe;
            TesseractPathTextBox.Text = setting.pathTesseractExe;
        }

        private void ImageMagickConvertPathSelectButton_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "ImageMagick convert.exe file|convert.exe|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ImageMagickConvertPathTextBox.Text = dialog.FileName;
                }
            }
        }

        private void TesseractPathSelectButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Tesseract tesseract.exe file|tesseract.exe|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    TesseractPathTextBox.Text = dialog.FileName;
                }
            }
        }

        private void RegistButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            // Update setting object
            setting.pathImageMagickConvertExe = ImageMagickConvertPathTextBox.Text;
            setting.pathTesseractExe = TesseractPathTextBox.Text;

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
