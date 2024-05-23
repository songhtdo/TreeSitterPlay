using GitHub.TreeSitter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreeSitterPlay
{
    static class TreeSitterLanguage
    {
        public static readonly int SEXPR_IDENT = 4;
        public static readonly string LANG_DIR = "lang";

        //[DllImport("tree-sitter-cpp.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr tree_sitter_cpp();

        public static Dictionary<string, string> loadLanguages()
        {
            const string TREE_SITTER_LANG_PAT = "tree-sitter-([A-Za-z0-9_]*)";
            Regex tree_sitter_reg = new Regex(TREE_SITTER_LANG_PAT);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            string currentDirectory = Environment.CurrentDirectory;
            var lang_dir = System.IO.Path.Combine(currentDirectory, LANG_DIR);
            foreach(var item in FileVisitor.GetAllFiles(lang_dir, TREE_SITTER_LANG_PAT))
            {
                var rawFn = Path.GetFileNameWithoutExtension(item);
                var rawName = tree_sitter_reg.Match(rawFn);
                if(rawName.Success)
                {
                    dict.Add(rawName.Groups[0].Value, item);
                }
            }
            return dict;
        }

        public static LanguageEntry createLanguage(string filename)
        {
            var handle = NativeLibrary.LoadLibrary(filename);
            if(handle == IntPtr.Zero)
            {
                return null;
            }
            var rawFn = Path.GetFileNameWithoutExtension(filename);
            var fnName = rawFn.Replace('-', '_');
            var new_lang_handle = NativeLibrary.GetProcAddress(handle, fnName);
            if (new_lang_handle == IntPtr.Zero)
            {
                return null;
            }
            var new_lang_fn = (NewLanguageDelegate)Marshal.GetDelegateForFunctionPointer(
                    new_lang_handle,
                    typeof(NewLanguageDelegate));
            var enty = new LanguageEntry { dll_handle = handle, new_fn = new_lang_fn };
            return enty;
        }

        public static void releaseLanguage(LanguageEntry entry)
        {
            if(entry.dll_handle != IntPtr.Zero)
            {
                NativeLibrary.FreeLibrary(entry.dll_handle);
                entry.dll_handle = IntPtr.Zero;
                entry.new_fn = null;
            }
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.SysInt)]
    delegate IntPtr NewLanguageDelegate();

    class LanguageEntry
    {
        public IntPtr dll_handle;
        public NewLanguageDelegate new_fn;
    }
    class PointRange
    {
        public TSPoint start_pt;
        public TSPoint end_pt;
        public string text;
    };

}
