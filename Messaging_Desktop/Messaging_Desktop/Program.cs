using Example.PublisherApplication;
using MessageService;
using System;
using Microsoft.Extensions.Configuration;
using MessageService.Models;

namespace Messaging_Desktop
{
    static class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Write a directory to listen");
                var directory = Console.ReadLine();

                var configuration = new ConfigurationBuilder()
                  .AddUserSecrets<ConfigAws>()
                  .Build();

                var messagePublisherService = new MessagePublisherService(
                     configuration.GetSection(nameof(ConfigAws)).Get<ConfigAws>());

                Watcher.CreateWatcher(directory, (string textContent, string filename) =>
                {
                    Console.WriteLine("Sending message to SNS");
                    //await messagePublisherService.SendMessageAsync(path);
                    messagePublisherService.SendMessageAsync(textContent, filename).Wait();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            finally { 
                Console.ReadLine();
            }
        }
    }
}
