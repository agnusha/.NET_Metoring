﻿using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using MessageService.Models;
using System.Threading.Tasks;

namespace MessageService
{
    public class MessagePublisherService
    {
        private readonly Publisher _publisher;

        public MessagePublisherService(ConfigAws configAws)
        {
            var credentials = new BasicAWSCredentials(configAws.AccessKey, configAws.SecretKey);

            using var snsClient = new AmazonSimpleNotificationServiceClient(credentials);
            _publisher = new Publisher(snsClient, configAws.TopicName);
        }

        public async Task SendMessageAsync(string message)
        {
            await _publisher.PublishAsync(message);
        }

    }
}
