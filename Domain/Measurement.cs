namespace Domain;

public class Measurement
{

    public required string Ssn { get; set; }
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
}