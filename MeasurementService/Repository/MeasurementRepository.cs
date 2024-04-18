using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using MongoDB.Driver;

namespace MeasurementService.Repository
{
    public class MeasurementRepository
    {
        private readonly IMongoCollection<Measurement> _measurements;

        public MeasurementRepository(IMongoClient client, IMongoDatabase database)
        {
            _measurements = database.GetCollection<Measurement>("measurements");
        }

        // Create a new measurement
        public async Task CreateMeasurementAsync(Measurement measurement)
        {
            await _measurements.InsertOneAsync(measurement);
        }

        // Update an existing measurement
        public async Task UpdateMeasurementAsync(int id, Measurement measurement)
        {
            await _measurements.ReplaceOneAsync(m => m.Id == id, measurement);
        }

        // Get measurements by patient Id
        public async Task<List<Measurement>> GetMeasurementsByPatientIdAsync(int id)
        {
            var filter = Builders<Measurement>.Filter.Eq(m => m.Id, id);
            return await _measurements.Find(filter).ToListAsync();
        }
    }
}
