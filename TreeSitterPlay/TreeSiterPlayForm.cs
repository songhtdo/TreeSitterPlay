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
using GitHub.TreeSitter;

namespace TreeSitterPlay
{
    public partial class TreeSiterPlayForm : Form
    {
        // (struct_specifier 或(qualified_identifier 
        private const string ROW_MATCH_PAT = @"^({0})\s|\s({0})\s|\s({0})$|^({0})$";
        private KeyMessageFilter keyFilter = null;
        private Dictionary<string, string> lang_mapping = null;
        private List<KeyValuePair<int, int>> srcRowLengths = new List<KeyValuePair<int, int>>();
        private LanguageEntry lang = null;
        private TreeNode matchNode = null;

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
            this.btnNextNodes.Click += BtnNextNodes_Click;
            this.tsbtnExpand.Click += TsbtnExpand_Click;
            this.tsbtnCollapse.Click += TsbtnCollapse_Click;
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
                this.tv.SelectedNode.Expand();
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
            this.rboxExpr.Clear();
            this.cboNodes.Items.Clear();
            this.srcRowLengths.Clear();
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
            this.tv.Nodes.Clear();
            this.cboNodes.Items.Clear();
            this.matchNode = null;
            HashSet<string> types = new HashSet<string>();
            this.bindingTree(raw_lang, this.rtbx.Text, tree.root_node(), this.tv.Nodes, types);
            this.tv.ExpandAll();
            //打印sexp
            var sexp = tree.root_node().to_string();
            var sexp_fmt = new SExpr(TreeSitterLanguage.SEXPR_IDENT);
            var sexp_str = sexp_fmt.format(sexp);
            this.rboxExpr.Text = sexp_str;
            //绑定到下拉列表
            List<string> sorts = types.ToList();
            sorts.Sort();
            foreach(var item in sorts)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                char firstch = item[0];
                if((firstch >= 'A' && firstch <= 'Z') || (firstch >= 'a' && firstch <= 'z'))
                {
                    this.cboNodes.Items.Add(item);
                }
            }
            foreach (var item in sorts)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                char firstch = item[0];
                if (!(firstch >= 'A' && firstch <= 'Z') && !(firstch >= 'a' && firstch <= 'z'))
                {
                    this.cboNodes.Items.Add(item);
                }
            }
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
                this.matchNode = null;
                this.resetQueryMatchNode(this.matchNode);
            }
        }
        private void BtnNextNodes_Click(object sender, EventArgs e)
        {
            if (this.cboNodes.SelectedIndex >= 0)
            {
                if (this.tv.SelectedNode != null)
                {
                    this.tv.SelectedNode.ForeColor = Color.Black;
                }
                this.resetQueryMatchNode(this.matchNode);
            }
        }
        private void resetQueryMatchNode(TreeNode prevNode)
        {
            string selected = this.cboNodes.Items[this.cboNodes.SelectedIndex].ToString();
            var matchWord = string.Format(ROW_MATCH_PAT, selected);
            Regex matchReg = new Regex(matchWord);

            TreeNode firstNode = (prevNode == null) ? this.tv.Nodes[0] : prevNode;
            if(this.matchNode != firstNode && matchReg.Match(firstNode.Text).Success)
            {
                this.matchNode = firstNode;
            }
            else if(!this.querySelectedNode(firstNode, matchReg, ref this.matchNode))
            {
                MessageBox.Show(string.Format("No matching data found, {0}.", selected), "TreeSitterPlay",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.tv.SelectedNode = this.matchNode;
            //this.tv.SelectedNode.ForeColor = Color.Red;
            this.tv.Focus();
        }

        private bool querySelectedNode(TreeNode node, Regex regex, ref TreeNode matched)
        {
            foreach(TreeNode sub in node.Nodes)
            {
                if(this.queryMatchedNode(sub, regex, ref matched))
                {
                    return true;
                }
            }
            return this.queryNextNode(node, regex, ref matched);
        }
        private bool queryMatchedNode(TreeNode node, Regex regex, ref TreeNode matched)
        {
            if ((this.matchNode != node) && regex.Match(node.Text).Success)
            {
                matched = node;
                return true;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                if (this.queryMatchedNode(subnode, regex, ref matched))
                {
                    return true;
                }
            }
            return false;
        }
        private bool queryNextNode(TreeNode node, Regex regex, ref TreeNode matched)
        {
            // Traverse next data
            var nextNode = node.NextNode;
            while (nextNode != null)
            {
                if(this.queryMatchedNode(nextNode, regex, ref matched))
                {
                    return true;
                }
                nextNode = nextNode.NextNode;
            }
            // Traverse parent next data
            var curr = node.Parent;
            while(curr != null)
            {
                if(curr.NextNode != null)
                {
                    break;
                }
                curr = curr.Parent;
            }
            if(curr != null && curr.NextNode != null)
            {
                if(this.queryMatchedNode(curr.NextNode, regex, ref matched))
                {
                    return true;
                }
                if(this.queryNextNode(curr.NextNode, regex, ref matched))
                {
                    return true;
                }
            }
            return false;
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
            if(string.IsNullOrWhiteSpace(this.rboxExpr.Text))
            {
                MessageBox.Show("No data to copy", "TreeSitterPlay", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Clipboard.SetText(this.rboxExpr.Text);

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
    }
}
