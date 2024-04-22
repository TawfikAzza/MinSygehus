using Microsoft.EntityFrameworkCore;
using Domain;
using MongoDB.Driver;

namespace MeasurementService.Context
{
    public class DatabaseContext : DbContext
    {
        public IMongoCollection<Measurement> Measurements { get; private set; }

        public DatabaseContext(IMongoCollection<Measurement> measurements)
        {
            Measurements = measurements;
        }
    }
}
