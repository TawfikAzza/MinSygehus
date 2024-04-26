using Domain;
using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;
using PatientService.Service;

namespace PatientService.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase {

    private readonly PatientManager _patientManager;
    private readonly IHttpClientFactory _clientFactory;
    private readonly Tracer _tracer;
    private readonly IClientContext _clientContext;
    
    public PatientController(PatientManager patientManager, IHttpClientFactory clientFactory, Tracer tracer, IClientContext clientContext) {
        _patientManager = patientManager;
        _clientFactory = clientFactory;
        _tracer = tracer;
        _clientContext = clientContext;
    }
   
    
    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
    {
        using var activity = _tracer.StartActiveSpan("CreatePatient");
        
        if (!_clientContext["createpatient"].IsEnabled)
        {
            return StatusCode(418, "Create patient is disabled");
        }
        
        var result = await _patientManager.Create(patient);
        
        if (result is null) {
            Monitoring.Monitoring.Log.Error("Couldn't create the patient");
            return BadRequest("Couldn't create the patient");
        }

        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<Patient>> GetPatient(string ssn, bool measurement = false) {
        using var activity = _tracer.StartActiveSpan("GetPatient");
        var result = await _patientManager.GetBySsn(ssn);
        
        if (result is null) {
            Monitoring.Monitoring.Log.Error("Patient not found");
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
        if (!_clientContext["deletepatient"].IsEnabled)
        {
            return StatusCode(418, "Delete patient is disabled");
        }
        
        var result = await _patientManager.DeleteBySsn(ssn);
        
        if (!result) {
            Monitoring.Monitoring.Log.Error("Couldn't delete a patient");
            return BadRequest($"Couldn't delete the patient with ssn: {ssn}");
        }

        return Ok($"Deleted patient with ssn: {ssn}");
    }

}
