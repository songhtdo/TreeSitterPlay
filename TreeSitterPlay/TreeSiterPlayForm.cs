using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using GitHub.TreeSitter;

namespace TreeSitterPlay
{
    public partial class TreeSiterPlayForm : Form
    {
        // (struct_specifier 或 (qualified_identifier 
        private const string NODE_MATCH_PAT = @"^({0})\s|\s({0})\s|\s({0})$|^({0})$";
        // (qualified_identifier 或 (identifier)
        private const string EXPR_MATCH_PAT = @"\({0}\)|\({0}";
        private KeyMessageFilter keyFilter = null;
        private Dictionary<string, string> lang_mapping = null;
        private List<KeyValuePair<int, int>> srcRowLengths = new List<KeyValuePair<int, int>>();
        private LanguageEntry lang = null;
        private TSTree root_tree = null;
        private List<TreeNode> matchedNodes = new List<TreeNode>();
        private List<MatchedPoint> matchedExprs = new List<MatchedPoint>();
        private int matchedNodePos = -1;
        private int matchedExprPos = -1;

        public TreeSiterPlayForm()
        {
            InitializeComponent();

            this.Load += TreeSiterPlayForm_Load;
            this.tsbtnParse.Click += TsbtnParse_Click;
            this.tsbtnExpandAll.Click += TsbtnExpandAll_Click;
            this.tsbtnCollapseAll.Click += TsbtnCollapseAll_Click;
            this.tsbtnNewData.Click += TsbtnNewData_Click;
            this.tsbtnAbout.Click += TsbtnAbout_Click;
            this.tsbtnExit.Click += TsbtnExit_Click;
            this.rboHorizontal.CheckedChanged += RboHorizontal_CheckedChanged;
            this.rboVertical.CheckedChanged += RboVertical_CheckedChanged;
            this.cboLang.SelectedIndexChanged += CboLang_SelectedIndexChanged;
            this.tv.AfterSelect += Tv_AfterSelect;
            this.tvQueryResult.AfterSelect += TvQueryResult_AfterSelect;
            this.tsbtnCopyTree.Click += TsbtnCopyTree_Click;
            this.tsbtnCopySExpr.Click += TsbtnCopySExpr_Click;
            this.cboNodes.SelectedIndexChanged += CboNodes_SelectedIndexChanged;
            this.tsbtnPaste.Click += TsbtnPaste_Click;
            this.btnNextNode.Click += BtnNextNode_Click;
            this.btnPrevNode.Click += BtnPrevNode_Click;
            this.tsbtnExpand.Click += TsbtnExpand_Click;
            this.tsbtnCollapse.Click += TsbtnCollapse_Click;
            this.cboExprs.SelectedIndexChanged += CboExprs_SelectedIndexChanged;
            this.btnNextExpr.Click += BtnNextExpr_Click;
            this.btnPrevExpr.Click += BtnPrevExpr_Click;
            this.tsbtnNewInstance.Click += TsbtnNewInstance_Click;
            this.tsbtnLoadFile.Click += TsbtnLoadFile_Click;
            this.tsbtnSaveFile.Click += TsbtnSaveFile_Click;
            this.btnQuery.Click += BtnQuery_Click;
        }

        ~TreeSiterPlayForm()
        {
            this.keyFilter.StopFilter();
            if(this.lang != null)
            {
                TreeSitterLanguage.releaseLanguage(this.lang);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(this.keyFilter.PreFilterMessage(ref msg, keyData))
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void TreeSiterPlayForm_Load(object sender, EventArgs e)
        {
            //注册事件
            this.keyFilter = new KeyMessageFilter(this);
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.O, () =>
            {
                this.tsbtnLoadFile.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.S, () =>
            {
                this.tsbtnSaveFile.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.N, () => {
                this.tsbtnNewData.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.D, () =>
            {
                this.tsbtnNewData.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.V, () =>
            {
                this.tsbtnPaste.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.Enter, () =>
            {
                this.tsbtnParse.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.P, () =>
            {
                this.tsbtnParse.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.Q, () =>
            {
                this.btnQuery.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.E, () =>
            {
                this.tsbtnExpandAll.PerformClick();
            });
            this.keyFilter.RegisterHotkey(Keys.Alt | Keys.G, () => {
                this.tsbtnCollapseAll.PerformClick();
            });
            this.keyFilter.StartFilter();

            this.lang_mapping = TreeSitterLanguage.loadLanguages();
            if(this.lang_mapping != null && this.lang_mapping.Count > 0)
            {
                foreach(var item in this.lang_mapping)
                {
                    this.cboLang.Items.Add(item.Key);
                }
                this.cboLang.SelectedIndex = 0;
            }
            this.cboLang.DropDownStyle = ComboBoxStyle.DropDownList;           
        }
        private void TsbtnNewInstance_Click(object sender, EventArgs e)
        {
            // 创建一个新的进程对象
            Process newProcess = new Process();
            newProcess.StartInfo.FileName = Application.ExecutablePath;
            newProcess.Start(); // 启动新实例
        }
        private void TsbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TsbtnSaveFile_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(this.rtbx.Text))
            {
                this.ShowMessage("Failed to save, the content is empty.");
                return;
            }
            using (SaveFileDialog sfdialog = new SaveFileDialog())
            {
                sfdialog.Filter = AppStrings.TREE_SITTER_FILEEXT + "|all files|*.*";
                sfdialog.FilterIndex = 0;
                var dialogRes = sfdialog.ShowDialog();
                if (dialogRes == DialogResult.OK)
                {
                    var saveRes = FileVisitor.SaveFile(sfdialog.FileName, this.rtbx.Text);
                    this.ShowMessage(saveRes ? string.Format("The file is saved successfully.\n{0}", sfdialog.FileName) 
                        : string.Format("The file is saved failed.\n{0}", sfdialog.FileName));
                }
            }
        }

        private void TsbtnLoadFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.rtbx.Text))
            {
                if(this.ShowMessage("The current window has content, you are sure to load new content.\n Select \"Yes\" to continue loading, select \"No\" return no loading.", 
                    MessageBoxButtons.YesNo) != DialogResult.OK)
                {
                    return;
                }
                this.ClearFormData();
            }
            using (OpenFileDialog ofdialog = new OpenFileDialog())
            {
                ofdialog.Filter = AppStrings.TREE_SITTER_FILEEXT + "|all files|*.*";
                ofdialog.FilterIndex = 0;
                var dialogRes = ofdialog.ShowDialog();
                if(dialogRes == DialogResult.OK)
                {
                    string content = "";
                    var openRes = FileVisitor.LoadFile(ofdialog.FileName, ref content);
                    if(!openRes)
                    {
                        this.ShowMessage(string.Format("The file loading operation failed.\n{0}", ofdialog.FileName));
                        return;
                    }
                    this.rtbx.Text = content;
                    this.ReparseData();
                }
            }
        }

        private void RboVertical_CheckedChanged(object sender, EventArgs e)
        {
            if(this.rboVertical.Checked)
            {
                this.splitContainer1.Orientation = Orientation.Horizontal;
            }
        }
        private void RboHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rboHorizontal.Checked)
            {
                this.splitContainer1.Orientation = Orientation.Vertical;
            }
        }
        private void CboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = this.cboLang.SelectedIndex;
            if(selected >= 0 && this.lang_mapping != null)
            {
                var selected_text = this.cboLang.Items[selected].ToString();
                if(this.lang_mapping.ContainsKey(selected_text))
                {
                    var lang_entry = TreeSitterLanguage.createLanguage(this.lang_mapping[selected_text]);
                    if(lang_entry == null)
                    {
                        this.ShowMessage(string.Format("Load Language failed, \n{0}", this.lang_mapping[selected_text]));
                        return;
                    }
                    if(lang_entry.lang == null)
                    {
                        lang_entry.lang = new TSLanguage(lang_entry.new_fn());
                    }
                    this.lang = lang_entry;
                    this.tsslblLang.Text = selected_text;
                    this.tsslblLangVer.Text = TreeSitterLanguage.getLanguageVersion(lang_entry);
                }
            }
        }
        private void TsbtnCollapseAll_Click(object sender, EventArgs e)
        {
            this.tv.CollapseAll();
        }

        private void TsbtnExpandAll_Click(object sender, EventArgs e)
        {
            this.tv.ExpandAll();
        }

        private void TsbtnCollapse_Click(object sender, EventArgs e)
        {
            if(this.tv.SelectedNode != null)
            {
                this.tv.SelectedNode.Collapse(false);
            }
        }
        private void TsbtnExpand_Click(object sender, EventArgs e)
        {
            if(this.tv.SelectedNode != null)
            {
                this.tv.SelectedNode.ExpandAll();
            }
        }
        private void TsbtnPaste_Click(object sender, EventArgs e)
        {
            this.tsbtnNewData.PerformClick();
            this.rtbx.Text = Clipboard.GetText();
            this.tsbtnParse.PerformClick();
        }

        private void TsbtnAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog(this);
            }
        }
        private void TsbtnNewData_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(this.rtbx.Text))
            {
                if(this.ShowMessage("Are you sure you want to erase the data? \nSelect \"Yes\" to continue emptying, select \"No\" to return not emptying", MessageBoxButtons.YesNo) 
                    != DialogResult.Yes)
                {
                    return;
                }
            }
            this.ClearFormData();
        }
        private void TsbtnParse_Click(object sender, EventArgs e)
        {
            this.ReparseData();
        }
        private void bindingTree(string content, TSNode node, TreeNodeCollection collection, HashSet<string> types)
        {
            for (uint i = 0; i < node.child_count(); i++)
            {
                var sub = node.child(i);

                var new_range = sub.buildPointRange(content);
                var range_str = new_range.formatString();
                var fullname = sub.formatString(i, range_str);

                types.Add(sub.type());

                TreeNode newNode = new TreeNode(fullname);
                newNode.Tag = new_range;
                collection.Add(newNode);

                this.bindingTree(content, sub, newNode.Nodes, types);
            }
        }
        private void CboNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboNodes.SelectedIndex >= 0)
            {
                if (this.tv.SelectedNode != null)
                {
                    this.tv.SelectedNode.ForeColor = Color.Black;
                }
                this.matchedNodes.Clear();
                this.matchedNodePos = 0;
                this.resetQueryMatchedNodes();
                if(this.matchedNodes.Count > 0)
                {
                    this.matchedNodePos = 1;
                }
                this.resetMatchedNodePos(this.matchedNodePos);
            }
        }
        private void BtnPrevNode_Click(object sender, EventArgs e)
        {
            if (this.cboNodes.SelectedIndex >= 0)
            {
                if (this.matchedNodePos <= 1)
                {
                    string selected = this.cboNodes.Items[this.cboNodes.SelectedIndex].ToString();
                    this.ShowMessage(string.Format("The query is already in the FIRST match location, \r\n{0}.", selected));
                    this.tv.Focus();
                    return;
                }
                if (this.tv.SelectedNode != null)
                {
                    this.tv.SelectedNode.ForeColor = Color.Black;
                }
                this.matchedNodePos--;
                this.resetMatchedNodePos(this.matchedNodePos);
            }
        }
        private void BtnNextNode_Click(object sender, EventArgs e)
        {
            if (this.cboNodes.SelectedIndex >= 0)
            {
                if(this.matchedNodePos >= this.matchedNodes.Count)
                {
                    string selected = this.cboNodes.Items[this.cboNodes.SelectedIndex].ToString();
                    this.ShowMessage(string.Format("The query is already in the LAST match location, \r\n{0}.", selected));
                    this.tv.Focus();
                    return;
                }
                if (this.tv.SelectedNode != null)
                {
                    this.tv.SelectedNode.ForeColor = Color.Black;
                }
                this.matchedNodePos++;
                this.resetMatchedNodePos(this.matchedNodePos);
            }
        }
        private void resetQueryMatchedNodes()
        {
            string selected = this.cboNodes.Items[this.cboNodes.SelectedIndex].ToString();
            var matchWord = string.Format(NODE_MATCH_PAT, selected);
            Regex matchReg = new Regex(matchWord);
            this.queryMatchedNodes(matchReg, this.tv.Nodes, this.matchedNodes);
        }
        private void queryMatchedNodes(Regex regex, TreeNodeCollection nodes, List<TreeNode> result)
        {
            foreach(TreeNode node in nodes)
            {
                if(regex.Match(node.Text).Success)
                {
                    result.Add(node);
                }
                if(node.Nodes.Count > 0)
                {
                    this.queryMatchedNodes(regex, node.Nodes, result);
                }
            }
        }
        private void resetMatchedNodePos(int pos)
        {
            if(pos <= 0)
            {
                pos = 1;
            }
            int total = this.matchedNodes.Count;
            if(pos > total)
            {
                string selected = this.cboNodes.Items[this.cboNodes.SelectedIndex].ToString();
                this.ShowMessage(string.Format("No matching data found, {0}.", selected));
                return;
            }
            this.tv.SelectedNode = this.matchedNodes[pos - 1];
            this.tv.SelectedNode.ExpandAll();
            this.tv.SelectedNode.EnsureVisible();
            this.tv.Focus();

            this.lblNodeTotal.Text = total.ToString();
            this.lblNodePos.Text = pos.ToString();
        }
        private void BtnPrevExpr_Click(object sender, EventArgs e)
        {
            if (this.cboExprs.SelectedIndex >= 0)
            {
                if (this.matchedExprPos <= 1)
                {
                    string selected = this.cboExprs.Items[this.cboExprs.SelectedIndex].ToString();
                    this.ShowMessage(string.Format("The query is already in the FIRST match location, \r\n{0}.", selected));
                    this.rtbxExpr.Focus();
                    return;
                }
                this.rtbxExpr.ResetSelected();
                this.matchedExprPos--;
                this.resetMatchedExprPos(this.matchedExprPos);
            }
        }
        private void BtnNextExpr_Click(object sender, EventArgs e)
        {
            if (this.cboExprs.SelectedIndex >= 0)
            {
                if (this.matchedExprPos >= this.matchedExprs.Count)
                {
                    string selected = this.cboExprs.Items[this.cboExprs.SelectedIndex].ToString();
                    this.ShowMessage(string.Format("The query is already in the LAST match location, \r\n{0}.", selected));
                    this.rtbxExpr.Focus();
                    return;
                }
                this.rtbxExpr.ResetSelected();
                this.matchedExprPos++;
                this.resetMatchedExprPos(this.matchedExprPos);
            }
        }
        private void CboExprs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboExprs.SelectedIndex >= 0)
            {
                this.rtbxExpr.ResetSelected();
                this.matchedExprs.Clear();
                this.matchedExprPos = 0;
                this.resetQueryMatchedExprs();

                if(this.matchedExprs.Count > 0)
                {
                    this.matchedExprPos = 1;
                }
                this.resetMatchedExprPos(this.matchedExprPos);
            }
        }
        private void resetQueryMatchedExprs()
        {
            string selected = this.cboExprs.Items[this.cboExprs.SelectedIndex].ToString();
            var matchWord = string.Format(EXPR_MATCH_PAT, selected);
            Regex matchReg = new Regex(matchWord);

            var matchedrows = this.rtbxExpr.MatchValues(matchReg);
            foreach(KeyValuePair<int, int> item in matchedrows)
            {
                this.matchedExprs.Add(new MatchedPoint { start = item.Key, length = item.Value });
            }
        }

        private void resetMatchedExprPos(int pos)
        {
            if (pos <= 0)
            {
                pos = 1;
            }
            int total = this.matchedExprs.Count;
            if (pos > total)
            {
                string selected = this.cboExprs.Items[this.cboExprs.SelectedIndex].ToString();
                this.ShowMessage(string.Format("No matching data found, {0}.", selected));
                return;
            }
            var selectedPt = this.matchedExprs[pos - 1];
            this.rtbxExpr.UpdateSelected(selectedPt.start, selectedPt.length);
            this.rtbxExpr.Focus();

            this.lblExprTotal.Text = total.ToString();
            this.lblExprPos.Text = pos.ToString();
        }

        private void TsbtnCopyTree_Click(object sender, EventArgs e)
        {
            if (this.tv.Nodes.Count == 0)
            {
                this.ShowMessage("No data to copy");
                return;
            }
            StringBuilder builder = new StringBuilder();
            this.countTreeData(builder, this.tv.Nodes);
            string full_data = builder.ToString();
            Clipboard.SetText(full_data);

            this.ShowMessage("The Tree data has been copied to the clipboard");
        }
        private void buildIdent(StringBuilder builder, int level)
        {
            const string ONE_IDENT = "    ";
            for(int i=0;i<level;i++)
            {
                builder.Append(ONE_IDENT);
            }
        }
        private void countTreeData(StringBuilder builder, TreeNodeCollection collection)
        {
            foreach(TreeNode node in collection)
            {
                this.buildIdent(builder, node.Level);
                builder.Append(node.Text);
                builder.AppendLine();
                //插入子节点
                this.countTreeData(builder, node.Nodes);
            }
        }
        private void TsbtnCopySExpr_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(this.rtbxExpr.Text))
            {
                this.ShowMessage("No data to copy");
                return;
            }
            Clipboard.SetText(this.rtbxExpr.Text);

            this.ShowMessage("The S-Expression data has been copied to the clipboard");
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            Debug.Assert(this.lang.lang != null, "Query Language is null");
            Debug.Assert(this.root_tree != null, "Query Tree is null.");
            if (string.IsNullOrWhiteSpace(this.tbxQuery.Text))
            {
                this.ShowMessage("Query input textbox is empty.");
                return;
            }
            this.tvQueryResult.Nodes.Clear();
            this.lblCount.Text = "0";

            string query_ctx = this.tbxQuery.Text.Trim();
            uint error_offset = 0;
            TSQueryError error_type = TSQueryError.TSQueryErrorNone;
            var tsquery = this.lang.lang.query_new(query_ctx, out error_offset, out error_type);
            if (error_type != TSQueryError.TSQueryErrorNone)
            {
                this.ShowMessage(string.Format("Query failed. error position: {0}, type: {1}", error_offset, error_type.ToString()));
                return;
            }
            var tsquerycursor = new TSQueryCursor();
            tsquerycursor.exec(tsquery, this.root_tree.root_node());

            string conent = this.rtbx.Text;
            uint count = 0;
            TSQueryMatch matchcd = new TSQueryMatch();
            TSQueryCapture[] captures = null;
            while (tsquerycursor.next_match(out matchcd, out captures))
            {
                this.bindingMatched(count, matchcd, captures, conent);
                count++;
            }
            this.tvQueryResult.ExpandAll();
            this.lblCount.Text = count.ToString();
            if(count == 0)
            {
                this.ShowMessage("No matching data was found.");
            }
        }
        private void bindingMatched(uint pos, TSQueryMatch matched, TSQueryCapture[] captures, string content)
        {
            if (captures != null)
            {
                var currnode = this.tvQueryResult.Nodes.Add(string.Format("[{0}]", pos));
                foreach (var sub in captures)
                {
                    var new_range = sub.node.buildPointRange(content);
                    var range_str = new_range.formatString();
                    var ppos = sub.node.findPositionByParent();
                    var fullname = sub.node.formatString(ppos, range_str);

                    TreeNode newNode = new TreeNode(fullname);
                    newNode.Tag = new_range;
                    currnode.Nodes.Add(newNode);

                    this.bindingTree(sub.node, newNode.Nodes, content);
                }
            }
        }

        private void bindingTree(TSNode node, TreeNodeCollection collection, string content)
        {
            for (uint i = 0; i < node.child_count(); i++)
            {
                var sub = node.child(i);
                var new_range = sub.buildPointRange(content);
                var range_str = new_range.formatString();
                var fullname = sub.formatString(i, range_str);

                TreeNode subnewNode = new TreeNode(fullname);
                subnewNode.Tag = new_range;
                collection.Add(subnewNode);

                this.bindingTree(sub, subnewNode.Nodes, content);
            }
        }
        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag == null)
            {
                return;
            }
            this.rtbx.ResetSelected();

            PointRange range_data = (PointRange)e.Node.Tag;
            this.resetTreeViewAfterCheck(range_data);
        }

        private void TvQueryResult_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                return;
            }
            this.rtbx.ResetSelected();

            PointRange range_data = (PointRange)e.Node.Tag;
            this.resetTreeViewAfterCheck(range_data);
        }

        private void resetTreeViewAfterCheck(PointRange range_data)
        {
            int total_start_pos = 0;
            for (int i = 0; i < this.srcRowLengths.Count; i++)
            {
                var curr = this.srcRowLengths[i];
                if (i == range_data.start_pt.row)
                {
                    int curr_length = 0;
                    if (range_data.end_pt.row == range_data.start_pt.row)
                    {
                        curr_length = Convert.ToInt32(range_data.end_pt.column - range_data.start_pt.column);
                    }
                    else
                    {
                        curr_length = curr.Value - Convert.ToInt32(range_data.start_pt.column);
                    }
                    this.rtbx.UpdateSelected(total_start_pos + Convert.ToInt32(range_data.start_pt.column), curr_length);
                }
                else if (i > range_data.start_pt.row && i < range_data.end_pt.row)
                {
                    this.rtbx.UpdateSelected(total_start_pos, curr.Value);
                }
                else if (i == range_data.end_pt.row)
                {
                    this.rtbx.UpdateSelected(total_start_pos, Convert.ToInt32(range_data.end_pt.column));
                }
                else if (i > range_data.end_pt.row)
                {
                    break;
                }
                total_start_pos += curr.Value;
            }
        }

        private void ClearFormData()
        {
            this.rtbx.Clear();
            this.tv.Nodes.Clear();
            this.rtbxExpr.Clear();
            this.cboNodes.Items.Clear();
            this.cboExprs.Items.Clear();
            this.srcRowLengths.Clear();
            this.lblExprPos.Text = this.lblExprTotal.Text = "0";
            this.lblNodePos.Text = this.lblNodeTotal.Text = "0";
        }
        private void ReparseData()
        {
            if (string.IsNullOrWhiteSpace(this.rtbx.Text))
            {
                this.ShowMessage("There is no content for parsing.");
                return;
            }
            if (this.lang == null)
            {
                this.ShowMessage("Please select the language to be parsed first.");
                return;
            }
            this.tv.Nodes.Clear();
            this.cboNodes.Items.Clear();
            this.matchedNodes.Clear();
            this.matchedExprs.Clear();
            this.matchedNodePos = 0;
            this.matchedExprPos = 0;
            this.lblExprPos.Text = this.lblExprTotal.Text = "0";
            this.lblNodePos.Text = this.lblNodeTotal.Text = "0";

            this.rtbx.ResetSelected();
            this.srcRowLengths = this.rtbx.ToRows();

            TSTree tree = null;
            using (var parser = new TSParser())
            {
                parser.set_language(this.lang.lang);
                tree = parser.parse_string(null, this.rtbx.Text);
                if (tree == null)
                {
                    this.ShowMessage("Parse failed.");
                    return;
                }
                this.root_tree = tree;
            }
            HashSet<string> types = new HashSet<string>();
            this.bindingTree(this.rtbx.Text, tree.root_node(), this.tv.Nodes, types);
            this.tv.ExpandAll();
            //打印sexp
            var sexp = tree.root_node().to_string();
            var sexp_fmt = new SExpr(TreeSitterLanguage.SEXPR_IDENT);
            var sexp_str = sexp_fmt.format(sexp);
            this.rtbxExpr.Text = sexp_str;
            //绑定到下拉列表
            List<string> sorts = types.ToList();
            sorts.Sort();
            List<string> resorts = new List<string>();
            foreach (var item in sorts)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                char firstch = item[0];
                if ((firstch >= 'A' && firstch <= 'Z') || (firstch >= 'a' && firstch <= 'z'))
                {
                    resorts.Add(item);
                }
            }
            foreach (var item in sorts)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                char firstch = item[0];
                if (!(firstch >= 'A' && firstch <= 'Z') && !(firstch >= 'a' && firstch <= 'z'))
                {
                    resorts.Add(item);
                }
            }
            this.cboNodes.RebindItems(resorts);
            this.cboExprs.RebindItems(resorts);
        }

        private class MatchedPoint
        {
            public int start;
            public int length;
        }
    }
}
