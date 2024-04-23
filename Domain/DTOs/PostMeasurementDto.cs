namespace Domain.DTOs;

public class PostMeasurementDto {
    public string Ssn { get; set; } = default!;

    public int Systolic { get; set; }

    public int Diastolic { get; set; }
}
