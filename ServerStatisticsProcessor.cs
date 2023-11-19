using MessageProcessing.Alert;
using Weather_Monitoring.ReadConfig;
public class ServerStatisticsProcessor
{
    private readonly IAlertNotifier _signalRClient;
    private readonly IConfigurationReader _configReader;

    public ServerStatisticsProcessor(IAlertNotifier signalRClient, IConfigurationReader configReader)
    {
        _signalRClient = signalRClient;
        _configReader = configReader;
    }
    private bool IsMemoryAnomaly(ServerStatistics current, ServerStatistics previous)
    {
        return current.MemoryUsage > (previous.MemoryUsage * (1 + _configReader.ReadAnomalyDetectionConfig().MemoryUsageAnomalyThresholdPercentage));
    }

    private bool IsCpuAnomaly(ServerStatistics current, ServerStatistics previous)
    {
        return current.CpuUsage > (previous.CpuUsage * (1 + _configReader.ReadAnomalyDetectionConfig().CpuUsageAnomalyThresholdPercentage));
    }

    private bool IsMemoryHighUsage(ServerStatistics statistics)
    {
        return (statistics.MemoryUsage / (statistics.MemoryUsage + statistics.AvailableMemory)) > _configReader.ReadAnomalyDetectionConfig().MemoryUsageThresholdPercentage;
    }

    private bool IsCpuHighUsage(ServerStatistics statistics)
    {
        return statistics.CpuUsage > _configReader.ReadAnomalyDetectionConfig().CpuUsageThresholdPercentage;
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