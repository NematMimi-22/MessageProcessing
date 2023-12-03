using System.Diagnostics;

public class CpuUsageProvider : ICpuUsageProvider
{
    public double GetCpuUsage()
    {
        var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        cpuCounter.NextValue();
        Thread.Sleep(1000);
        return cpuCounter.NextValue();
    }
}