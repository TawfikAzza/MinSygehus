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
    public async Task<ActionResult<Patient>> GetPatient(string ssn, bool measurement = false) {
        var result = await _patientManager.GetBySsn(ssn);

        if (result is null) {
            return BadRequest("Patient not found");
        }

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePatient(string ssn) {
        var result = await _patientManager.DeleteBySsn(ssn);

        if (!result) {
            return BadRequest($"Couldn't delete the patient with ssn: {ssn}");
        }

        return Ok($"Deleted patient with ssn: {ssn}");
    }
}
