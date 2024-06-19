using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeSitterPlay
{
    public class KeyMessageFilter : System.IDisposable
    {
        private Dictionary<Keys, Action> keyMapping = new Dictionary<Keys, Action>();
        private Form hWnd;

        public KeyMessageFilter(Form Wnd)
        {
            this.hWnd = Wnd;
        }
        public void StartFilter()
        {
            //this.hWnd.KeyPreview = true;
        }
        public void StopFilter()
        {
            //this.hWnd.KeyPreview = false;
        }

        public bool RegisterHotkey(System.Windows.Forms.Keys key, System.Action action)
        {
            if(this.keyMapping.ContainsKey(key))
            {
                this.keyMapping[key] = action;
                return true;
            }
            this.keyMapping.Add(key, action);
            return true;
        }
        public void UnRegisterHotKey(System.Windows.Forms.Keys key)
        {
            if (this.keyMapping.ContainsKey(key))
            {
                Action action = this.keyMapping[key];
                this.keyMapping.Remove(key);
            }
        }
        public void UnRegisterAll()
        {
            this.keyMapping.Clear();
        }
        public void Dispose()
        {
            this.UnRegisterAll();
        }

        public bool PreFilterMessage(ref Message msg, Keys keydata)
        {
            if(this.keyMapping.ContainsKey(keydata))
            {
                this.keyMapping[keydata]();
                return true;
            }
            return false;
        }
    }
}
