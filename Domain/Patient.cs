using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public class Patient {
    [BsonElement("Ssn")]
    public string Ssn { get; set; } = default!;
    
    [BsonElement("Mail")]
    public string Mail { get; set; } = default!;
    
    [BsonElement("Name")]
    public string Name { get; set; } = default!;
}