using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class AchProgress{
    
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("PatientID")] 
    public ObjectId PatientID { get; set; }
    
    [BsonElement("TreatmentID")]
    public ObjectId TreatmentID { get; set; }

    [BsonElement("Date")] 
    public BsonDateTime Date { get; set; }

    [BsonElement("Time")] 
    public int Time { get; set; } 
    
    [BsonElement("AchievedProgress")] 
    public ReqProObject[] Progress { get; set; }
}
