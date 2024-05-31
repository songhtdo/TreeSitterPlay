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
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnPaste = new System.Windows.Forms.ToolStripButton();
            this.tsbtnParse = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCopyTree = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCopySExpr = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExpand = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCollapse = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtbx = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tv = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNodeTotal = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNodePos = new System.Windows.Forms.Label();
            this.btnNextNode = new System.Windows.Forms.Button();
            this.cboNodes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbxExpr = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblExprTotal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblExprPos = new System.Windows.Forms.Label();
            this.btnNextExpr = new System.Windows.Forms.Button();
            this.cboExprs = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rboVertical = new System.Windows.Forms.RadioButton();
            this.rboHorizontal = new System.Windows.Forms.RadioButton();
            this.cboLang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrevNode = new System.Windows.Forms.Button();
            this.btnPrevExpr = new System.Windows.Forms.Button();
            this.tsbtnNewInstance = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNewInstance,
            this.tsbtnClear,
            this.toolStripSeparator3,
            this.tsbtnPaste,
            this.tsbtnParse,
            this.tsbtnCopyTree,
            this.tsbtnCopySExpr,
            this.toolStripSeparator2,
            this.tsbtnExpandAll,
            this.tsbtnExpand,
            this.tsbtnCollapse,
            this.tsbtnCollapseAll,
            this.toolStripSeparator1,
            this.tsbtnHelp,
            this.tsbtnAbout,
            this.tsbtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(9, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1327, 48);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.Image = global::TreeSitterPlay.Properties.Resources.clear;
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(69, 45);
            this.tsbtnClear.Text = "Clear";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbtnPaste
            // 
            this.tsbtnPaste.Image = global::TreeSitterPlay.Properties.Resources.paste;
            this.tsbtnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPaste.Name = "tsbtnPaste";
            this.tsbtnPaste.Size = new System.Drawing.Size(71, 45);
            this.tsbtnPaste.Text = "Paste";
            // 
            // tsbtnParse
            // 
            this.tsbtnParse.Image = global::TreeSitterPlay.Properties.Resources.parse;
            this.tsbtnParse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnParse.Name = "tsbtnParse";
            this.tsbtnParse.Size = new System.Drawing.Size(71, 45);
            this.tsbtnParse.Text = "Parse";
            // 
            // tsbtnCopyTree
            // 
            this.tsbtnCopyTree.Image = global::TreeSitterPlay.Properties.Resources.copy;
            this.tsbtnCopyTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCopyTree.Name = "tsbtnCopyTree";
            this.tsbtnCopyTree.Size = new System.Drawing.Size(102, 45);
            this.tsbtnCopyTree.Text = "CopyTree";
            // 
            // tsbtnCopySExpr
            // 
            this.tsbtnCopySExpr.Image = global::TreeSitterPlay.Properties.Resources.copysexpr;
            this.tsbtnCopySExpr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCopySExpr.Name = "tsbtnCopySExpr";
            this.tsbtnCopySExpr.Size = new System.Drawing.Size(111, 45);
            this.tsbtnCopySExpr.Text = "CopySExpr";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbtnExpandAll
            // 
            this.tsbtnExpandAll.Image = global::TreeSitterPlay.Properties.Resources.expand;
            this.tsbtnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExpandAll.Name = "tsbtnExpandAll";
            this.tsbtnExpandAll.Size = new System.Drawing.Size(110, 45);
            this.tsbtnExpandAll.Text = "Expand All";
            // 
            // tsbtnExpand
            // 
            this.tsbtnExpand.Name = "tsbtnExpand";
            this.tsbtnExpand.Size = new System.Drawing.Size(117, 45);
            this.tsbtnExpand.Text = "Expand Node";
            // 
            // tsbtnCollapse
            // 
            this.tsbtnCollapse.Name = "tsbtnCollapse";
            this.tsbtnCollapse.Size = new System.Drawing.Size(125, 45);
            this.tsbtnCollapse.Text = "Collapse Node";
            // 
            // tsbtnCollapseAll
            // 
            this.tsbtnCollapseAll.Image = global::TreeSitterPlay.Properties.Resources.collapse;
            this.tsbtnCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCollapseAll.Name = "tsbtnCollapseAll";
            this.tsbtnCollapseAll.Size = new System.Drawing.Size(118, 45);
            this.tsbtnCollapseAll.Text = "Collapse All";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
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
            this.splitContainer1.Location = new System.Drawing.Point(9, 110);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtbx);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1327, 590);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // rtbx
            // 
            this.rtbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx.Location = new System.Drawing.Point(0, 0);
            this.rtbx.Name = "rtbx";
            this.rtbx.Size = new System.Drawing.Size(294, 588);
            this.rtbx.TabIndex = 0;
            this.rtbx.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(140, 32);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1024, 588);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tv);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1000, 548);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tree Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.FullRowSelect = true;
            this.tv.HideSelection = false;
            this.tv.HotTracking = true;
            this.tv.Location = new System.Drawing.Point(3, 3);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(994, 493);
            this.tv.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblNodeTotal);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lblNodePos);
            this.panel2.Controls.Add(this.btnNextNode);
            this.panel2.Controls.Add(this.cboNodes);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnPrevNode);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 496);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(994, 49);
            this.panel2.TabIndex = 2;
            // 
            // lblNodeTotal
            // 
            this.lblNodeTotal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNodeTotal.Location = new System.Drawing.Point(652, 9);
            this.lblNodeTotal.Name = "lblNodeTotal";
            this.lblNodeTotal.Size = new System.Drawing.Size(43, 33);
            this.lblNodeTotal.TabIndex = 4;
            this.lblNodeTotal.Text = "0";
            this.lblNodeTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(635, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "/";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNodePos
            // 
            this.lblNodePos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNodePos.Location = new System.Drawing.Point(590, 9);
            this.lblNodePos.Name = "lblNodePos";
            this.lblNodePos.Size = new System.Drawing.Size(48, 33);
            this.lblNodePos.TabIndex = 3;
            this.lblNodePos.Text = "0";
            this.lblNodePos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnNextNode
            // 
            this.btnNextNode.Location = new System.Drawing.Point(830, 9);
            this.btnNextNode.Name = "btnNextNode";
            this.btnNextNode.Size = new System.Drawing.Size(122, 33);
            this.btnNextNode.TabIndex = 2;
            this.btnNextNode.Text = "Next";
            this.btnNextNode.UseVisualStyleBackColor = true;
            // 
            // cboNodes
            // 
            this.cboNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNodes.FormattingEnabled = true;
            this.cboNodes.Location = new System.Drawing.Point(135, 13);
            this.cboNodes.Name = "cboNodes";
            this.cboNodes.Size = new System.Drawing.Size(450, 24);
            this.cboNodes.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select Nodes:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbxExpr);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1016, 548);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "S-Exprssion Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbxExpr
            // 
            this.rtbxExpr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbxExpr.Location = new System.Drawing.Point(3, 3);
            this.rtbxExpr.Name = "rtbxExpr";
            this.rtbxExpr.Size = new System.Drawing.Size(1010, 493);
            this.rtbxExpr.TabIndex = 3;
            this.rtbxExpr.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblExprTotal);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.lblExprPos);
            this.panel3.Controls.Add(this.btnNextExpr);
            this.panel3.Controls.Add(this.cboExprs);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnPrevExpr);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 496);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1010, 49);
            this.panel3.TabIndex = 4;
            // 
            // lblExprTotal
            // 
            this.lblExprTotal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExprTotal.Location = new System.Drawing.Point(652, 9);
            this.lblExprTotal.Name = "lblExprTotal";
            this.lblExprTotal.Size = new System.Drawing.Size(43, 33);
            this.lblExprTotal.TabIndex = 7;
            this.lblExprTotal.Text = "0";
            this.lblExprTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(635, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "/";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExprPos
            // 
            this.lblExprPos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExprPos.Location = new System.Drawing.Point(590, 9);
            this.lblExprPos.Name = "lblExprPos";
            this.lblExprPos.Size = new System.Drawing.Size(48, 33);
            this.lblExprPos.TabIndex = 6;
            this.lblExprPos.Text = "0";
            this.lblExprPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnNextExpr
            // 
            this.btnNextExpr.Location = new System.Drawing.Point(830, 9);
            this.btnNextExpr.Name = "btnNextExpr";
            this.btnNextExpr.Size = new System.Drawing.Size(122, 33);
            this.btnNextExpr.TabIndex = 2;
            this.btnNextExpr.Text = "Next";
            this.btnNextExpr.UseVisualStyleBackColor = true;
            // 
            // cboExprs
            // 
            this.cboExprs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExprs.FormattingEnabled = true;
            this.cboExprs.Location = new System.Drawing.Point(135, 13);
            this.cboExprs.Name = "cboExprs";
            this.cboExprs.Size = new System.Drawing.Size(450, 24);
            this.cboExprs.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Select Exprs:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rboVertical);
            this.panel1.Controls.Add(this.rboHorizontal);
            this.panel1.Controls.Add(this.cboLang);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(9, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1327, 53);
            this.panel1.TabIndex = 2;
            // 
            // rboVertical
            // 
            this.rboVertical.AutoSize = true;
            this.rboVertical.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rboVertical.Location = new System.Drawing.Point(543, 19);
            this.rboVertical.Name = "rboVertical";
            this.rboVertical.Size = new System.Drawing.Size(98, 20);
            this.rboVertical.TabIndex = 4;
            this.rboVertical.Text = "Vertical";
            this.rboVertical.UseVisualStyleBackColor = true;
            // 
            // rboHorizontal
            // 
            this.rboHorizontal.AutoSize = true;
            this.rboHorizontal.Checked = true;
            this.rboHorizontal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rboHorizontal.Location = new System.Drawing.Point(412, 19);
            this.rboHorizontal.Name = "rboHorizontal";
            this.rboHorizontal.Size = new System.Drawing.Size(116, 20);
            this.rboHorizontal.TabIndex = 3;
            this.rboHorizontal.TabStop = true;
            this.rboHorizontal.Text = "Horizontal";
            this.rboHorizontal.UseVisualStyleBackColor = true;
            // 
            // cboLang
            // 
            this.cboLang.FormattingEnabled = true;
            this.cboLang.Location = new System.Drawing.Point(105, 17);
            this.cboLang.Name = "cboLang";
            this.cboLang.Size = new System.Drawing.Size(195, 24);
            this.cboLang.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Language:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(324, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layout:";
            // 
            // btnPrevNode
            // 
            this.btnPrevNode.Location = new System.Drawing.Point(694, 9);
            this.btnPrevNode.Name = "btnPrevNode";
            this.btnPrevNode.Size = new System.Drawing.Size(122, 33);
            this.btnPrevNode.TabIndex = 5;
            this.btnPrevNode.Text = "Previous";
            this.btnPrevNode.UseVisualStyleBackColor = true;
            // 
            // btnPrevExpr
            // 
            this.btnPrevExpr.Location = new System.Drawing.Point(694, 9);
            this.btnPrevExpr.Name = "btnPrevExpr";
            this.btnPrevExpr.Size = new System.Drawing.Size(122, 33);
            this.btnPrevExpr.TabIndex = 8;
            this.btnPrevExpr.Text = "Previous";
            this.btnPrevExpr.UseVisualStyleBackColor = true;
            // 
            // tsbtnNewInstance
            // 
            this.tsbtnNewInstance.Image = global::TreeSitterPlay.Properties.Resources.new_instance;
            this.tsbtnNewInstance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNewInstance.Name = "tsbtnNewInstance";
            this.tsbtnNewInstance.Size = new System.Drawing.Size(129, 45);
            this.tsbtnNewInstance.Text = "NewInstance";
            // 
            // TreeSiterPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 709);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnAbout;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtbx;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ToolStripButton tsbtnExpandAll;
        private System.Windows.Forms.ToolStripButton tsbtnCollapseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnHelp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNextNode;
        private System.Windows.Forms.ComboBox cboNodes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbtnCopyTree;
        private System.Windows.Forms.ToolStripButton tsbtnCopySExpr;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbxExpr;
        private System.Windows.Forms.ToolStripButton tsbtnParse;
        private System.Windows.Forms.ToolStripButton tsbtnExpand;
        private System.Windows.Forms.ToolStripButton tsbtnCollapse;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rboVertical;
        private System.Windows.Forms.RadioButton rboHorizontal;
        private System.Windows.Forms.ComboBox cboLang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbtnPaste;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNextExpr;
        private System.Windows.Forms.ComboBox cboExprs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNodeTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNodePos;
        private System.Windows.Forms.Label lblExprTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblExprPos;
        private System.Windows.Forms.Button btnPrevNode;
        private System.Windows.Forms.Button btnPrevExpr;
        private System.Windows.Forms.ToolStripButton tsbtnNewInstance;
    }
}

