﻿public class ServerStatistics
{
    public String ServerIdentifier { get; set; }
    public double MemoryUsage { get; set; } 
    public double AvailableMemory { get; set; } 
    public double CpuUsage { get; set; }
    public DateTime Timestamp { get; set; }
}