using Domain;
using MeasurementService.Context;

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
}
