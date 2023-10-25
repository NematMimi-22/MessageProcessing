using MessageProcessing.ReadConfig;
using Microsoft.Extensions.Configuration;
namespace Weather_Monitoring.ReadConfig
{
    public class ConfigReader
    {
        public static AnomalyDetectionConfig ReadAnomalyDetectionConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var MemoryUsageAnomalyThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:MemoryUsageAnomalyThresholdPercentage"]);
            var CpuUsageAnomalyThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:CpuUsageAnomalyThresholdPercentage"]);
            var MemoryUsageThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:MemoryUsageThresholdPercentage"]);
            var CpuUsageThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:CpuUsageThresholdPercentage"]);

            return new AnomalyDetectionConfig(MemoryUsageAnomalyThresholdPercentage, CpuUsageAnomalyThresholdPercentage, MemoryUsageThresholdPercentage, CpuUsageThresholdPercentage);
        }

        public static SignalRConfig ReadSignalRConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var SignalRUrl = configuration["SignalRConfig:SignalRUrl"];

            return new SignalRConfig(SignalRUrl);
        }
    }
}