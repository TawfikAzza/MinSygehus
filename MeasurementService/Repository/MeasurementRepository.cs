﻿using Domain;
using MeasurementService.Context;
using MongoDB.Driver;
using OpenTelemetry.Trace;

namespace MeasurementService.Repository;

public class MeasurementRepository {

    private readonly DbContext _context;
    private readonly Tracer _tracer;

    public MeasurementRepository(DbContext dbContext, Tracer tracer) {
        _context = dbContext;
        _tracer = tracer;
    }
    
    public async Task<List<Measurement>> GetAllBySsn(string ssn) {
        using var activity = _tracer.StartActiveSpan("GetMeasurement - Repository");
        try {
            var filter = Builders<Measurement>.Filter
                .Eq(r => r.Ssn, ssn);
            return await _context.Measurements.Find(filter).ToListAsync();
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return new List<Measurement>();
        }
    }

    public async Task<Measurement?> Create(Measurement measurement) {
        try {
            await _context.Measurements.InsertOneAsync(measurement);
            return measurement;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    public async Task<Measurement?> Update(Measurement measurement) {
        try {
            var filter = Builders<Measurement>.Filter
                .Eq(r => r.Id, measurement.Id);
            var result = await _context.Measurements.ReplaceOneAsync(filter, measurement);
            return result.IsAcknowledged ? measurement : null;
        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
