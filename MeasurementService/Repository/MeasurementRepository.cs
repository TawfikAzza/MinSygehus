using Domain;
using MeasurementService.Context;

namespace MeasurementService.Repository;

public class MeasurementRepository {

    private readonly DbContext _context;

    public MeasurementRepository(DbContext dbContext) {
        _context = dbContext;
    }

    public async Task CreateMeasurement(Measurement measurement) {
        
    }
}
