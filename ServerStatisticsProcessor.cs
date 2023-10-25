using Weather_Monitoring.ReadConfig;
public class ServerStatisticsProcessor
{
    //private readonly IMongoDBService _mongoDBService;
    //   private readonly IMessageQueueService _messageQueueService;
    //  private readonly IHubContext<AlertHub> _hubContext;
    //private readonly AnomalyDetectionConfig _anomalyDetectionConfig;

    /*  public ServerStatisticsProcessor( IOptions<AnomalyDetectionConfig> anomalyDetectionConfig)
      {

      }

      public async Task StartProcessingAsync()
      {
          await _messageQueueService.StartReceivingMessagesAsync();
      }

      public async Task ProcessMessageAsync(ServerStatistics data)
      {
          // Persist data to MongoDB
          await _mongoDBService.InsertServerStatisticsAsync(data);

          // Detect anomalies and send alerts
          if (IsMemoryUsageAnomaly(data) || IsCpuUsageAnomaly(data))
          {
              await _hubContext.Clients.All.SendAsync("ReceiveAnomalyAlert", data);
          }
          else if (IsMemoryHighUsage(data) || IsCpuHighUsage(data))
          {
              await _hubContext.Clients.All.SendAsync("ReceiveHighUsageAlert", data);
          }
      }*/

    private bool IsMemoryAnomaly(ServerStatistics current, ServerStatistics previous)
    {
        return current.MemoryUsage > (previous.MemoryUsage * (1 + ConfigReader.ReadAnomalyDetectionConfig().MemoryUsageAnomalyThresholdPercentage));
    }

    private bool IsCpuAnomaly(ServerStatistics current, ServerStatistics previous)
    {
        return current.CpuUsage > (previous.CpuUsage * (1 + ConfigReader.ReadAnomalyDetectionConfig().CpuUsageAnomalyThresholdPercentage));
    }

    private bool IsMemoryHighUsage(ServerStatistics statistics)
    {
        return (statistics.MemoryUsage / (statistics.MemoryUsage + statistics.AvailableMemory)) > ConfigReader.ReadAnomalyDetectionConfig().MemoryUsageThresholdPercentage;
    }

    private bool IsCpuHighUsage(ServerStatistics statistics)
    {
        return statistics.CpuUsage > ConfigReader.ReadAnomalyDetectionConfig().CpuUsageThresholdPercentage;
    }
}