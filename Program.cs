var receiver = new MessageReceiver();
var receivedMessage = await receiver.ReceiveMessagesAsync();
var Obj = receiver.DeserializeServerStatistics(receivedMessage);
var mongoRepo = new MongoDBRepository();
mongoRepo.InsertIntoMongoDB(Obj);