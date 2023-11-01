namespace MessageProcessing.MongoDB
{
    public interface IMongoDBRepository
    {
        void InsertIntoMongoDB(ServerStatistics serverStats);
    }
}