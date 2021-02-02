using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

public class ReProObject2 : MonoBehaviour
{
    [BsonElement("Movements")] public int Movements { get; set; }
    [BsonElement("Time")] public int Time { get; set; }
}
