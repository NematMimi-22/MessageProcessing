using MongoDB.Driver;
using MessageProcessing.MongoDB;
public class MongoDBRepository : IRepository
{
        private readonly IMongoCollection<ServerStatistics> _collection;
        private string connectionString = "mongodb://localhost:27017";

        public MongoDBRepository()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ServerStatistics");
            _collection = database.GetCollection<ServerStatistics>("ServerStatistics");
        }
        public void Insert(ServerStatistics serverStats)
        {
            try
            {
                _collection.InsertOne(serverStats);
                Console.WriteLine($"Inserted data into MongoDB: {serverStats}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting data into MongoDB: {ex.Message}");
            }
        }
    }