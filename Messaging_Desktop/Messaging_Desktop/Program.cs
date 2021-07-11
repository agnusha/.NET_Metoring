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
                //var directory = "C:\\test";
                
                var configuration = new ConfigurationBuilder()
                  .AddUserSecrets<ConfigAws>()
                  .Build();

                var messagePublisherService = new MessagePublisherService(
                     configuration.GetSection(nameof(ConfigAws)).Get<ConfigAws>());

                Watcher.CreateWatcher(directory, async (byte[] bytesFromFile, string filename) =>
                {
                    await messagePublisherService.SendMessageAsync(bytesFromFile, filename);
                    Console.WriteLine("Sent message to SNS");
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
