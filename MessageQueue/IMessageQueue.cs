namespace MessageProcessing.MessageQueue
{
    public interface IMessageQueue
    {
        Task<string> ReceiveMessagesAsync();
        ServerStatistics DeserializeServerStatistics(string message);
    }
}