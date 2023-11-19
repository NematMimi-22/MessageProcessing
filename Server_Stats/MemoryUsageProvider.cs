using System.Diagnostics;

public class MemoryUsageProvider : IMemoryUsageProvider
{
    public double GetMemoryUsage()
    {
        var currentProcess = Process.GetCurrentProcess();
        var memoryUsageBytes = currentProcess.WorkingSet64;
        var memoryUsageMB = memoryUsageBytes / (1024.0 * 1024.0);

        return memoryUsageMB;
    }

    public double GetAvailableMemory()
    {
        var availablePhysicalMemory = new PerformanceCounter("Memory", "Available MBytes");

        return availablePhysicalMemory.NextValue();
    }
}