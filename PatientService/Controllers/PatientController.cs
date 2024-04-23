using Domain;
using Microsoft.AspNetCore.Mvc;
using PatientService.Service;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase {

    private readonly PatientManager _patientManager;
    private readonly IHttpClientFactory _clientFactory;

    public PatientController(PatientManager patientManager, IHttpClientFactory clientFactory) {
        _patientManager = patientManager;
        _clientFactory = clientFactory;
    }

    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient(Patient patient) {
        var result = await _patientManager.Create(patient);

        if (result is null) {
            return BadRequest("Couldn't create the patient");
        }

        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<Patient>> GetPatient(string ssn, bool measurement = false) {
        var result = await _patientManager.GetBySsn(ssn);

        if (result is null) {
            return BadRequest("Patient not found");
        }
        
        if (measurement) {
            using var instance = _clientFactory.CreateClient();
            var measurementResult = await instance.GetAsync(Constants.MeasurementAddress + $"?ssn={ssn}");

            if (!measurementResult.IsSuccessStatusCode) {
                return Ok(result);
            }
            
            // TODO: Test
            result.Measurements = await measurementResult.Content.ReadFromJsonAsync<List<Measurement>>();
            return Ok(result);
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
