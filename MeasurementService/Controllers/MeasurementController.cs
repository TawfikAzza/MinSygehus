using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using MeasurementService.Service;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase {
    private readonly MeasurementManager _measurementManager;
    private readonly IHttpClientFactory _clientFactory;

    public MeasurementController(MeasurementManager manager, IHttpClientFactory clientFactory) {
        _measurementManager = manager;
        _clientFactory = clientFactory;
    }

    [HttpPost]
    public async Task<ActionResult<Measurement>> Create([FromBody] PostMeasurementDto dto) {
        // Mapping DTO to domain model
        var measurement = new Measurement {
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

        return Ok(measurement);
    }

    /*
    [HttpGet]
    public async Task<ActionResult<List<Measurement>>> GetMeasurement([FromQuery] int id) {
        var measurements = await _repository.GetMeasurementsByPatientIdAsync(id);

        return Ok(measurements);
    }

    [HttpGet("BySsn")]
    public async Task<ActionResult<List<Measurement>>> GetMeasurementBySsn([FromQuery] string ssn) {
        var patientMeasurements = await _repository.GetLatestMeasurementBySsnAsync(ssn);

        if (patientMeasurements is null) {
            return BadRequest("Measurements not found");
        }

        return Ok(patientMeasurements);
    }
    */
}
