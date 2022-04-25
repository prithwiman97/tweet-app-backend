using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;


namespace com.tweetapp.Models
{
    public class Tweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("content")]
        public string Content { get; set; }
        [BsonElement("created_by")]
        public string CreatedBy { get; set; }
        [BsonElement("likes")]
        public int Likes { get; set; }
    }
    
}
