using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using System.Threading.Tasks;

namespace MessageService
{
    public class MessagePublisherService
    {
        private readonly Publisher _publisher;

        public MessagePublisherService()
        {
            var credentials = new BasicAWSCredentials("accessKey", "secretKey");

            using var snsClient = new AmazonSimpleNotificationServiceClient(credentials);
            _publisher = new Publisher(snsClient, "topicName");
        }

        public async Task SendMessageAsync(string message)
        {
            await _publisher.PublishAsync(message);
        }

    }
}
