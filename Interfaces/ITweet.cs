using com.tweetapp.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.Interfaces
{
    public interface ITweet
    {
        public void InsertTweet(Tweet tweet);
        public IEnumerable<Tweet> GetTweetsByUsername(string username);
        public IEnumerable<Tweet> GetAllTweet();
        public Tweet GetTweetById(string id);
        public void UpdateTweet(Tweet tweet);
        public void LikeTweet(Tweet tweet);
        public void ReplyToTweet(Reply reply);
        public void DeleteTweet(string id);
    }
}
