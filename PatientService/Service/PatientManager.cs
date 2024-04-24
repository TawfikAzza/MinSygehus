using Domain;
using OpenTelemetry.Trace;
using PatientService.Repository;

namespace PatientService.Service;

public class PatientManager {

    private readonly PatientRepository _patientRepository;
    private readonly Tracer _tracer;
    public PatientManager(PatientRepository patientRepository, Tracer tracer) {
        _patientRepository = patientRepository;
        _tracer = tracer;
    }

    public async Task<Patient?> Create(Patient patient) {
        using var activity = _tracer.StartActiveSpan("CreatePatient-Manager");
        return await _patientRepository.Create(patient);
    }

    public async Task<Patient?> GetBySsn(string ssn) {
        using var activity = _tracer.StartActiveSpan("GetPatient-Manager");
        return await _patientRepository.GetBySsn(ssn);
    }
    
    public async Task<bool> DeleteBySsn(string ssn) {
        using var activity = _tracer.StartActiveSpan("DeletePatient-Manager");
        return await _patientRepository.DeleteBySsn(ssn);
    }
}
