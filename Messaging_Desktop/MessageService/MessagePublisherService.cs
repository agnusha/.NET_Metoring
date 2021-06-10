using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using MessageService.Models;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MessageService
{
    public class MessagePublisherService
    {
        private readonly ConfigAws _configAws;
        private readonly Publisher _publisher;

        public MessagePublisherService(IOptions<ConfigAws> configAws)
        {
            _configAws = configAws.Value ?? throw new ArgumentNullException(nameof(configAws));

            var credentials = new BasicAWSCredentials(_configAws.AccessKey, _configAws.SecretKey);

            using var snsClient = new AmazonSimpleNotificationServiceClient(credentials);
            _publisher = new Publisher(snsClient, _configAws.TopicName);
        }

        public async Task SendMessageAsync(string message)
        {
            await _publisher.PublishAsync(message);
        }

    }
}
