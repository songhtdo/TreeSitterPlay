using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitHub.TreeSitter;

namespace TreeSitterPlay
{
    public partial class TreeSiterPlayForm : Form
    {
        KeyMessageFilter keyFilter = null;

        private Dictionary<string, string> lang_mapping = null;
        private LanguageEntry lang = null;
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
            this.tscbo.SelectedIndexChanged += Tscbo_SelectedIndexChanged;
            this.tscboLang.SelectedIndexChanged += TscboLang_SelectedIndexChanged;
            this.tv.AfterSelect += Tv_AfterSelect;
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
                this.tsbtnClear.PerformClick();
                this.rtbx.Text = Clipboard.GetText();
                this.tsbtnParse.PerformClick();
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
                    this.tscboLang.Items.Add(item.Key);
                }
                this.tscboLang.SelectedIndex = 0;
            }
            this.tscboLang.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tscbo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tscbo.SelectedIndex = 0;
            
        }
        private void TsbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Tscbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tscbo.SelectedIndex == 0)
            {
                this.splitContainer1.Orientation = Orientation.Horizontal;
            }
            else if (this.tscbo.SelectedIndex == 1)
            {
                this.splitContainer1.Orientation = Orientation.Vertical;
            }
        }
        private void TscboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = this.tscboLang.SelectedIndex;
            if(selected >= 0 && this.lang_mapping != null)
            {
                var selected_text = this.tscboLang.Items[selected].ToString();
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
            this.tbx.Clear();
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
            this.bindingTree(this.rtbx.Text, tree.root_node(), this.tv.Nodes);
            this.tv.ExpandAll();
            //打印sexp
            var sexp = tree.root_node().to_string();
            var sexp_fmt = new SExpr(TreeSitterLanguage.SEXPR_IDENT);
            var sexp_str = sexp_fmt.format(sexp);
            this.tbx.Text = sexp_str;
        }
        private void Tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag == null)
            {
                return;
            }
            PointRange range_data = (PointRange)e.Node.Tag;
            int start = 0, end = 0;
            uint row = 0;
            foreach (string line in this.rtbx.Lines)
            {
                if(row > range_data.end_pt.row)
                {
                    break;
                }
                if(row < range_data.start_pt.row)
                {
                    start += line.Length;
                }
                if(row < range_data.end_pt.row)
                {
                    end += line.Length;
                }
            }
            start += Convert.ToInt32(range_data.start_pt.column);
            end += Convert.ToInt32(range_data.end_pt.column);
            int total = end - start;
            this.SetRedAndBoldText(this.rtbx, range_data.text, start);
        }
        private string formatPoint(PointRange pt_range)
        {
            return string.Format("[[{0},{1}],[{2},{3}]]",
                   pt_range.start_pt.row, pt_range.start_pt.column, pt_range.end_pt.row, pt_range.end_pt.column);
        }

        private void bindingTree(string conent, TSNode node, TreeNodeCollection collection)
        {
            for(uint i=0;i<node.child_count();i++)
            {
                var sub = node.child(i);
                var new_range = new PointRange { start_pt = sub.start_point(), end_pt = sub.end_point(), text = sub.text(conent) };
                var range_str = this.formatPoint(new_range);
                var fullname = string.Format("{0} {1}", sub.type(), range_str);

                TreeNode newNode = new TreeNode(fullname);
                newNode.Tag = new_range;
                collection.Add(newNode);

                this.bindingTree(conent, sub, newNode.Nodes);
            }
        }
        private void SetRedAndBoldText(RichTextBox textbox, string text, int startIndex)
        {
            textbox.SelectionStart = 0;
            textbox.SelectionLength = textbox.TextLength;
            textbox.SelectionColor = textbox.ForeColor;
            textbox.SelectionFont = textbox.Font;

            textbox.SelectionStart = startIndex;
            textbox.SelectionLength = text.Length;
            textbox.SelectionColor = Color.Red;
            textbox.SelectionFont = new Font(textbox.Font, FontStyle.Bold);
        }
    }
}
