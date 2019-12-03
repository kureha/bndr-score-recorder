namespace BndrScoreRecorder
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SettingGroupBox = new System.Windows.Forms.GroupBox();
            this.TesseractPathSelectButton = new System.Windows.Forms.Button();
            this.ImageMagickConvertPathSelectButton = new System.Windows.Forms.Button();
            this.TesseractPathTextBox = new System.Windows.Forms.TextBox();
            this.ImageMagickConvertPathTextBox = new System.Windows.Forms.TextBox();
            this.TesseractLabel = new System.Windows.Forms.Label();
            this.ImageMagickConvertLabel = new System.Windows.Forms.Label();
            this.RegistButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SettingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingGroupBox
            // 
            this.SettingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingGroupBox.Controls.Add(this.TesseractPathSelectButton);
            this.SettingGroupBox.Controls.Add(this.ImageMagickConvertPathSelectButton);
            this.SettingGroupBox.Controls.Add(this.TesseractPathTextBox);
            this.SettingGroupBox.Controls.Add(this.ImageMagickConvertPathTextBox);
            this.SettingGroupBox.Controls.Add(this.TesseractLabel);
            this.SettingGroupBox.Controls.Add(this.ImageMagickConvertLabel);
            this.SettingGroupBox.Location = new System.Drawing.Point(12, 12);
            this.SettingGroupBox.Name = "SettingGroupBox";
            this.SettingGroupBox.Size = new System.Drawing.Size(776, 72);
            this.SettingGroupBox.TabIndex = 0;
            this.SettingGroupBox.TabStop = false;
            this.SettingGroupBox.Text = "依存実行ファイルのパス設定";
            // 
            // TesseractPathSelectButton
            // 
            this.TesseractPathSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TesseractPathSelectButton.Location = new System.Drawing.Point(695, 41);
            this.TesseractPathSelectButton.Name = "TesseractPathSelectButton";
            this.TesseractPathSelectButton.Size = new System.Drawing.Size(75, 23);
            this.TesseractPathSelectButton.TabIndex = 6;
            this.TesseractPathSelectButton.Text = "Browse";
            this.TesseractPathSelectButton.UseVisualStyleBackColor = true;
            this.TesseractPathSelectButton.Click += new System.EventHandler(this.TesseractPathSelectButton_Click);
            // 
            // ImageMagickConvertPathSelectButton
            // 
            this.ImageMagickConvertPathSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageMagickConvertPathSelectButton.Location = new System.Drawing.Point(695, 16);
            this.ImageMagickConvertPathSelectButton.Name = "ImageMagickConvertPathSelectButton";
            this.ImageMagickConvertPathSelectButton.Size = new System.Drawing.Size(75, 23);
            this.ImageMagickConvertPathSelectButton.TabIndex = 5;
            this.ImageMagickConvertPathSelectButton.Text = "Browse";
            this.ImageMagickConvertPathSelectButton.UseVisualStyleBackColor = true;
            this.ImageMagickConvertPathSelectButton.Click += new System.EventHandler(this.ImageMagickConvertPathSelectButton_Click);
            // 
            // TesseractPathTextBox
            // 
            this.TesseractPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TesseractPathTextBox.Location = new System.Drawing.Point(153, 43);
            this.TesseractPathTextBox.Name = "TesseractPathTextBox";
            this.TesseractPathTextBox.Size = new System.Drawing.Size(536, 19);
            this.TesseractPathTextBox.TabIndex = 4;
            // 
            // ImageMagickConvertPathTextBox
            // 
            this.ImageMagickConvertPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImageMagickConvertPathTextBox.Location = new System.Drawing.Point(153, 18);
            this.ImageMagickConvertPathTextBox.Name = "ImageMagickConvertPathTextBox";
            this.ImageMagickConvertPathTextBox.Size = new System.Drawing.Size(536, 19);
            this.ImageMagickConvertPathTextBox.TabIndex = 3;
            // 
            // TesseractLabel
            // 
            this.TesseractLabel.AutoSize = true;
            this.TesseractLabel.Location = new System.Drawing.Point(91, 46);
            this.TesseractLabel.Name = "TesseractLabel";
            this.TesseractLabel.Size = new System.Drawing.Size(56, 12);
            this.TesseractLabel.TabIndex = 2;
            this.TesseractLabel.Text = "Tesseract";
            this.TesseractLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ImageMagickConvertLabel
            // 
            this.ImageMagickConvertLabel.AutoSize = true;
            this.ImageMagickConvertLabel.Location = new System.Drawing.Point(6, 21);
            this.ImageMagickConvertLabel.Name = "ImageMagickConvertLabel";
            this.ImageMagickConvertLabel.Size = new System.Drawing.Size(141, 12);
            this.ImageMagickConvertLabel.TabIndex = 1;
            this.ImageMagickConvertLabel.Text = "ImageMagick (convert.exe)";
            this.ImageMagickConvertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RegistButton
            // 
            this.RegistButton.Location = new System.Drawing.Point(12, 90);
            this.RegistButton.Name = "RegistButton";
            this.RegistButton.Size = new System.Drawing.Size(75, 23);
            this.RegistButton.TabIndex = 7;
            this.RegistButton.Text = "Regist";
            this.RegistButton.UseVisualStyleBackColor = true;
            this.RegistButton.Click += new System.EventHandler(this.RegistButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(93, 90);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 121);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RegistButton);
            this.Controls.Add(this.SettingGroupBox);
            this.Name = "SettingForm";
            this.Text = "初期設定";
            this.SettingGroupBox.ResumeLayout(false);
            this.SettingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SettingGroupBox;
        private System.Windows.Forms.Button TesseractPathSelectButton;
        private System.Windows.Forms.Button ImageMagickConvertPathSelectButton;
        private System.Windows.Forms.TextBox TesseractPathTextBox;
        private System.Windows.Forms.TextBox ImageMagickConvertPathTextBox;
        private System.Windows.Forms.Label TesseractLabel;
        private System.Windows.Forms.Label ImageMagickConvertLabel;
        private System.Windows.Forms.Button RegistButton;
        private System.Windows.Forms.Button CancelButton;
    }
}