using MessageProcessing.ReadConfig;

namespace Weather_Monitoring.ReadConfig
{
    public interface IConfigurationReader
    {
        AnomalyDetectionConfig ReadAnomalyDetectionConfig();
        SignalRConfig ReadSignalRConfig();
        RabbitMQConfiguration ReadRabbitMQConfig();
        string ReadMongoDBRepository();
    }
}