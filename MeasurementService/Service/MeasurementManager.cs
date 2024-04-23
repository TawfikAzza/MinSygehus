using Domain;
using MeasurementService.Repository;

namespace MeasurementService.Service;

public class MeasurementManager {
    private readonly MeasurementRepository _measurementRepository;
    
    public MeasurementManager(MeasurementRepository measurementRepository) {
        _measurementRepository = measurementRepository;
    }
    
    public async Task CreateMeasurement(Measurement measurement) {
        await _measurementRepository.CreateMeasurement(measurement);
    }
}
