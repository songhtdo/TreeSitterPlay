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
    }
}
