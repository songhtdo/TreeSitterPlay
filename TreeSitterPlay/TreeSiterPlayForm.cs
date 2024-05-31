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
            this.tsbtnClear.Click += TsbtnClear_Click;
            this.tsbtnAbout.Click += TsbtnAbout_Click;
            this.tsbtnHelp.Click += TsbtnHelp_Click;
            this.tsbtnExit.Click += TsbtnExit_Click;
            this.rboHorizontal.CheckedChanged += RboHorizontal_CheckedChanged;
            this.rboVertical.CheckedChanged += RboVertical_CheckedChanged;
            this.cboLang.SelectedIndexChanged += CboLang_SelectedIndexChanged;
            this.tv.AfterSelect += Tv_AfterSelect;
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
        }


        ~TreeSiterPlayForm()
        {
            this.keyFilter.UnRegisterAll();
            if(this.lang != null)
            {
                TreeSitterLanguage.releaseLanguage(this.lang);
            }
        }
        private void TreeSiterPlayForm_Load(object sender, EventArgs e)
        {
            //注册事件
            this.keyFilter = new KeyMessageFilter(this.Handle);
            this.keyFilter.RegisterHotkey(KeyModifiers.Alt, Keys.S, () =>
            {
                this.tsbtnPaste.PerformClick();
            });
            this.keyFilter.RegisterHotkey(KeyModifiers.Alt, Keys.V, () =>
            {
                this.tsbtnClear.PerformClick();
                this.rtbx.Text = Clipboard.GetText();
            });
            this.keyFilter.RegisterHotkey(KeyModifiers.Ctrl, Keys.Enter, () =>
            {
                this.tsbtnParse.PerformClick();
            });
            this.keyFilter.RegisterHotkey(KeyModifiers.Alt, Keys.P, () =>
            {
                this.tsbtnParse.PerformClick();
            });
            this.keyFilter.RegisterHotkey(KeyModifiers.Alt, Keys.E, () =>
            {
                this.tsbtnExpandAll.PerformClick();
            });
            this.keyFilter.RegisterHotkey(KeyModifiers.Alt, Keys.G, () => {
                this.tsbtnCollapseAll.PerformClick();
            });
            this.keyFilter.RegisterHotkey(KeyModifiers.Alt, Keys.N, () => {
                this.tsbtnClear.PerformClick();
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
                        MessageBox.Show(string.Format("Load Language failed, \n{0}", this.lang_mapping[selected_text]),
                            "TreeSitterPlay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    this.lang = lang_entry;
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
            this.tsbtnClear.PerformClick();
            this.rtbx.Text = Clipboard.GetText();
            this.tsbtnParse.PerformClick();
        }
        private void TsbtnHelp_Click(object sender, EventArgs e)
        {
            using (HelpForm form = new HelpForm())
            {
                form.ShowDialog(this);
            }
        }

        private void TsbtnAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog(this);
            }
        }
        private void TsbtnClear_Click(object sender, EventArgs e)
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
        private void TsbtnParse_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(this.rtbx.Text))
            {
                MessageBox.Show("There is no content for parsing.", "TreeSitterPlay", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(this.lang == null)
            {
                MessageBox.Show("Please select the language to be parsed first.", "TreeSitterPlay",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            var parser = new TSParser();
            TSLanguage raw_lang = new TSLanguage(lang.new_fn());
            parser.set_language(raw_lang);

            var tree = parser.parse_string(null, this.rtbx.Text);
            if (tree == null)
            {
                MessageBox.Show("Parse failed.", "TreeSitterPlay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HashSet<string> types = new HashSet<string>();
            this.bindingTree(raw_lang, this.rtbx.Text, tree.root_node(), this.tv.Nodes, types);
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
        private string formatPoint(PointRange pt_range)
        {
            return string.Format("[[{0},{1}],[{2},{3}]]",
                   pt_range.start_pt.row, pt_range.start_pt.column, pt_range.end_pt.row, pt_range.end_pt.column);
        }
        private void bindingTree(TSLanguage raw_lang, string conent, TSNode node, TreeNodeCollection collection, HashSet<string> types)
        {
            for (uint i = 0; i < node.child_count(); i++)
            {
                var sub = node.child(i);
                var new_range = new PointRange { start_pt = sub.start_point(), end_pt = sub.end_point(), text = sub.text(conent) };
                var range_str = this.formatPoint(new_range);
                string field_name = node.field_name_for_child(i);
                string fullname = string.Empty;
                string type = sub.type();
                if (!string.IsNullOrWhiteSpace(field_name))
                {
                    fullname = string.Format("{0} : {1} {2}", field_name, type, range_str);
                }
                else
                {
                    fullname = string.Format("{0} {1}", type, range_str);
                }
                types.Add(type);

                TreeNode newNode = new TreeNode(fullname);
                newNode.Tag = new_range;
                collection.Add(newNode);

                this.bindingTree(raw_lang, conent, sub, newNode.Nodes, types);
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
                    MessageBox.Show(string.Format("The query is already in the FIRST match location, \r\n{0}.", selected), "TreeSitterPlay",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(string.Format("The query is already in the LAST match location, \r\n{0}.", selected), "TreeSitterPlay",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(string.Format("No matching data found, {0}.", selected), "TreeSitterPlay",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(string.Format("The query is already in the FIRST match location, \r\n{0}.", selected), "TreeSitterPlay",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(string.Format("The query is already in the LAST match location, \r\n{0}.", selected), "TreeSitterPlay",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(string.Format("No matching data found, {0}.", selected), "TreeSitterPlay",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("No data to copy", "TreeSitterPlay",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StringBuilder builder = new StringBuilder();
            this.countTreeData(builder, this.tv.Nodes);
            string full_data = builder.ToString();
            Clipboard.SetText(full_data);

            MessageBox.Show("The Tree data has been copied to the clipboard", "TreeSitterPlay",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("No data to copy", "TreeSitterPlay", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Clipboard.SetText(this.rtbxExpr.Text);

            MessageBox.Show("The S-Expression data has been copied to the clipboard", "TreeSitterPlay",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag == null)
            {
                return;
            }
            this.rtbx.ResetSelected();

            PointRange range_data = (PointRange)e.Node.Tag;
            int total_start_pos = 0;
            for(int i=0;i<this.srcRowLengths.Count;i++)
            {
                var curr = this.srcRowLengths[i];
                if(i == range_data.start_pt.row)
                {
                    int curr_length = 0;
                    if(range_data.end_pt.row == range_data.start_pt.row)
                    {
                        curr_length = Convert.ToInt32(range_data.end_pt.column - range_data.start_pt.column);
                    }
                    else
                    {
                        curr_length = curr.Value - Convert.ToInt32(range_data.start_pt.column);
                    }
                    this.rtbx.UpdateSelected(total_start_pos + Convert.ToInt32(range_data.start_pt.column), curr_length);
                }
                else if(i > range_data.start_pt.row && i < range_data.end_pt.row)
                {
                    this.rtbx.UpdateSelected(total_start_pos, curr.Value);
                }
                else if(i == range_data.end_pt.row)
                {
                    this.rtbx.UpdateSelected(total_start_pos, Convert.ToInt32(range_data.end_pt.column));
                }
                else if(i > range_data.end_pt.row)
                {
                    break;
                }
                total_start_pos += curr.Value;
            }
        }

        private class MatchedPoint
        {
            public int start;
            public int length;
        }
    }
}
