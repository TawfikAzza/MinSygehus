using Domain;
using MongoDB.Driver;
using OpenTelemetry.Trace;
using PatientService.PatientContext;

namespace PatientService.Repository;

public class PatientRepository {
    
    private readonly DbContext _context;
    private readonly Tracer _tracer;
    public PatientRepository(DbContext dbContext, Tracer tracer) {
        _context = dbContext;
        _tracer = tracer;
    }

    public async Task<Patient?> Create(Patient patient) {
        using var activity = _tracer.StartActiveSpan("CreatePatient-Repository");
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
        using var activity = _tracer.StartActiveSpan("GetPatient-Repository");
        var filter = Builders<Patient>.Filter
            .Eq(r => r.Ssn, ssn);
        var result = await _context.Patients.FindAsync(filter);
        return await result.FirstOrDefaultAsync();
    }
    
    public async Task<bool> DeleteBySsn(string ssn) {
        using var activity = _tracer.StartActiveSpan("DeletePatient-Repository");
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
