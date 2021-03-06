﻿namespace BndrScoreRecorder
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
            this.components = new System.ComponentModel.Container();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExePathSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CropImageRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStripSeparator01 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExecuteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ApplicationInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MusicListGroupBox = new System.Windows.Forms.GroupBox();
            this.MusicTreeView = new System.Windows.Forms.TreeView();
            this.ScoreDataGridView = new System.Windows.Forms.DataGridView();
            this.SocreDataGridViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ScoreEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScoreDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            this.MusicListGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreDataGridView)).BeginInit();
            this.SocreDataGridViewContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripMenuItem,
            this.AnalyzeToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(1164, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
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
            this.ConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExePathSettingToolStripMenuItem,
            this.CropImageRangeToolStripMenuItem});
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.ConfigToolStripMenuItem.Text = "環境設定";
            // 
            // ExePathSettingToolStripMenuItem
            // 
            this.ExePathSettingToolStripMenuItem.Name = "ExePathSettingToolStripMenuItem";
            this.ExePathSettingToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.ExePathSettingToolStripMenuItem.Text = "外部EXEパス設定";
            this.ExePathSettingToolStripMenuItem.Click += new System.EventHandler(this.ExePathSettingToolStripMenuItem_Click);
            // 
            // CropImageRangeToolStripMenuItem
            // 
            this.CropImageRangeToolStripMenuItem.Name = "CropImageRangeToolStripMenuItem";
            this.CropImageRangeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.CropImageRangeToolStripMenuItem.Text = "画像切り抜き座標設定";
            this.CropImageRangeToolStripMenuItem.Click += new System.EventHandler(this.CropImageRangeToolStripMenuItem_Click);
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
            this.ExitStripMenuItem.Click += new System.EventHandler(this.ExitStripMenuItem_Click);
            // 
            // AnalyzeToolStripMenuItem
            // 
            this.AnalyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExecuteToolStripMenuItem,
            this.ExecuteAllToolStripMenuItem});
            this.AnalyzeToolStripMenuItem.Name = "AnalyzeToolStripMenuItem";
            this.AnalyzeToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.AnalyzeToolStripMenuItem.Text = "スコア解析";
            // 
            // ExecuteToolStripMenuItem
            // 
            this.ExecuteToolStripMenuItem.Name = "ExecuteToolStripMenuItem";
            this.ExecuteToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ExecuteToolStripMenuItem.Text = "解析実行";
            this.ExecuteToolStripMenuItem.Click += new System.EventHandler(this.ExecuteToolStripMenuItem_Click);
            // 
            // ExecuteAllToolStripMenuItem
            // 
            this.ExecuteAllToolStripMenuItem.Name = "ExecuteAllToolStripMenuItem";
            this.ExecuteAllToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ExecuteAllToolStripMenuItem.Text = "全件解析実行";
            this.ExecuteAllToolStripMenuItem.Click += new System.EventHandler(this.ExecuteAllToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplicationInfoToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.HelpToolStripMenuItem.Text = "ヘルプ";
            // 
            // ApplicationInfoToolStripMenuItem
            // 
            this.ApplicationInfoToolStripMenuItem.Name = "ApplicationInfoToolStripMenuItem";
            this.ApplicationInfoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.ApplicationInfoToolStripMenuItem.Text = "アプリケーション情報";
            this.ApplicationInfoToolStripMenuItem.Click += new System.EventHandler(this.ApplicationInfoToolStripMenuItem_Click);
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
            this.ScoreDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ScoreDataGridView_CellMouseClick);
            // 
            // SocreDataGridViewContextMenuStrip
            // 
            this.SocreDataGridViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScoreEditToolStripMenuItem,
            this.ScoreDeleteToolStripMenuItem});
            this.SocreDataGridViewContextMenuStrip.Name = "SocreDataGridViewContextMenuStrip";
            this.SocreDataGridViewContextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // ScoreEditToolStripMenuItem
            // 
            this.ScoreEditToolStripMenuItem.Name = "ScoreEditToolStripMenuItem";
            this.ScoreEditToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ScoreEditToolStripMenuItem.Text = "編集";
            this.ScoreEditToolStripMenuItem.Click += new System.EventHandler(this.ScoreEditToolStripMenuItem_Click);
            // 
            // ScoreDeleteToolStripMenuItem
            // 
            this.ScoreDeleteToolStripMenuItem.Name = "ScoreDeleteToolStripMenuItem";
            this.ScoreDeleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ScoreDeleteToolStripMenuItem.Text = "削除";
            this.ScoreDeleteToolStripMenuItem.Click += new System.EventHandler(this.ScoreDeleteToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 561);
            this.Controls.Add(this.ScoreDataGridView);
            this.Controls.Add(this.MusicListGroupBox);
            this.Controls.Add(this.MainMenuStrip);
            this.MainMenuStrip = this.MainMenuStrip;
            this.Name = "MainForm";
            this.Text = "BNDRスコアレコーダー";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.MusicListGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScoreDataGridView)).EndInit();
            this.SocreDataGridViewContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator MainStripSeparator01;
        private System.Windows.Forms.ToolStripMenuItem ExitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExecuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ApplicationInfoToolStripMenuItem;
        private System.Windows.Forms.GroupBox MusicListGroupBox;
        private System.Windows.Forms.TreeView MusicTreeView;
        private System.Windows.Forms.DataGridView ScoreDataGridView;
        private System.Windows.Forms.ToolStripMenuItem ExecuteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExePathSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CropImageRangeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip SocreDataGridViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ScoreEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScoreDeleteToolStripMenuItem;
    }
}

