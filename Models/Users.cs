using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace com.tweetapp.Models
{
    public class Users
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("dob")]
        public DateTime DOB { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("activeState")]
        public bool ActiveStatus { get; set; }

        public bool ValidateRequired()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(Username) || DOB == null || string.IsNullOrEmpty(Password))
                return false;
            return true;
        }
    }
}
