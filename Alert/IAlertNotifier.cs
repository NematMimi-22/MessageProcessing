namespace MessageProcessing.Alert
{
    public interface IAlertNotifier
    {
        Task SendAnomalyAlert(ServerStatistics data);
        Task SendHighUsageAlert(ServerStatistics data);
    }
}