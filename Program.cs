using MessageProcessing.Alert;
using Weather_Monitoring.ReadConfig;

namespace MessageProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var signalRUrl = ConfigReader.ReadSignalRConfig().SignalRUrl;
            var hubContext = new SignalRAlertNotifier(signalRUrl);

            var receiver = new MessageReceiver();
            var receivedMessage = await receiver.ReceiveMessagesAsync();
            var previousServerStats = receiver.DeserializeServerStatistics(receivedMessage);
            var mongoRepo = new MongoDBRepository();
            mongoRepo.InsertIntoMongoDB(previousServerStats);

            var currentServerStats = ServerResourceMonitor.CurrentServerStatistics();
            var serverStatsProcessor = new ServerStatisticsProcessor(hubContext);
            await serverStatsProcessor.ProcessServerStatistics(currentServerStats, previousServerStats);
        }
    }
}