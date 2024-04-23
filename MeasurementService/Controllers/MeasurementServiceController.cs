using Microsoft.AspNetCore.Mvc;
using MeasurementService.Service;

namespace MeasurementService.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MeasurementServiceController : ControllerBase {
        private readonly MeasurementManager _measurementManager;
        private readonly IHttpClientFactory _clientFactory;

        public MeasurementServiceController(MeasurementManager manager, IHttpClientFactory clientFactory) {
            _measurementManager = manager;
            _clientFactory = clientFactory;
        }

        /*
        [HttpPost]
        public async Task<ActionResult<Measurement>> PostMeasurement([FromBody] PostMeasurementDTO dto) {
            var measurement = new Measurement {
                Ssn = dto.Ssn,
                Id = dto.Id,
                Date = DateTime.UtcNow,
                Systolic = dto.Systolic,
                Diastolic = dto.Diastolic
            };

            await _repository.CreateMeasurementAsync(measurement);

            return Ok(measurement);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Measurement>> PutMeasurement(int id, [FromBody] Measurement measurement) {
            await _repository.UpdateMeasurementAsync(id, measurement);

            return Ok(measurement);
        }

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
}
