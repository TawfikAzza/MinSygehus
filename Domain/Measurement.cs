namespace Domain;

public class Measurement {

    public string Ssn { get; set; }

    public DateTime Date { get; set; }

    public int Systolic { get; set; }

    public int Diastolic { get; set; }
    
    public bool Seen { get; set; }
}
