using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using System;
using System.Threading.Tasks;

namespace MessageService
{
    public class MessageSubscriberService
    {
        private readonly Subscriber _subscriber;

        public MessageSubscriberService()
        {
            var credentials = new BasicAWSCredentials("accessKey", "secretKey");

            using var sqsClient = new AmazonSQSClient(credentials);
            using var snsClient = new AmazonSimpleNotificationServiceClient(credentials);
            _subscriber = new Subscriber(sqsClient, snsClient, "topicName", "queueName");
        }

        public async Task ListenMessageAsync()
        {
            await _subscriber.ListenAsync(message => {
                Console.WriteLine("Received message: " + message);
            });
        }
    }
}
