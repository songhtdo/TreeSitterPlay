using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeSitterPlay
{
    static class ControlExtensions
    {
        public static List<KeyValuePair<int, int>> ToRows(this RichTextBox rtbx)
        {
            const string NEWLINE_PAT = @"([\r\n])";
            Regex newlineReg = new Regex(NEWLINE_PAT);

            List<KeyValuePair<int, int>> rows = new List<KeyValuePair<int, int>>();
            var matchedRes = newlineReg.Matches(rtbx.Text);
            if (matchedRes.Count > 0)
            {
                int startIndex = 0;
                foreach (Match match in matchedRes)
                {
                    int endIndex = match.Index;
                    rows.Add(new KeyValuePair<int, int>(startIndex, endIndex-startIndex+match.Length));
                    startIndex = match.Index + match.Length;
                }
                // Add last row (from last newline to end of text)
                rows.Add(new KeyValuePair<int, int>(startIndex, rtbx.Text.Length-startIndex));
            }
            else
            {
                rows.Add(new KeyValuePair<int, int>(0, rtbx.Text.Length));
            }
            return rows;
        }
        public static List<KeyValuePair<int, int>> MatchValues(this RichTextBox rtbx, Regex regex)
        {
            List<KeyValuePair<int, int>> rows = new List<KeyValuePair<int, int>>();
            var matchedRes = regex.Matches(rtbx.Text);
            if (matchedRes.Count > 0)
            {
                foreach (Match match in matchedRes)
                {
                    rows.Add(new KeyValuePair<int, int>(match.Index, match.Length));
                }
            }
            return rows;
        }

        public static void ResetSelected(this RichTextBox rtbx)
        {
            rtbx.SelectionStart = 0;
            rtbx.SelectionLength = rtbx.TextLength;
            rtbx.SelectionColor = rtbx.ForeColor;
            rtbx.SelectionFont = rtbx.Font;
        }

        public static void UpdateSelected(this RichTextBox rtbx, int startpos, int length)
        {
            rtbx.SelectionStart = startpos;
            rtbx.SelectionLength = length;
            rtbx.SelectionColor = Color.Red;
            rtbx.SelectionFont = new Font(rtbx.Font, FontStyle.Bold);
        }

        public static void RebindItems(this ComboBox cbo, IEnumerable<string> data)
        {
            cbo.Items.Clear();
            foreach(var item in data)
            {
                cbo.Items.Add(item);
            }
        }
    }
}
