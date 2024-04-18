using Domain;
using Microsoft.AspNetCore.Mvc;
using PatientService.Service;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase {

    private readonly PatientManager _patientManager;

    public PatientController(PatientManager patientManager) {
        _patientManager = patientManager;
    }

    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient(Patient patient) {
        var result = await _patientManager.Create(patient);

        if (result is null) {
            return BadRequest("Couldn't create the patient");
        }

        return Ok(result);
    }

    //The measurement boolean is there to allow the client to decide if they want to include the measurements in the response
    [HttpGet]
    public ActionResult<Patient> GetPatient(string ssn, bool measurement = false) {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeletePatient(string ssn) {
        return Ok();
    }
}
