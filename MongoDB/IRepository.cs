namespace MessageProcessing.MongoDB
{
    public interface IRepository
    {
        void Insert(ServerStatistics serverStats);
    }
}