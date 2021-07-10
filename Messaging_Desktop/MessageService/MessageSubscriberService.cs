using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MessageService.Models;
using System;
using System.Threading.Tasks;

namespace MessageService
{
    public class MessageSubscriberService
    {
        private readonly Subscriber _subscriber;

        public MessageSubscriberService(ConfigAws configAws)
        {
            var credentials = new BasicAWSCredentials(configAws.AccessKey, configAws.SecretKey);

            var sqsClient = new AmazonSQSClient(credentials);
            var snsClient = new AmazonSimpleNotificationServiceClient(credentials);
            _subscriber = new Subscriber(sqsClient, snsClient, configAws.TopicName, configAws.QueueName);
        }

        public async Task ListenMessageAsync()
        {
            await _subscriber.ListenAsync(message => {
                Console.WriteLine("Received message: " + message);
            });
        }
    }
}
