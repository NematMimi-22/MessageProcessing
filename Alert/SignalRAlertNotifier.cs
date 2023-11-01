using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace MessageProcessing.Alert
{
    public class SignalRAlertNotifier : IAlertNotifier
    {
        private readonly HubConnection _hubConnection;

        public SignalRAlertNotifier(string signalRHubUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(signalRHubUrl)
                .Build();

            _hubConnection.Closed += async (error) =>
            {
                // Handle the error, log it, and attempt to restart the connection after a delay
                Console.WriteLine($"Connection closed with error: {error?.Message}");
                await Task.Delay(TimeSpan.FromSeconds(5)); // Wait for 5 seconds before restarting the connection
                await StartAsync(); // Restart the connection
            };
        }

        public async Task StartAsync()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                await _hubConnection.StartAsync();
            }
        }

        public async Task SendAnomalyAlert(ServerStatistics data)
        {
            await StartAsync();
            await _hubConnection.SendAsync("SendAnomalyAlert", data);
        }

        public async Task SendHighUsageAlert(ServerStatistics data)
        {
            await StartAsync();
            await _hubConnection.SendAsync("SendHighUsageAlert", data);
        }
    }
}