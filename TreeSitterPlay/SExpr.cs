using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreeSitterPlay
{
    class SExpr
    {
        // (struct_specifier 或(qualified_identifier 
        private const string NODE_START_PAT = @"\([A-Za-z_][A-Za-z0-9_]*(?!\))$";
        // (field_identifier))) 或 (identifier)))))))
        private const string NODE_END_PAT = @"\([A-Za-z_][A-Za-z0-9_]*\)*$";
        // declarator: 或 scope:
        private const string NODE_FIELD_PAT = @"[A-Za-z_][A-Za-z0-9_]*\:$";
        // 单括号
        private const string RIGHT_BRACKET_PAT = @"^\)$";
        public SExpr(int numDown)
        {
            this.NumDown = numDown;
        }
        public int NumDown { get; set; }

        private string getIdent()
        {
            string ident = "";
            for (int i = 0; i < this.NumDown; i++)
            {
                ident += " ";
            }
            return ident;
        }
        private string[] split(string src)
        {
            string step1 = Regex.Replace(src, @"\r", " ");
            string step2 = Regex.Replace(step1, @"\n", " ");
            string step3 = Regex.Replace(step2, @"\s{2,}", " ");
            string step4 = step3;
            while(true)
            {
                if(!step4.Contains(") )"))
                    break;
                step4 = step4.Replace(") )", "))");
            }
            var result = step4.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }
        private void appendNewLine(StringBuilder builder)
        {
            builder.AppendLine();
        }
        private void appendNewSpace(StringBuilder builder)
        {
            builder.Append(' ');
        }
        private void appendValue(StringBuilder builder, string val)
        {
            builder.Append(val);
        }
        private void appendIdent(StringBuilder builder, int level)
        {
            string ident = this.getIdent();
            for (int i = 0; i < level; i++)
            {
                builder.Append(ident);
            }
        }
        public string format(string src)
        {
            Regex nodeStartReg = new Regex(NODE_START_PAT);
            Regex nodeEndReg = new Regex(NODE_END_PAT);
            Regex nodeFieldReg = new Regex(NODE_FIELD_PAT);
            Regex rightBracketReg = new Regex(RIGHT_BRACKET_PAT);

            var newSrc = src.Trim();
            var newSrcList = this.split(newSrc);
            StringBuilder builder = new StringBuilder();
            int level = 0;
            int pos = 0;
            bool print_ident = true;
            while (pos < newSrcList.Length)
            {
                var curr = newSrcList[pos++];
                if (print_ident)
                {
                    this.appendIdent(builder, level);
                }
                //如果匹配开始节点，要缩进到下一个节点
                if (nodeStartReg.Match(curr).Success)
                {
                    this.appendValue(builder, curr);
                    this.appendNewLine(builder);
                    print_ident = true;
                    level++;
                    continue;
                }
                else if (nodeEndReg.Match(curr).Success)
                {
                    this.appendValue(builder, curr);
                    this.appendNewLine(builder);

                    var rightFields = curr.Split(')');
                    int sub_pos = 0, total = rightFields.Length - 1;
                    while (sub_pos < total)
                    {
                        if (sub_pos < (total - 1))
                        {
                            level--;
                        }
                        sub_pos++;
                    }
                    print_ident = true;
                    continue;
                }
                else if (rightBracketReg.Match(curr).Success)
                {
                    this.appendValue(builder, curr);
                    this.appendNewLine(builder);
                    if (pos < (newSrcList.Length - 1))
                    {
                        var next = newSrcList[pos + 1];
                        //如果是)，则缩进
                        if (rightBracketReg.Match(next).Success)
                        {
                            level--;
                        }
                    }
                    print_ident = true;
                }
                //如果是字段，打印值。不增加缩进位
                else if (nodeFieldReg.Match(curr).Success)
                {
                    this.appendValue(builder, curr);
                    this.appendNewSpace(builder);
                    print_ident = false;
                    continue;
                }
            }
            return builder.ToString();
        }
    }
}
