using Domain;
using MeasurementService.Context;
using MongoDB.Driver;

namespace MeasurementService.Repository;

public class MeasurementRepository {

    private readonly DbContext _context;

    public MeasurementRepository(DbContext dbContext) {
        _context = dbContext;
    }

    public async Task<Measurement?> Create(Measurement measurement) {
        try {
            await _context.Measurements.InsertOneAsync(measurement);
            return measurement;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    public async Task<Measurement?> Update(Measurement measurement) {
        try {
            var filter = Builders<Measurement>.Filter
                .Eq(r => r.Id, measurement.Id);
            var result = await _context.Measurements.ReplaceOneAsync(filter, measurement);
            return result.IsAcknowledged ? measurement : null;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
    }

}
