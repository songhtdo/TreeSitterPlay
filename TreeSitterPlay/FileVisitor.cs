using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreeSitterPlay
{
    static class FileVisitor
    {
        public static List<string> GetAllFiles(string directoryPath, string pattern)
        {
            List<string> fileList = new List<string>();
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                if (Regex.IsMatch(Path.GetFileName(file), pattern))
                {
                    fileList.Add(file);
                }
            }
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
            {
                fileList.AddRange(GetAllFiles(subdirectory, pattern));
            }
            return fileList;
        }

        public static bool LoadFile(string fileName, ref string content)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, true))
                    {
                        content = sr.ReadToEnd();
                        return true;
                    }
                }
            }
            catch (Exception /*ex*/)
            {
                return false;
            }
        }

        public static bool SaveFile(string fileName, string content)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                    {
                        sw.Write(content);
                        return true;
                    }
                }
            }
            catch(Exception /*ex*/)
            {
                return false;
            }
        }
    }
}
