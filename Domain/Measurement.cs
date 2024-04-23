using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public class Measurement {
    [BsonId]
    public int Id { get; set; }

    [BsonElement("Ssn")] 
    public required string Ssn { get; set; } 
    
    [BsonElement("Date")]
    public DateTime Date { get; set; }

    [BsonElement("Systolic")] 
    public int Systolic { get; set; }

    [BsonElement("Diastolic")] 
    public int Diastolic { get; set; }
    
    [BsonElement("Seen")]
    public bool Seen { get; set; }
}
