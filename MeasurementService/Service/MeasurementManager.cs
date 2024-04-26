using Domain;
using MeasurementService.Repository;
using OpenTelemetry.Trace;

namespace MeasurementService.Service;

public class MeasurementManager {
    private readonly MeasurementRepository _measurementRepository;
    private readonly Tracer _tracer;
    
    public MeasurementManager(MeasurementRepository measurementRepository, Tracer tracer) {
        _measurementRepository = measurementRepository;
        _tracer = tracer;
    }
    
    public async Task<Measurement?> CreateMeasurement(Measurement measurement) {
        return await _measurementRepository.Create(measurement);
    }
    public async Task<Measurement?> UpdateMeasurement(Measurement measurement) {
        return await _measurementRepository.Update(measurement);
    }
    public async Task<List<Measurement>> GetAllBySsn(string ssn) {
        using var activity = _tracer.StartActiveSpan("GetMeasurement - Manager");
        return await _measurementRepository.GetAllBySsn(ssn);
    }
}
