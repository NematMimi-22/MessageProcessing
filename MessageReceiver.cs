using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
public class MessageReceiver
{
    private readonly string _queueName = "test";

    public void ReceiveMessages()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                byte[] byteArray = body.ToArray();
                var message = Encoding.UTF8.GetString(byteArray);
                ServerStatistics serverStats = DeserializeServerStatistics(message);
                Console.WriteLine($"Received message: {message}");
            };

            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
    private ServerStatistics DeserializeServerStatistics(string message)
    {
        ServerStatistics serverStats = JsonConvert.DeserializeObject<ServerStatistics>(message);
        return serverStats;
    }
}