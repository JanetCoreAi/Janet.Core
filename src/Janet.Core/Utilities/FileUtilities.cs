using System;
using System.IO;

namespace Janet.Core.Utilities
{
    public static class FileUtilities
    {
        public static void CopyFile(string sourceFile, string destinationFile, bool overwrite = false)
        {
            File.Copy(sourceFile, destinationFile, overwrite);
        }

        public static void MoveFile(string sourceFile, string destinationFile, bool overwrite = false)
        {
            File.Move(sourceFile, destinationFile, overwrite);
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

        public static DateTime GetFileCreationTime(string filePath)
        {
            return File.GetCreationTime(filePath);
        }
    }

    public static class DirectoryUtilities
    {
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DeleteDirectory(string path, bool recursive = false)
        {
            Directory.Delete(path, recursive);
        }

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static string[] GetFiles(string path, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        public static string[] GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }
    }
}