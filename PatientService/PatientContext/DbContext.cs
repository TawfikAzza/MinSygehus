using Domain;
using MongoDB.Driver;

namespace PatientService.PatientContext;

public class DbContext {
    private readonly IMongoDatabase _database;

    public DbContext(string connectionString, string databaseName) {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Patient> Patients => _database.GetCollection<Patient>("Patients");
}
