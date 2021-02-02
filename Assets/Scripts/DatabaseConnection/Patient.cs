using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Patient {
    
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("approved")] 
    public string IsApproved { get; set; }
    
    [BsonElement("firstName")] 
    public string Fname { get; set; }
    
    [BsonElement("lastName")] 
    public string Lname { get; set; } 
    
    [BsonElement("gender")]
    public string Gender { get; set; }
    
    [BsonElement("contactNo")]
    public double ContactNo { get; set; }
    
    [BsonElement("username")] 
    public string Username { get; set; }
    
    [BsonElement("therapist")]
    public string Therapist { get; set; }
    
    [BsonElement("injury")]
    public string Injury { get; set; }

    [BsonElement("createdDate")]
    public DateTime CDate { get; set; }
    
    [BsonElement("hash")]
    public string Password { get; set; }
    
    [BsonElement("__v")]
    public int V { get; set; }
    
}
