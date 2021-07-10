using System;
using System.IO;
using System.Text;

namespace Messaging_Desktop
{
    public static class Watcher
    {
        private static Action<byte[], string> _action;
        public static void CreateWatcher(string folder, Action<byte[], string> action)
        {
            _action = action;
            var _watcher = new FileSystemWatcher(@$"{folder}")
            {
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size
            };

            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Error += OnError;

            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Console.WriteLine(value);

            var bytesFromFile = ReadFile(e.FullPath);
            _action(bytesFromFile, e.Name);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"Deleted: {e.FullPath}");

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }

        private static byte[] ReadFile(string path)
        {
            using var fstream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            return array;
        }
    }
}