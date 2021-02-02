using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

public class ReqProObject{
    [BsonElement("Movements")] public int Movements { get; set; }
    [BsonElement("Time")] public string Time { get; set; }
}
