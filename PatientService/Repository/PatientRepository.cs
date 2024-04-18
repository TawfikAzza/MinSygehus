using Domain;
using PatientService.PatientContext;

namespace PatientService.Repository;

public class PatientRepository {
    
    private readonly DbContext _context;

    public PatientRepository(DbContext dbContext) {
        _context = dbContext;
    }

    public async Task<Patient?> Create(Patient patient) {
        try {
            await _context.Patients.InsertOneAsync(patient);
            return patient;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
