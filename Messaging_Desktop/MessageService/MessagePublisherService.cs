using Amazon.Runtime;
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

            var snsClient = new AmazonSimpleNotificationServiceClient(credentials);
            _publisher = new Publisher(snsClient, configAws.TopicName);
        }

        public async Task SendMessageAsync(byte[] bytesFromFile, string filename)
        {
            await _publisher.PublishAsync(bytesFromFile, filename);
        }

    }
}
