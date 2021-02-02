using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Injury {
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("TreatmentName")] 
    public string TreatmentName { get; set; }
    
    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("VideoGames")] 
    public string[] VideoGames { get; set; }
}
