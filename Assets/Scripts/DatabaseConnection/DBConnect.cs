using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class DBConnect {
    
    private const string MONGO_URI = "mongodb+srv://Prageeth:1234@cluster0-yibwy.mongodb.net/test?retryWrites=true&w=majority";
    private const string DATABASE_NAME = "ppt";
    public static IMongoClient client { get; set; }
    public static IMongoDatabase database { get; set; }
    public static ObjectId currentUserID { get; set; }

    public static Patient current_patient { get; set; }

    public static void createConnection() {
        try {
            //create new client and connect to the database.
            client = new MongoClient(MONGO_URI);
            database = client.GetDatabase(DATABASE_NAME);
            var collection = database.GetCollection<Patient>("patient");
        } catch(Exception e) {
            Debug.Log(e.Message);
        }
    }

    public static void InsertRecord<T>(string collectionName, T document) {
        var collection = database.GetCollection<T>(collectionName);
        collection.InsertOne(document);
    }

    public static List<T> LoadRecords<T>(string collectionName) {
        var collection = database.GetCollection<T>(collectionName);
        return collection.Find(new BsonDocument()).ToList();
    }

    public static T LoadRecordById<T>(string collectionName, ObjectId id) {
        var collection = database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        return collection.Find(filter).First();
    }

    public static T LoadRecordByIdAndColumn<T>(string collectionName,ObjectId id,string columnName) {
        var collection = database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(columnName , id);
        return collection.Find(filter).First();
    }
    
    public static List<T> LoadRecordListByIdAndColumn<T>(string collectionName,ObjectId id,string columnName) {
        var collection = database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq(columnName , id);
        return collection.Find(filter).ToList();
    }

    public static void DeleteRecord<T>(string collectionName, ObjectId id) {
        var collection = database.GetCollection<T>(collectionName);
        var filter = Builders<T>.Filter.Eq("Id", id);
        collection.DeleteOne(filter);
    }

    public static bool IsAuthorized(string username, string password) {
        Debug.Log("AuthInit");
        ////& new FilterDefinitionBuilder<Patient>().Eq(patient=>patient.Password,password)
        var filter = new FilterDefinitionBuilder<Patient>().Eq(patient => patient.Username, username)
                     & new FilterDefinitionBuilder<Patient>().Eq(patient=>patient.IsApproved,"true");
        var collection = database.GetCollection<Patient>("patients");
        if (collection.CountDocuments(filter) == 0) {
            return false;
        }
        current_patient = collection.Find(filter).First();
        currentUserID = current_patient.Id;
        return true;
    }

    public void shutdown() {
        
    }
    
}
