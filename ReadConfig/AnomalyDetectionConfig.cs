﻿namespace MessageProcessing.ReadConfig
{
    public class AnomalyDetectionConfig
    {
        public double MemoryUsageAnomalyThresholdPercentage { get; set; }
        public double CpuUsageAnomalyThresholdPercentage { get; set; }
        public double MemoryUsageThresholdPercentage { get; set; }
        public double CpuUsageThresholdPercentage { get; set; }
        public AnomalyDetectionConfig(double memoryUsageAnomalyThresholdPercentage, double cpuUsageAnomalyThresholdPercentage, double memoryUsageThresholdPercentage, double cpuUsageThresholdPercentage)
        {
            MemoryUsageAnomalyThresholdPercentage = memoryUsageAnomalyThresholdPercentage;
            CpuUsageAnomalyThresholdPercentage = cpuUsageAnomalyThresholdPercentage;
            MemoryUsageThresholdPercentage = memoryUsageThresholdPercentage;
            CpuUsageThresholdPercentage = cpuUsageThresholdPercentage;
        }
    }
}