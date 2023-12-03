﻿using Microsoft.AspNetCore.SignalR.Client;

namespace MessageProcessing.Alert
{
    public class SignalRAlertNotifier : IAlertNotifier
    {
        private readonly HubConnection _hubConnection;

        public SignalRAlertNotifier(ISignalRConfig signalRHubUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(signalRHubUrl.SignalRUrl)
                .Build();

            _hubConnection.Closed += async (error) =>
            {
                Console.WriteLine($"Connection closed with error: {error?.Message}");
                await Task.Delay(TimeSpan.FromSeconds(5));
                await StartAsync();
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