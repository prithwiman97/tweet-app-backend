using com.tweetapp.Interfaces;
using com.tweetapp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.Repositories
{
    public class TweetRepository : ITweet,IReply
    {
        private readonly IMongoCollection<Tweet> tweets;
        private readonly IMongoCollection<Reply> replies;
        public TweetRepository(ITweetDatabaseSettings settings, IReplyDatabaseSettings replySettings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            tweets = database.GetCollection<Tweet>(settings.TweetsCollectionName);
            
            client = new MongoClient(replySettings.ConnectionString);
            database = client.GetDatabase(replySettings.DatabaseName);

            replies = database.GetCollection<Reply>(replySettings.ReplyCollectionName);
        }


        public void DeleteTweet(string id)
        {
            tweets.DeleteOne(t => t.Id == id);
        }

        public IEnumerable<Tweet> GetAllTweet()
        {
            try
            {
                return tweets.Find<Tweet>(_ => true).ToList();
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Reply> GetReplyList(string tweetId)
        {
            return replies.Find<Reply>(reply => reply.TweetId == tweetId).ToList();
        }

        public Tweet GetTweetById(string id)
        {
            return tweets.Find<Tweet>(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<Tweet> GetTweetsByUsername(string username)
        {
            try
            {
                return tweets.Find<Tweet>(u => u.CreatedBy == username).ToList();
            }
            catch
            {
                return null;
            }
        }

        public void InsertTweet(Tweet tweet)
        {
            try
            {
                tweets.InsertOne(tweet);
            }
            catch
            {
                throw new Exception("Couldn't create tweet.");
            }
        }

        public void LikeTweet(Tweet tweet)
        {
            var filter = Builders<Tweet>.Filter.Eq(t => t.Id, tweet.Id);
            var update = Builders<Tweet>.Update.Set(t => t.Likes, tweet.Likes + 1);
            var options = new UpdateOptions { IsUpsert = true };
            tweets.UpdateOne(filter, update, options);
        }

        public void ReplyToTweet(Reply reply)
        {
            try
            {
                replies.InsertOne(reply);
            }
            catch
            {
                throw new Exception("Couldn't rteply to tweet.");
            }
        }

        public void UpdateTweet(Tweet tweet)
        {
            var filter = Builders<Tweet>.Filter.Eq(t => t.Id, tweet.Id);
            var update = Builders<Tweet>.Update.Set(t => t.Content, tweet.Content);
            var options = new UpdateOptions { IsUpsert = true };
            tweets.UpdateOne(filter, update, options);
        }


    }
}
