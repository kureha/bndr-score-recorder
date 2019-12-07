namespace BndrScoreRecorder
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStripSeparator01 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExecuteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyzeToolStripSeparator01 = new System.Windows.Forms.ToolStripSeparator();
            this.SetupStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropImagePositionAndSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MusicListGroupBox = new System.Windows.Forms.GroupBox();
            this.MusicTreeView = new System.Windows.Forms.TreeView();
            this.ScoreDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.MusicListGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripMenuItem,
            this.AnalyzeToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1164, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainToolStripMenuItem
            // 
            this.MainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigToolStripMenuItem,
            this.MainStripSeparator01,
            this.ExitStripMenuItem});
            this.MainToolStripMenuItem.Name = "MainToolStripMenuItem";
            this.MainToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.MainToolStripMenuItem.Text = "ファイル";
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.ConfigToolStripMenuItem.Text = "環境設定";
            // 
            // MainStripSeparator01
            // 
            this.MainStripSeparator01.Name = "MainStripSeparator01";
            this.MainStripSeparator01.Size = new System.Drawing.Size(119, 6);
            // 
            // ExitStripMenuItem
            // 
            this.ExitStripMenuItem.Name = "ExitStripMenuItem";
            this.ExitStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.ExitStripMenuItem.Text = "終了";
            // 
            // AnalyzeToolStripMenuItem
            // 
            this.AnalyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExecuteToolStripMenuItem,
            this.ExecuteAllToolStripMenuItem,
            this.AnalyzeToolStripSeparator01,
            this.SetupStripMenuItem,
            this.CropImagePositionAndSizeToolStripMenuItem});
            this.AnalyzeToolStripMenuItem.Name = "AnalyzeToolStripMenuItem";
            this.AnalyzeToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.AnalyzeToolStripMenuItem.Text = "スコア解析";
            // 
            // ExecuteToolStripMenuItem
            // 
            this.ExecuteToolStripMenuItem.Name = "ExecuteToolStripMenuItem";
            this.ExecuteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.ExecuteToolStripMenuItem.Text = "解析実行";
            this.ExecuteToolStripMenuItem.Click += new System.EventHandler(this.ExecuteToolStripMenuItem_Click);
            // 
            // ExecuteAllToolStripMenuItem
            // 
            this.ExecuteAllToolStripMenuItem.Name = "ExecuteAllToolStripMenuItem";
            this.ExecuteAllToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.ExecuteAllToolStripMenuItem.Text = "全件解析実行";
            this.ExecuteAllToolStripMenuItem.Click += new System.EventHandler(this.ExecuteAllToolStripMenuItem_Click);
            // 
            // AnalyzeToolStripSeparator01
            // 
            this.AnalyzeToolStripSeparator01.Name = "AnalyzeToolStripSeparator01";
            this.AnalyzeToolStripSeparator01.Size = new System.Drawing.Size(159, 6);
            // 
            // SetupStripMenuItem
            // 
            this.SetupStripMenuItem.Name = "SetupStripMenuItem";
            this.SetupStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.SetupStripMenuItem.Text = "初期設定";
            this.SetupStripMenuItem.Click += new System.EventHandler(this.SetupStripMenuItem_Click);
            // 
            // CropImagePositionAndSizeToolStripMenuItem
            // 
            this.CropImagePositionAndSizeToolStripMenuItem.Name = "CropImagePositionAndSizeToolStripMenuItem";
            this.CropImagePositionAndSizeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.CropImagePositionAndSizeToolStripMenuItem.Text = "画像切り取り設定";
            this.CropImagePositionAndSizeToolStripMenuItem.Click += new System.EventHandler(this.CropImagePositionAndSizeToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.HelpToolStripMenuItem.Text = "ヘルプ";
            // 
            // InfoToolStripMenuItem
            // 
            this.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem";
            this.InfoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.InfoToolStripMenuItem.Text = "アプリケーション情報";
            // 
            // MusicListGroupBox
            // 
            this.MusicListGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MusicListGroupBox.Controls.Add(this.MusicTreeView);
            this.MusicListGroupBox.Location = new System.Drawing.Point(12, 27);
            this.MusicListGroupBox.Name = "MusicListGroupBox";
            this.MusicListGroupBox.Size = new System.Drawing.Size(239, 522);
            this.MusicListGroupBox.TabIndex = 1;
            this.MusicListGroupBox.TabStop = false;
            this.MusicListGroupBox.Text = "楽曲選択";
            // 
            // MusicTreeView
            // 
            this.MusicTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MusicTreeView.Location = new System.Drawing.Point(6, 18);
            this.MusicTreeView.Name = "MusicTreeView";
            this.MusicTreeView.Size = new System.Drawing.Size(227, 498);
            this.MusicTreeView.TabIndex = 0;
            this.MusicTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.MusicTreeView_AfterSelect);
            // 
            // ScoreDataGridView
            // 
            this.ScoreDataGridView.AllowUserToAddRows = false;
            this.ScoreDataGridView.AllowUserToDeleteRows = false;
            this.ScoreDataGridView.AllowUserToOrderColumns = true;
            this.ScoreDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScoreDataGridView.Location = new System.Drawing.Point(257, 45);
            this.ScoreDataGridView.MultiSelect = false;
            this.ScoreDataGridView.Name = "ScoreDataGridView";
            this.ScoreDataGridView.ReadOnly = true;
            this.ScoreDataGridView.RowTemplate.Height = 21;
            this.ScoreDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ScoreDataGridView.Size = new System.Drawing.Size(895, 504);
            this.ScoreDataGridView.TabIndex = 2;
            this.ScoreDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScoreDataGridView_CellDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 561);
            this.Controls.Add(this.ScoreDataGridView);
            this.Controls.Add(this.MusicListGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "スコアレコーダー";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MusicListGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScoreDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator MainStripSeparator01;
        private System.Windows.Forms.ToolStripMenuItem ExitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetupStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExecuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InfoToolStripMenuItem;
        private System.Windows.Forms.GroupBox MusicListGroupBox;
        private System.Windows.Forms.TreeView MusicTreeView;
        private System.Windows.Forms.DataGridView ScoreDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ExecuteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator AnalyzeToolStripSeparator01;
        private System.Windows.Forms.ToolStripMenuItem CropImagePositionAndSizeToolStripMenuItem;
    }
}

