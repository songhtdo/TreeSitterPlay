using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TreeSitterPlay
{
    public static class FormExtensions {
        public static void ShowMessage(this Form form, string Message) {
           System.Windows.Forms.MessageBox.Show(Message, AppStrings.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowMessage(this Form form, string Message, params object[] args) {
            System.Windows.Forms.MessageBox.Show(string.Format(Message, args), AppStrings.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowMessage(this Form form, string Message, MessageBoxButtons boxButtons) {
            return System.Windows.Forms.MessageBox.Show(Message, AppStrings.APP_NAME, boxButtons, MessageBoxIcon.Information);
        }

        public static DialogResult ShowMessage(this Form form, string Message, MessageBoxButtons boxButtons, params object[] args) {
            return System.Windows.Forms.MessageBox.Show(string.Format(Message, args), AppStrings.APP_NAME, boxButtons, MessageBoxIcon.Information);
        }
    }
}
