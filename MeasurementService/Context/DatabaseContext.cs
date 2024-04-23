using Domain;
using MongoDB.Driver;

namespace MeasurementService.Context;

public class DbContext {
    private readonly IMongoDatabase _database;

    public DbContext(string connectionString, string databaseName) {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Measurement> Measurements => _database.GetCollection<Measurement>("Measurements");
}