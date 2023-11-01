using System.Diagnostics;
public class ServerResourceMonitor
{
    public static ServerStatistics CurrentServerStatistics()
    {
        return new ServerStatistics
        {
            MemoryUsage = GetMemoryUsage(),
            AvailableMemory = GetAvailableMemory(),
            CpuUsage = GetCpuUsage(),
            Timestamp = DateTime.UtcNow
        };
    }

    private static double GetMemoryUsage()
    {
        var currentProcess = Process.GetCurrentProcess();
        var memoryUsageBytes = currentProcess.WorkingSet64;
        var memoryUsageMB = memoryUsageBytes / (1024.0 * 1024.0);

        return memoryUsageMB;
    }

    private static double GetAvailableMemory()
    {
        var availablePhysicalMemory = new PerformanceCounter("Memory", "Available MBytes");
        return availablePhysicalMemory.NextValue();
    }

    private static double GetCpuUsage()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        cpuCounter.NextValue();
        Thread.Sleep(1000);
        return cpuCounter.NextValue();
    }
}