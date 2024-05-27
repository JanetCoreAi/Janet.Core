using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Utilities
{
    public static class FileHandler
    {
        public static void SafeCreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void SafeCreateFile(string path)
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }

        }
        public static void SafeCreateFile(string path, string content)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, content);
            }
        }

    }
}
