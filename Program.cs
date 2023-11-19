using MessageProcessing.Alert;
using Weather_Monitoring.ReadConfig;

namespace MessageProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var signalRUrl = new ConfigReader().ReadSignalRConfig();
            var hubContext = new SignalRAlertNotifier(signalRUrl);
            var rabbitMQConfiguration = new ConfigReader().ReadRabbitMQConfig();
            var receiver = new MessageReceiver(rabbitMQConfiguration);
            var receivedMessage = await receiver.ReceiveMessagesAsync();
            var previousServerStats = receiver.DeserializeServerStatistics(receivedMessage);
            var mongoRepo = new MongoDBRepository();
            mongoRepo.Insert(previousServerStats);
            var anomalyDetectionConfig = new ConfigReader();
            var currentServerStats = new ServerResourceMonitor();
            var serverStatsProcessor = new ServerStatisticsProcessor(hubContext, anomalyDetectionConfig);
            await serverStatsProcessor.ProcessServerStatistics(currentServerStats.CurrentServerStatistics(), previousServerStats);
        }
    }
}