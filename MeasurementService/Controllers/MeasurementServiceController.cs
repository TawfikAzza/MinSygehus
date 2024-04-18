using Domain;
using MeasurementService.DTO;
using Microsoft.AspNetCore.Mvc;
using MeasurementService.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeasurementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementServiceController : ControllerBase
    {
        private readonly MeasurementRepository _repository;

        public MeasurementServiceController(MeasurementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Measurement>> PostMeasurement([FromBody] PostMeasurementDTO dto)
        {
            var measurement = new Measurement
            {
                Id = dto.Id,
                Date = DateTime.UtcNow,
                Systolic = dto.Systolic,
                Diastolic = dto.Diastolic
            };

            await _repository.CreateMeasurementAsync(measurement);

            return Ok(measurement);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Measurement>> PutMeasurement(int id, [FromBody] Measurement measurement)
        {
            await _repository.UpdateMeasurementAsync(id, measurement);

            return Ok(measurement);
        }

        [HttpGet]
        public async Task<ActionResult<List<Measurement>>> GetMeasurement([FromQuery] int id)
        {
            var measurements = await _repository.GetMeasurementsByPatientIdAsync(id);

            return Ok(measurements);
        }
    }
}
