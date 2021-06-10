using Example.PublisherApplication;
using MessageService;
using System;
using Microsoft.Extensions.Configuration;
using MessageService.Models;

namespace Messaging_Desktop
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main()
        {
            Console.WriteLine("Write a directory to listen");
            var directory = Console.ReadLine();

            var configuration = new ConfigurationBuilder()
              .AddUserSecrets<ConfigAws>()
              .Build();

            var messagePublisherService = new MessagePublisherService(
                configuration.GetSection(nameof(ConfigAws)).Get<ConfigAws>());

            Watcher.CreateWatcher(directory);

        }
    }
}
