namespace MeasurementService.DTO;

public record PostMeasurementDTO(int Id, DateTime Date, string Ssn, int Systolic, int Diastolic);
