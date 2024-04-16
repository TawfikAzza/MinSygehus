using Domain;
using MeasurementService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementServiceController : ControllerBase
{
    
    //TODO: Implement the following methods
    [HttpPost]
    public ActionResult<Measurement> PostMeasurement([FromBody] PostMeasurementDTO dto)
    {
        return Ok();
    }
    
    [HttpPut]
    public ActionResult<Measurement> PutMeasurement([FromBody] Measurement measurement)
    {
        return Ok();
    }
    
    [HttpGet]
    public ActionResult<List<Measurement>> GetMeasurement([FromQuery] string ssn)
    {
        return Ok();
    }
}