namespace BndrScoreRecorder
{
    partial class ApplicationInfoForm
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
            this.MainMessageLabel = new System.Windows.Forms.Label();
            this.GithubLinkLabel = new System.Windows.Forms.LinkLabel();
            this.TwitterLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainMessageLabel
            // 
            this.MainMessageLabel.AutoSize = true;
            this.MainMessageLabel.Location = new System.Drawing.Point(107, 9);
            this.MainMessageLabel.Name = "MainMessageLabel";
            this.MainMessageLabel.Size = new System.Drawing.Size(250, 60);
            this.MainMessageLabel.TabIndex = 0;
            this.MainMessageLabel.Text = "BNDR Score Recorder\r\nVersion 1.0\r\nUnder Apache License 2.0\r\n\r\nCreated by Kureha H" +
    "isame <kureha@gmail.com>";
            this.MainMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GithubLinkLabel
            // 
            this.GithubLinkLabel.AutoSize = true;
            this.GithubLinkLabel.Location = new System.Drawing.Point(108, 113);
            this.GithubLinkLabel.Name = "GithubLinkLabel";
            this.GithubLinkLabel.Size = new System.Drawing.Size(249, 12);
            this.GithubLinkLabel.TabIndex = 1;
            this.GithubLinkLabel.TabStop = true;
            this.GithubLinkLabel.Text = "https://github.com/kureha/bndr-score-recorder";
            this.GithubLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.GithubLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // TwitterLinkLabel
            // 
            this.TwitterLinkLabel.AutoSize = true;
            this.TwitterLinkLabel.Location = new System.Drawing.Point(159, 72);
            this.TwitterLinkLabel.Name = "TwitterLinkLabel";
            this.TwitterLinkLabel.Size = new System.Drawing.Size(147, 12);
            this.TwitterLinkLabel.TabIndex = 2;
            this.TwitterLinkLabel.TabStop = true;
            this.TwitterLinkLabel.Text = "https://twitter.com/atodelie";
            this.TwitterLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project page / Report bug";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(195, 138);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ApplicationInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 171);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TwitterLinkLabel);
            this.Controls.Add(this.GithubLinkLabel);
            this.Controls.Add(this.MainMessageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ApplicationInfoForm";
            this.Text = "Application Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MainMessageLabel;
        private System.Windows.Forms.LinkLabel GithubLinkLabel;
        private System.Windows.Forms.LinkLabel TwitterLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseButton;
    }
}