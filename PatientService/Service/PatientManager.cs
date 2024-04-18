using Domain;
using PatientService.Repository;

namespace PatientService.Service;

public class PatientManager {

    private readonly PatientRepository _patientRepository;

    public PatientManager(PatientRepository patientRepository) {
        _patientRepository = patientRepository;
    }

    public async Task<Patient?> Create(Patient patient) {
        return await _patientRepository.Create(patient);
    }
}
