using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MessageService.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
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
            await _subscriber.ListenAsync(async messages =>
            {
                var messageInfos = messages.Select(m => new MessageInfo(m.MessageAttributes["filename"].StringValue,
                        m.MessageAttributes["order"].StringValue, m.Body)).OrderBy(mi => mi.Order)
                    .GroupBy(mi => mi.FileName);

                foreach (var messageInfo in messageInfos)
                {
                    var fullContent = string.Join("", messageInfo.Select(mi => mi.Body));
                    await CreateFile(messageInfo.Key, fullContent);
                    Console.WriteLine("Received message for file: " + messageInfo.Key);
                }
            });
        }

        public async Task CreateFile(string name, string content)
        {
            var fileName = $@"C:\Users\Ahniya_Staravoitava\Downloads\{name}";

            if (File.Exists(fileName))
            {
                return;
            }

            await using var fs = File.Create(fileName);
            var title = new UTF8Encoding(true).GetBytes(content);
            await fs.WriteAsync(title, 0, title.Length);
        }
    }
}
