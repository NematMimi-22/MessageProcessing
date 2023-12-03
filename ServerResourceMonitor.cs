public class ServerResourceMonitor
{
    private readonly IMemoryUsageProvider _memoryUsageProvider;
    private readonly ICpuUsageProvider _cpuUsageProvider;

    public ServerStatistics CurrentServerStatistics()
    {
        return new ServerStatistics
        {
            MemoryUsage = _memoryUsageProvider.GetMemoryUsage(),
            AvailableMemory = _memoryUsageProvider.GetAvailableMemory(),
            CpuUsage = _cpuUsageProvider.GetCpuUsage(),
            Timestamp = DateTime.UtcNow
        };
    }
}