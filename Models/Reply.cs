using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.Models
{
    public class Reply
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("content")]
        public string Content { get; set; }
        [BsonElement("repliedBy")]
        public string RepliedBy { get; set; }
        [BsonElement("tweetId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TweetId { get; set; }
    }
}
