namespace MessageService.Models
{
    public class MessageInfo
    {
        public MessageInfo(string fileName, string order, string body)
        {
            FileName = fileName;
            Order = order;
            Body = body;
        }
        
        public string FileName { get; set; }
        public string Order { get; set; }
        public string Body { get; set; }
    }
}
