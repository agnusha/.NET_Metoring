using MessageService.Models;
using Microsoft.Extensions.Configuration;
using System;
using MessageService;
using System.Threading.Tasks;

namespace Messaging_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Will listen the queue.");

                var configuration = new ConfigurationBuilder()
                    .AddUserSecrets<ConfigAws>()
                    .Build();
                var messageSubscriberService = new MessageSubscriberService(
                    configuration.GetSection(nameof(ConfigAws)).Get<ConfigAws>());

                await messageSubscriberService.ListenMessageAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
