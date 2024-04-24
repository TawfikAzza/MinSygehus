using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using MeasurementService.Service;
using OpenTelemetry.Trace;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase {
    private readonly MeasurementManager _measurementManager;
    private readonly IHttpClientFactory _clientFactory;
    private readonly Tracer _tracer;

    public MeasurementController(MeasurementManager manager, IHttpClientFactory clientFactory, Tracer tracer) {
        _measurementManager = manager;
        _clientFactory = clientFactory;
        _tracer = tracer;
    }
    
    [HttpGet]
    public async Task<ActionResult<Measurement>> GetAllBySsn([FromQuery] string ssn) {
        using var activity = _tracer.StartActiveSpan("GetMeasurement");
        var result = await _measurementManager.GetAllBySsn(ssn);

        if (result.Count == 0) {
            return BadRequest($"Measurements were not found for ssn {ssn}");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Measurement>> Create([FromBody] PostMeasurementDto dto) {
        var random = new Random();
        // Mapping DTO to domain model
        var measurement = new Measurement {
            Id = random.Next(Int32.MaxValue),
            Ssn = dto.Ssn,
            Date = DateTime.UtcNow,
            Systolic = dto.Systolic,
            Diastolic = dto.Diastolic,
            Seen = false
        };

        // Sending create task to the manager
        var result = await _measurementManager.CreateMeasurement(measurement);

        // Check for errors
        if (result is null) {
            return BadRequest("Couldn't create the measurement");
        }

        return Ok(measurement);
    }

    
    [HttpPut]
    public async Task<ActionResult<Measurement>> Update([FromBody] Measurement measurement) {
        var result = await _measurementManager.UpdateMeasurement(measurement);
        
        if (result is null) {
            return BadRequest("Couldn't update the measurement");
        }

        return Ok(measurement);
    }
}
