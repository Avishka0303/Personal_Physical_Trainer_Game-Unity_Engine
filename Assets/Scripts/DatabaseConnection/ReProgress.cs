using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

public class ReProgress{
    
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("PatientID")] 
    public ObjectId PatientID { get; set; }
    
    [BsonElement("TreatmentID")]
    public ObjectId TreatmentID { get; set; }

    [BsonElement("StartDate")] 
    public BsonDateTime StartDate { get; set; }

    [BsonElement("EndDate")] 
    public BsonDateTime EndDate { get; set; } 
    
    [BsonElement("RequiredProgress")] 
    public ReProObject2[] ReqProgress { get; set; }
    
    [BsonElement("__v")]
    public int V { get; set; }
    
}
