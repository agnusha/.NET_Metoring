using Example.PublisherApplication;
using MessageService;
using System;

namespace Messaging_Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write a directory to linsten");
            var directory = Console.ReadLine();

            var messagePublisherService = new MessagePublisherService();

            Watcher.CreateWatcher(directory);

        }
    }
}
