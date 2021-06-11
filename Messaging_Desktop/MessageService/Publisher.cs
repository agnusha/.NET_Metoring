using System;
using System.Collections.Generic;
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

        public async Task PublishAsync(string message, string filename)
        {
            if (!_initialised)
                await Initialise();

            var publishRequest = new PublishRequest(_topicArn, message)
            {
                MessageAttributes = new Dictionary<string, MessageAttributeValue>() {
                    { 
                        "filename", new MessageAttributeValue { DataType = "String", StringValue = filename }
                    }
                }
            };

            await _snsClient.PublishAsync(publishRequest);
        }
    }
}