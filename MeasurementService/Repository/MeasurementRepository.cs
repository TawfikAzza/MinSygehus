using Domain;
using MongoDB.Driver;

namespace MeasurementService.Repository;

public class MeasurementRepository {
    private readonly IMongoCollection<Measurement> _measurements;

    public MeasurementRepository(IMongoClient client, IMongoDatabase database) {
        _measurements = database.GetCollection<Measurement>("measurements");
    }

    public async Task CreateMeasurementAsync(Measurement measurement) {
        await _measurements.InsertOneAsync(measurement);
    }

    public async Task UpdateMeasurementAsync(int id, Measurement measurement) {
        await _measurements.ReplaceOneAsync(m => m.Id == id, measurement);
    }

    public async Task<List<Measurement>> GetMeasurementsByPatientIdAsync(int id) {
        var filter = Builders<Measurement>.Filter.Eq(m => m.Id, id);
        return await _measurements.Find(filter).ToListAsync();
    }

    public async Task<Measurement?> GetLatestMeasurementBySsnAsync(string ssn) {
        var filter = Builders<Measurement>.Filter.Eq(m => m.Ssn, ssn);
        var sort = Builders<Measurement>.Sort.Descending(m => m.Date);
        return await _measurements.Find(filter).Sort(sort).FirstOrDefaultAsync();
    }

}