public interface IRabbitMQConfiguration
{
    string HostName { get; }
    string UserName { get; }
    string Password { get; }
    string QueueName { get; set; }
}