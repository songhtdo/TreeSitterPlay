namespace TreeSitterPlay
{
    partial class TreeSiterPlayForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeSiterPlayForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscboLang = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnParse = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbx = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.tbx = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbo,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tscboLang,
            this.toolStripSeparator1,
            this.tsbtnParse,
            this.tsbtnExpandAll,
            this.tsbtnCollapseAll,
            this.tsbtnClear,
            this.toolStripSeparator3,
            this.tsbtnHelp,
            this.tsbtnAbout,
            this.tsbtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(9, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1180, 48);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(61, 45);
            this.toolStripLabel1.Text = "Layout";
            // 
            // tscbo
            // 
            this.tscbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscbo.Items.AddRange(new object[] {
            "Vertical",
            "Horizontal"});
            this.tscbo.Name = "tscbo";
            this.tscbo.Size = new System.Drawing.Size(121, 48);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(85, 45);
            this.toolStripLabel2.Text = "Language";
            // 
            // tscboLang
            // 
            this.tscboLang.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscboLang.Name = "tscboLang";
            this.tscboLang.Size = new System.Drawing.Size(160, 48);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbtnParse
            // 
            this.tsbtnParse.Image = global::TreeSitterPlay.Properties.Resources.parse;
            this.tsbtnParse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnParse.Name = "tsbtnParse";
            this.tsbtnParse.Size = new System.Drawing.Size(71, 45);
            this.tsbtnParse.Text = "Parse";
            // 
            // tsbtnExpandAll
            // 
            this.tsbtnExpandAll.Image = global::TreeSitterPlay.Properties.Resources.expand;
            this.tsbtnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExpandAll.Name = "tsbtnExpandAll";
            this.tsbtnExpandAll.Size = new System.Drawing.Size(105, 45);
            this.tsbtnExpandAll.Text = "ExpandAll";
            // 
            // tsbtnCollapseAll
            // 
            this.tsbtnCollapseAll.Image = global::TreeSitterPlay.Properties.Resources.collapse;
            this.tsbtnCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCollapseAll.Name = "tsbtnCollapseAll";
            this.tsbtnCollapseAll.Size = new System.Drawing.Size(113, 45);
            this.tsbtnCollapseAll.Text = "CollapseAll";
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.Image = global::TreeSitterPlay.Properties.Resources.clear;
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(105, 45);
            this.tsbtnClear.Text = "ClearData";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbtnHelp
            // 
            this.tsbtnHelp.Image = global::TreeSitterPlay.Properties.Resources.info;
            this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHelp.Name = "tsbtnHelp";
            this.tsbtnHelp.Size = new System.Drawing.Size(77, 45);
            this.tsbtnHelp.Text = "Help...";
            // 
            // tsbtnAbout
            // 
            this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAbout.Name = "tsbtnAbout";
            this.tsbtnAbout.Size = new System.Drawing.Size(73, 45);
            this.tsbtnAbout.Text = "About...";
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.Image = global::TreeSitterPlay.Properties.Resources.exit;
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(57, 45);
            this.tsbtnExit.Text = "Exit";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(9, 57);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbx);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1180, 643);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // rtbx
            // 
            this.rtbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx.Location = new System.Drawing.Point(0, 0);
            this.rtbx.Name = "rtbx";
            this.rtbx.Size = new System.Drawing.Size(1178, 168);
            this.rtbx.TabIndex = 0;
            this.rtbx.Text = "";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tv);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbx);
            this.splitContainer2.Size = new System.Drawing.Size(1180, 468);
            this.splitContainer2.SplitterDistance = 729;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(727, 466);
            this.tv.TabIndex = 0;
            // 
            // tbx
            // 
            this.tbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx.Location = new System.Drawing.Point(0, 0);
            this.tbx.Multiline = true;
            this.tbx.Name = "tbx";
            this.tbx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbx.Size = new System.Drawing.Size(444, 466);
            this.tbx.TabIndex = 0;
            this.tbx.WordWrap = false;
            // 
            // TreeSiterPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 709);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TreeSiterPlayForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.Text = "TreeSitterPlay";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnParse;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnAbout;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbx;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.TextBox tbx;
        private System.Windows.Forms.ToolStripButton tsbtnExpandAll;
        private System.Windows.Forms.ToolStripButton tsbtnCollapseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscboLang;
        private System.Windows.Forms.ToolStripButton tsbtnHelp;
    }
}

