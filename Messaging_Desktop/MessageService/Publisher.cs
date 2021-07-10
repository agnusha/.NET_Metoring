using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace MessageService
{
    public class Publisher
    {
        private readonly AmazonSimpleNotificationServiceClient _snsClient;
        private readonly string _topicName;
        private string _topicArn;
        private bool _initialised;
        
        //SQS supports a maximum message size of 256kb.
        private readonly int maxSize = 200000;

        public Publisher(AmazonSimpleNotificationServiceClient snsClient, string topicName)
        {
            _snsClient = snsClient;
            _topicName = topicName;
        }

        public async Task Initialise()
        {
            _topicArn = (await _snsClient.CreateTopicAsync(_topicName)).TopicArn;

            _initialised = true;
        }

        public async Task PublishAsync(byte[] bytesFromFile, string filename)
        {
            if (!_initialised)
                await Initialise();
            
            if (bytesFromFile.Length < maxSize)
            {
                await PublishOneMessageAsync(filename, bytesFromFile, 0);
            }
            
            else
            {
                var partCount = bytesFromFile.Length / maxSize;
                var resizedBytesList = bytesFromFile.AsEnumerable().Split(partCount).ToList();

                for (var i = 0; i < resizedBytesList.Count; i++)
                {
                    await PublishOneMessageAsync(filename, resizedBytesList[i].ToArray(), i);
                }
            }
        }

        private Task PublishOneMessageAsync(string filename, byte[] bytesFromFile, int order)
        {
            var message = Encoding.UTF8.GetString(bytesFromFile);

            var publishRequest = new PublishRequest(_topicArn, message)
            {
                MessageAttributes = new Dictionary<string, MessageAttributeValue>()
                {
                    {
                        "filename", new MessageAttributeValue {DataType = "String", StringValue = filename}
                    },
                    {
                        "order", new MessageAttributeValue {DataType = "String", StringValue = order.ToString()}
                    }
                }
            };

            return _snsClient.PublishAsync(publishRequest);
        }
    }

    internal static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
        {
            return list.Select((item, index) => new { index, item })
                .GroupBy(x => x.index % parts)
                .Select(x => x.Select(y => y.item));
        }
    }
}