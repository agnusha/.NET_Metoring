using Example.PublisherApplication;
using System;

namespace Messaging_Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write a directory to linsten");
            var directory = Console.ReadLine();

            Watcher.CreateWatcher(directory);

        }
    }
}
