using GitHub.TreeSitter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSitterPlay
{
    static class TreeSitterExtensions
    {
        public static uint findPositionByParent(this TSNode node)
        {
            if (!node.parent().is_null())
            {
                for (uint p = 0; p < node.parent().child_count(); p++)
                {
                    var pchild = node.parent().child(p);
                    if (pchild.eq(node))
                    {
                        return p;
                    }
                }
            }
            return 0;
        }
        public static PointRange buildPointRange(this TSNode sub, string content)
        {
            var new_range = new PointRange
            {
                start_pt = sub.start_point(),
                end_pt = sub.end_point(),
                text = (string.IsNullOrWhiteSpace(content) ? string.Empty : sub.text(content))
            };
            return new_range;
        }
        public static string formatString(this PointRange pt_range)
        {
            return string.Format("[[{0},{1}],[{2},{3}]]",
                pt_range.start_pt.row, pt_range.start_pt.column, pt_range.end_pt.row, pt_range.end_pt.column);
        }

        public static string formatString(this TSNode sub, uint pos, string range_str)
        {
            string field_name = sub.parent().field_name_for_child(pos);
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
            return fullname;
        }
    }
}
