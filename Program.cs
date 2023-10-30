var receiver = new MessageReceiver();
var receivedMessage = await receiver.ReceiveMessagesAsync();
var serverStats = receiver.DeserializeServerStatistics(receivedMessage);
var mongoRepo = new MongoDBRepository();
mongoRepo.InsertIntoMongoDB(serverStats);