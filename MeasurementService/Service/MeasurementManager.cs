using Domain;
using MeasurementService.Repository;

namespace MeasurementService.Service;

public class MeasurementManager {
    private readonly MeasurementRepository _measurementRepository;
    
    public MeasurementManager(MeasurementRepository measurementRepository) {
        _measurementRepository = measurementRepository;
    }
    
    public async Task<Measurement?> CreateMeasurement(Measurement measurement) {
        return await _measurementRepository.Create(measurement);
    }
    public async Task<Measurement?> UpdateMeasurement(Measurement measurement) {
        return await _measurementRepository.Update(measurement);
    }
    public async Task<List<Measurement>> GetAllBySsn(string ssn) {
        return await _measurementRepository.GetAllBySsn(ssn);
    }
}
