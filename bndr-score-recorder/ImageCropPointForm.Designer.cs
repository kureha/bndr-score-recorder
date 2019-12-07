namespace BndrScoreRecorder
{
    partial class ImageCropPointForm
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
            this.PointGroupBox = new System.Windows.Forms.GroupBox();
            this.HeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.WidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PositionYNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PositionXNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.PositionYLabel = new System.Windows.Forms.Label();
            this.PositionXLabel = new System.Windows.Forms.Label();
            this.ExpectedGroupBox = new System.Windows.Forms.GroupBox();
            this.ConfirmMessageLabel = new System.Windows.Forms.Label();
            this.CropResultTextLabel = new System.Windows.Forms.Label();
            this.CropResultTextBox = new System.Windows.Forms.TextBox();
            this.CropResultPictureBox = new System.Windows.Forms.PictureBox();
            this.CropResultPictureBoxLabel = new System.Windows.Forms.Label();
            this.ExpectedLabel = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CropPictureGroupBox = new System.Windows.Forms.GroupBox();
            this.CropPicturePanel = new System.Windows.Forms.Panel();
            this.CropPictureBox = new System.Windows.Forms.PictureBox();
            this.AbortButton = new System.Windows.Forms.Button();
            this.TryCropButton = new System.Windows.Forms.Button();
            this.SelectOcrSettingListBox = new System.Windows.Forms.ListBox();
            this.SaveSettingButton = new System.Windows.Forms.Button();
            this.LoadSettingButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.PointGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionYNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionXNumericUpDown)).BeginInit();
            this.ExpectedGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CropResultPictureBox)).BeginInit();
            this.CropPictureGroupBox.SuspendLayout();
            this.CropPicturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CropPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PointGroupBox
            // 
            this.PointGroupBox.Controls.Add(this.HeightNumericUpDown);
            this.PointGroupBox.Controls.Add(this.WidthNumericUpDown);
            this.PointGroupBox.Controls.Add(this.PositionYNumericUpDown);
            this.PointGroupBox.Controls.Add(this.PositionXNumericUpDown);
            this.PointGroupBox.Controls.Add(this.HeightLabel);
            this.PointGroupBox.Controls.Add(this.WidthLabel);
            this.PointGroupBox.Controls.Add(this.PositionYLabel);
            this.PointGroupBox.Controls.Add(this.PositionXLabel);
            this.PointGroupBox.Location = new System.Drawing.Point(12, 111);
            this.PointGroupBox.Name = "PointGroupBox";
            this.PointGroupBox.Size = new System.Drawing.Size(330, 73);
            this.PointGroupBox.TabIndex = 0;
            this.PointGroupBox.TabStop = false;
            this.PointGroupBox.Text = "Position x Size";
            // 
            // HeightNumericUpDown
            // 
            this.HeightNumericUpDown.Location = new System.Drawing.Point(219, 43);
            this.HeightNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.HeightNumericUpDown.Name = "HeightNumericUpDown";
            this.HeightNumericUpDown.Size = new System.Drawing.Size(88, 19);
            this.HeightNumericUpDown.TabIndex = 7;
            this.HeightNumericUpDown.ValueChanged += new System.EventHandler(this.PositionNumericUpDown_ValueChanged);
            // 
            // WidthNumericUpDown
            // 
            this.WidthNumericUpDown.Location = new System.Drawing.Point(219, 18);
            this.WidthNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.WidthNumericUpDown.Name = "WidthNumericUpDown";
            this.WidthNumericUpDown.Size = new System.Drawing.Size(88, 19);
            this.WidthNumericUpDown.TabIndex = 6;
            this.WidthNumericUpDown.ValueChanged += new System.EventHandler(this.PositionNumericUpDown_ValueChanged);
            // 
            // PositionYNumericUpDown
            // 
            this.PositionYNumericUpDown.Location = new System.Drawing.Point(69, 43);
            this.PositionYNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.PositionYNumericUpDown.Name = "PositionYNumericUpDown";
            this.PositionYNumericUpDown.Size = new System.Drawing.Size(88, 19);
            this.PositionYNumericUpDown.TabIndex = 5;
            this.PositionYNumericUpDown.ValueChanged += new System.EventHandler(this.PositionNumericUpDown_ValueChanged);
            // 
            // PositionXNumericUpDown
            // 
            this.PositionXNumericUpDown.Location = new System.Drawing.Point(69, 18);
            this.PositionXNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.PositionXNumericUpDown.Name = "PositionXNumericUpDown";
            this.PositionXNumericUpDown.Size = new System.Drawing.Size(88, 19);
            this.PositionXNumericUpDown.TabIndex = 4;
            this.PositionXNumericUpDown.ValueChanged += new System.EventHandler(this.PositionNumericUpDown_ValueChanged);
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(175, 46);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(38, 12);
            this.HeightLabel.TabIndex = 3;
            this.HeightLabel.Text = "Height";
            this.HeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(180, 21);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(33, 12);
            this.WidthLabel.TabIndex = 2;
            this.WidthLabel.Text = "Width";
            this.WidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PositionYLabel
            // 
            this.PositionYLabel.AutoSize = true;
            this.PositionYLabel.Location = new System.Drawing.Point(6, 46);
            this.PositionYLabel.Name = "PositionYLabel";
            this.PositionYLabel.Size = new System.Drawing.Size(57, 12);
            this.PositionYLabel.TabIndex = 1;
            this.PositionYLabel.Text = "Position Y";
            this.PositionYLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PositionXLabel
            // 
            this.PositionXLabel.AutoSize = true;
            this.PositionXLabel.Location = new System.Drawing.Point(6, 21);
            this.PositionXLabel.Name = "PositionXLabel";
            this.PositionXLabel.Size = new System.Drawing.Size(57, 12);
            this.PositionXLabel.TabIndex = 0;
            this.PositionXLabel.Text = "Position X";
            this.PositionXLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExpectedGroupBox
            // 
            this.ExpectedGroupBox.Controls.Add(this.ConfirmMessageLabel);
            this.ExpectedGroupBox.Controls.Add(this.CropResultTextLabel);
            this.ExpectedGroupBox.Controls.Add(this.CropResultTextBox);
            this.ExpectedGroupBox.Controls.Add(this.CropResultPictureBox);
            this.ExpectedGroupBox.Controls.Add(this.CropResultPictureBoxLabel);
            this.ExpectedGroupBox.Controls.Add(this.ExpectedLabel);
            this.ExpectedGroupBox.Controls.Add(this.DescriptionTextBox);
            this.ExpectedGroupBox.Location = new System.Drawing.Point(12, 219);
            this.ExpectedGroupBox.Name = "ExpectedGroupBox";
            this.ExpectedGroupBox.Size = new System.Drawing.Size(330, 421);
            this.ExpectedGroupBox.TabIndex = 1;
            this.ExpectedGroupBox.TabStop = false;
            this.ExpectedGroupBox.Text = "試験出力";
            // 
            // ConfirmMessageLabel
            // 
            this.ConfirmMessageLabel.AutoSize = true;
            this.ConfirmMessageLabel.Location = new System.Drawing.Point(6, 406);
            this.ConfirmMessageLabel.Name = "ConfirmMessageLabel";
            this.ConfirmMessageLabel.Size = new System.Drawing.Size(237, 12);
            this.ConfirmMessageLabel.TabIndex = 6;
            this.ConfirmMessageLabel.Text = "結果が期待した文字列であれば問題ありません。";
            // 
            // CropResultTextLabel
            // 
            this.CropResultTextLabel.AutoSize = true;
            this.CropResultTextLabel.Location = new System.Drawing.Point(6, 292);
            this.CropResultTextLabel.Name = "CropResultTextLabel";
            this.CropResultTextLabel.Size = new System.Drawing.Size(72, 12);
            this.CropResultTextLabel.TabIndex = 5;
            this.CropResultTextLabel.Text = "読み取り結果";
            // 
            // CropResultTextBox
            // 
            this.CropResultTextBox.Location = new System.Drawing.Point(6, 307);
            this.CropResultTextBox.Multiline = true;
            this.CropResultTextBox.Name = "CropResultTextBox";
            this.CropResultTextBox.ReadOnly = true;
            this.CropResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CropResultTextBox.Size = new System.Drawing.Size(318, 96);
            this.CropResultTextBox.TabIndex = 4;
            // 
            // CropResultPictureBox
            // 
            this.CropResultPictureBox.Location = new System.Drawing.Point(6, 134);
            this.CropResultPictureBox.Name = "CropResultPictureBox";
            this.CropResultPictureBox.Size = new System.Drawing.Size(318, 155);
            this.CropResultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CropResultPictureBox.TabIndex = 3;
            this.CropResultPictureBox.TabStop = false;
            // 
            // CropResultPictureBoxLabel
            // 
            this.CropResultPictureBoxLabel.AutoSize = true;
            this.CropResultPictureBoxLabel.Location = new System.Drawing.Point(4, 119);
            this.CropResultPictureBoxLabel.Name = "CropResultPictureBoxLabel";
            this.CropResultPictureBoxLabel.Size = new System.Drawing.Size(69, 12);
            this.CropResultPictureBoxLabel.TabIndex = 2;
            this.CropResultPictureBoxLabel.Text = "切り取り結果";
            // 
            // ExpectedLabel
            // 
            this.ExpectedLabel.AutoSize = true;
            this.ExpectedLabel.Location = new System.Drawing.Point(4, 15);
            this.ExpectedLabel.Name = "ExpectedLabel";
            this.ExpectedLabel.Size = new System.Drawing.Size(153, 12);
            this.ExpectedLabel.TabIndex = 1;
            this.ExpectedLabel.Text = "画像から期待される出力内容：";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(6, 30);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionTextBox.Size = new System.Drawing.Size(318, 86);
            this.DescriptionTextBox.TabIndex = 0;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 646);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(125, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save Setting";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.RegistButton_Click);
            // 
            // CropPictureGroupBox
            // 
            this.CropPictureGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CropPictureGroupBox.Controls.Add(this.CropPicturePanel);
            this.CropPictureGroupBox.Location = new System.Drawing.Point(348, 12);
            this.CropPictureGroupBox.Name = "CropPictureGroupBox";
            this.CropPictureGroupBox.Size = new System.Drawing.Size(904, 657);
            this.CropPictureGroupBox.TabIndex = 3;
            this.CropPictureGroupBox.TabStop = false;
            this.CropPictureGroupBox.Text = "クリック＆ドラッグによる切り取り";
            // 
            // CropPicturePanel
            // 
            this.CropPicturePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CropPicturePanel.AutoScroll = true;
            this.CropPicturePanel.Controls.Add(this.CropPictureBox);
            this.CropPicturePanel.Location = new System.Drawing.Point(6, 18);
            this.CropPicturePanel.Name = "CropPicturePanel";
            this.CropPicturePanel.Size = new System.Drawing.Size(898, 639);
            this.CropPicturePanel.TabIndex = 0;
            // 
            // CropPictureBox
            // 
            this.CropPictureBox.Location = new System.Drawing.Point(0, 0);
            this.CropPictureBox.Name = "CropPictureBox";
            this.CropPictureBox.Size = new System.Drawing.Size(100, 50);
            this.CropPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.CropPictureBox.TabIndex = 0;
            this.CropPictureBox.TabStop = false;
            this.CropPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CropPictureBox_MouseDown);
            this.CropPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CropPictureBox_MouseMove);
            this.CropPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CropPictureBox_MouseUp);
            // 
            // AbortButton
            // 
            this.AbortButton.Location = new System.Drawing.Point(217, 646);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(125, 23);
            this.AbortButton.TabIndex = 4;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // TryCropButton
            // 
            this.TryCropButton.Location = new System.Drawing.Point(12, 190);
            this.TryCropButton.Name = "TryCropButton";
            this.TryCropButton.Size = new System.Drawing.Size(330, 23);
            this.TryCropButton.TabIndex = 5;
            this.TryCropButton.Text = "Try to Analyze";
            this.TryCropButton.UseVisualStyleBackColor = true;
            this.TryCropButton.Click += new System.EventHandler(this.TryCropButton_Click);
            // 
            // SelectOcrSettingListBox
            // 
            this.SelectOcrSettingListBox.FormattingEnabled = true;
            this.SelectOcrSettingListBox.ItemHeight = 12;
            this.SelectOcrSettingListBox.Location = new System.Drawing.Point(12, 12);
            this.SelectOcrSettingListBox.Name = "SelectOcrSettingListBox";
            this.SelectOcrSettingListBox.Size = new System.Drawing.Size(330, 64);
            this.SelectOcrSettingListBox.TabIndex = 6;
            this.SelectOcrSettingListBox.SelectedIndexChanged += new System.EventHandler(this.SelectOcrSettingListBox_SelectedIndexChanged);
            // 
            // SaveSettingButton
            // 
            this.SaveSettingButton.Location = new System.Drawing.Point(217, 82);
            this.SaveSettingButton.Name = "SaveSettingButton";
            this.SaveSettingButton.Size = new System.Drawing.Size(125, 23);
            this.SaveSettingButton.TabIndex = 8;
            this.SaveSettingButton.Text = "↑ SAVE";
            this.SaveSettingButton.UseVisualStyleBackColor = true;
            this.SaveSettingButton.Click += new System.EventHandler(this.SaveSettingButton_Click);
            // 
            // LoadSettingButton
            // 
            this.LoadSettingButton.Location = new System.Drawing.Point(12, 82);
            this.LoadSettingButton.Name = "LoadSettingButton";
            this.LoadSettingButton.Size = new System.Drawing.Size(125, 23);
            this.LoadSettingButton.TabIndex = 7;
            this.LoadSettingButton.Text = "↓ LOAD";
            this.LoadSettingButton.UseVisualStyleBackColor = true;
            this.LoadSettingButton.Click += new System.EventHandler(this.LoadSettingButton_Click);
            // 
            // ImageCropPointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.SaveSettingButton);
            this.Controls.Add(this.LoadSettingButton);
            this.Controls.Add(this.SelectOcrSettingListBox);
            this.Controls.Add(this.TryCropButton);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.CropPictureGroupBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ExpectedGroupBox);
            this.Controls.Add(this.PointGroupBox);
            this.Name = "ImageCropPointForm";
            this.Text = "画像切り取り設定";
            this.PointGroupBox.ResumeLayout(false);
            this.PointGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionYNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionXNumericUpDown)).EndInit();
            this.ExpectedGroupBox.ResumeLayout(false);
            this.ExpectedGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CropResultPictureBox)).EndInit();
            this.CropPictureGroupBox.ResumeLayout(false);
            this.CropPicturePanel.ResumeLayout(false);
            this.CropPicturePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CropPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PointGroupBox;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.Label PositionYLabel;
        private System.Windows.Forms.Label PositionXLabel;
        private System.Windows.Forms.GroupBox ExpectedGroupBox;
        private System.Windows.Forms.Label ConfirmMessageLabel;
        private System.Windows.Forms.Label CropResultTextLabel;
        private System.Windows.Forms.TextBox CropResultTextBox;
        private System.Windows.Forms.PictureBox CropResultPictureBox;
        private System.Windows.Forms.Label CropResultPictureBoxLabel;
        private System.Windows.Forms.Label ExpectedLabel;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox CropPictureGroupBox;
        private System.Windows.Forms.Panel CropPicturePanel;
        private System.Windows.Forms.PictureBox CropPictureBox;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.NumericUpDown HeightNumericUpDown;
        private System.Windows.Forms.NumericUpDown WidthNumericUpDown;
        private System.Windows.Forms.NumericUpDown PositionYNumericUpDown;
        private System.Windows.Forms.NumericUpDown PositionXNumericUpDown;
        private System.Windows.Forms.Button TryCropButton;
        private System.Windows.Forms.ListBox SelectOcrSettingListBox;
        private System.Windows.Forms.Button SaveSettingButton;
        private System.Windows.Forms.Button LoadSettingButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}