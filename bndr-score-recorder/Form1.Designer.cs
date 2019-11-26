namespace bndr_score_recorder
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.AnalyzeScoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AnalyzeScoreButton
            // 
            this.AnalyzeScoreButton.Location = new System.Drawing.Point(12, 12);
            this.AnalyzeScoreButton.Name = "AnalyzeScoreButton";
            this.AnalyzeScoreButton.Size = new System.Drawing.Size(127, 34);
            this.AnalyzeScoreButton.TabIndex = 1;
            this.AnalyzeScoreButton.Text = "Analyze Score";
            this.AnalyzeScoreButton.UseVisualStyleBackColor = true;
            this.AnalyzeScoreButton.Click += new System.EventHandler(this.AnalyzeScoreButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AnalyzeScoreButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AnalyzeScoreButton;
    }
}

