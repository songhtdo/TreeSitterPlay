using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeSitterPlay
{
    public class KeyMessageFilter : System.Windows.Forms.IMessageFilter, System.IDisposable
    {
        private Dictionary<uint, KeyAction> keyMapping = new Dictionary<uint, KeyAction>();
        private IntPtr hWnd;

        public KeyMessageFilter(IntPtr Wnd)
        {
            this.hWnd = Wnd;
        }
        public void StartFilter()
        {
            System.Windows.Forms.Application.AddMessageFilter(this);
        }
        public void StopFilter()
        {
            System.Windows.Forms.Application.RemoveMessageFilter(this);
        }

        public bool RegisterHotkey(KeyModifiers modifier, System.Windows.Forms.Keys key, System.Action action)
        {
            var code = this.calcHashcode(modifier, key);
            if(this.keyMapping.ContainsKey(code))
            {
                this.keyMapping[code].Action = action;
                return true;
            }
            UInt32 hotkeyid = NativeLibrary.GlobalAddAtom(System.Guid.NewGuid().ToString());
            KeyAction newAction = new KeyAction
            {
                Modifier = modifier,
                Key = key,
                Action = action,
                KeyId = hotkeyid
            };
            var res = NativeLibrary.RegisterHotKey(this.hWnd, hotkeyid, (uint)modifier, (uint)key);
            if(res > 0)
            {
                this.keyMapping.Add(code, newAction);
                return true;
            }
            NativeLibrary.GlobalDeleteAtom(hotkeyid);
            return false;
        }
        public void UnRegisterHotKey(KeyModifiers modifier, System.Windows.Forms.Keys key)
        {
            var code = this.calcHashcode(modifier, key);
            if (this.keyMapping.ContainsKey(code))
            {
                KeyAction action = this.keyMapping[code];
                NativeLibrary.GlobalDeleteAtom(action.KeyId);
                this.keyMapping.Remove(code);
            }
        }
        public void UnRegisterAll()
        {
            foreach(var keypair in this.keyMapping)
            {
                NativeLibrary.GlobalDeleteAtom(keypair.Value.KeyId);
            }
            this.keyMapping.Clear();
        }

        private uint calcHashcode(KeyModifiers modifier, System.Windows.Forms.Keys key)
        {
            uint code = (uint)modifier * 1000 + (uint)key;
            return code;
        }
        public void Dispose()
        {
            this.UnRegisterAll();
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == NativeLibrary.WM_HOTKEY_MSG_ID)
            {
                foreach(var keypair in this.keyMapping)
                {
                    if ((UInt32)m.WParam == keypair.Value.KeyId)
                    {
                        keypair.Value.Action();
                        return true;
                    }
                }
            }
            return false;
        }
        static class NativeLibrary
        {
            [DllImport("user32.dll")]
            public static extern UInt32 RegisterHotKey(IntPtr hWnd, UInt32 id, UInt32 fsModifiers, UInt32 vk);

            [DllImport("user32.dll")]
            public static extern UInt32 UnregisterHotKey(IntPtr hWnd, UInt32 id);

            [DllImport("kernel32.dll")]
            public static extern UInt32 GlobalAddAtom(String lpString);

            [DllImport("kernel32.dll")]
            public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);
            //MessageId
            public const int WM_HOTKEY_MSG_ID = 0x0312;
        }
        private class KeyAction
        {
            public KeyModifiers Modifier { get; set; }
            public Keys Key { get; set; }
            public Action Action { get; set; }
            public uint KeyId { get; set; }
        }
    }
    [Flags()]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8
    }
}
