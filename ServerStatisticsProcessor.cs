using MessageProcessing.Alert;
using Weather_Monitoring.ReadConfig;
public class ServerStatisticsProcessor
{
    private readonly IAlertNotifier _signalRClient;

    public ServerStatisticsProcessor(IAlertNotifier signalRClient)
    {
        _signalRClient = signalRClient;
    }
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
    public async Task ProcessServerStatistics(ServerStatistics current, ServerStatistics previous)
    {
        if (IsMemoryAnomaly(current, previous) || IsCpuAnomaly(current, previous))
        {
            await _signalRClient.SendAnomalyAlert(current);
        }
        else if (IsMemoryHighUsage(current) || IsCpuHighUsage(current))
        {
            await _signalRClient.SendHighUsageAlert(current);
        }
    }
}