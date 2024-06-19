using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TreeSitterPlay
{
    public static class NativeLibrary
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public static string GetLibraryPathname(string filename)
        {
            // If 64-bit process, load 64-bit DLL
            bool is64bit = System.Environment.Is64BitProcess;

            string prefix = TreeSitterLanguage.LANG_DIR;
            var lib1 = prefix + @"\" + filename;
            return lib1;
        }
        [DllImport("user32.dll")]
        public static extern UInt32 RegisterHotKey(IntPtr hWnd, UInt32 id, UInt32 fsModifiers, UInt32 vk);

        [DllImport("user32.dll")]
        public static extern UInt32 UnregisterHotKey(IntPtr hWnd, UInt32 id);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(String lpString);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);
        // MessageId
        public const int WM_HOTKEY_MSG_ID = 0x0312;
        // KeyDown id
        public const int WM_KEYDOWN = 0x100;
    }
}
