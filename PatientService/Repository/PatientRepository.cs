using Domain;
using MongoDB.Driver;
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
    
    public async Task<Patient?> GetBySsn(string ssn) {
        var filter = Builders<Patient>.Filter
            .Eq(r => r.Ssn, ssn);
        var result = await _context.Patients.FindAsync(filter);
        return await result.FirstOrDefaultAsync();
    }
    
    public async Task<bool> DeleteBySsn(string ssn) {
        try {
            var filter = Builders<Patient>.Filter
                .Eq(r => r.Ssn, ssn);
            await _context.Patients.DeleteOneAsync(filter);
            return true;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}
