using Domain;
using Microsoft.AspNetCore.Mvc;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientServiceController : ControllerBase
{
    //TODO: Implement the following methods
    [HttpPost]
    public ActionResult<Patient> CreatePatient(Patient patient)
    {
        return Ok();
    }
    //The measurement boolean is there to allow the client to decide if they want to include the measurements in the response
    [HttpGet]
    public ActionResult<Patient> GetPatient(string ssn, bool measurement=false)
    {
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult DeletePatient(string ssn)
    {
        return Ok();
    }
}