using MessageProcessing.MessageQueue;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
public class MessageReceiver : IMessageQueue
{
    private readonly IRabbitMQConfiguration _rabbitMQConfiguration;

    public MessageReceiver(IRabbitMQConfiguration rabbitMQConfiguration)
    {
        _rabbitMQConfiguration = rabbitMQConfiguration;
    }

    public async Task<string> ReceiveMessagesAsync()
    {
        var messageReceivedTaskCompletionSource = new TaskCompletionSource<string>();
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMQConfiguration.HostName,
            UserName = _rabbitMQConfiguration.UserName,
            Password = _rabbitMQConfiguration.Password
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _rabbitMQConfiguration.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var byteArray = body.ToArray();
                var message = Encoding.UTF8.GetString(byteArray);
                var serverStats = DeserializeServerStatistics(message);
                Console.WriteLine($"Received message: {message}");
                messageReceivedTaskCompletionSource.SetResult(message);
            };
            channel.BasicConsume(queue: _rabbitMQConfiguration.QueueName, autoAck: true, consumer: consumer);
            return await messageReceivedTaskCompletionSource.Task;
        }
    }

    public ServerStatistics DeserializeServerStatistics(string message)
    {
        var doc = JsonDocument.Parse(message);
        var root = doc.RootElement;
        if (root.TryGetProperty("Statistics", out var statisticsElement) &&
            root.TryGetProperty("ServerIdentifier", out var serverIdentifierElement))
        {
            var serverStats = new ServerStatistics
            {
                MemoryUsage = statisticsElement.GetProperty("MemoryUsage").GetDouble(),
                AvailableMemory = statisticsElement.GetProperty("AvailableMemory").GetDouble(),
                CpuUsage = statisticsElement.GetProperty("CpuUsage").GetDouble(),
                Timestamp = statisticsElement.GetProperty("Timestamp").GetDateTime(),
            };
            var serverIdentifier = serverIdentifierElement.GetString();
            serverStats.ServerIdentifier = serverIdentifier;
            return serverStats;
        }
        Console.WriteLine("Error: Invalid JSON format");
        return null;
    }
}