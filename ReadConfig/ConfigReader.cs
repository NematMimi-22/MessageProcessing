using Microsoft.Extensions.Configuration;
using MessageProcessing.ReadConfig;

namespace Weather_Monitoring.ReadConfig
{
    public class ConfigReader : IConfigurationReader
    {
        public AnomalyDetectionConfig ReadAnomalyDetectionConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var MemoryUsageAnomalyThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:MemoryUsageAnomalyThresholdPercentage"]);
            var CpuUsageAnomalyThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:CpuUsageAnomalyThresholdPercentage"]);
            var MemoryUsageThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:MemoryUsageThresholdPercentage"]);
            var CpuUsageThresholdPercentage = double.Parse(configuration["AnomalyDetectionConfig:CpuUsageThresholdPercentage"]);

            return new AnomalyDetectionConfig{
                MemoryUsageAnomalyThresholdPercentage = MemoryUsageAnomalyThresholdPercentage,
                CpuUsageAnomalyThresholdPercentage = CpuUsageAnomalyThresholdPercentage,
                MemoryUsageThresholdPercentage = MemoryUsageThresholdPercentage,
                CpuUsageThresholdPercentage = CpuUsageThresholdPercentage 
            };
        }

        public SignalRConfig ReadSignalRConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var SignalRUrl = configuration["SignalRConfig:SignalRUrl"];

            return new SignalRConfig { SignalRUrl = SignalRUrl };
        }

        public RabbitMQConfiguration ReadRabbitMQConfig()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return new RabbitMQConfiguration
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                QueueName = configuration["RabbitMQ:QueueName"]
            };
        }

        public string ReadMongoDBRepository()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration["MongoDBConfig:ConnectionString"];

            return  connectionString;
        }
    }
}