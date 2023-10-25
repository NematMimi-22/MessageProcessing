namespace MessageProcessing.ReadConfig
{
    public class SignalRConfig
    {
        public string SignalRUrl { get; set; }
        public SignalRConfig(string signalRUrl)
        {
            SignalRUrl = signalRUrl;
        }
    }
}
