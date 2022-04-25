using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using com.tweetapp.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace com.tweetapp.Repositories
{
    public class UsersRepository : IUsers
    {
        private readonly IMongoCollection<Users> users;

        public UsersRepository(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            users = database.GetCollection<Users>(settings.UsersCollectionName);
        }
        public Users GetUser(string username)
        {
            try
            {
                Users user = users.Find<Users>(u => u.Username == username).FirstOrDefault();
                if (user != null)
                    return user;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Users> GetAllUsers()
        {
            try
            {
                return users.Find<Users>(_ => true).ToList();
            }
            catch
            {
                return null;
            }
        }

        public void InsertUser(Users user)
        {
            try
            {
                users.InsertOne(user);
            }
            catch
            {
                throw new Exception("Couldn't create user.");
            }
        }

        public void UpdateUser(Users user)
        {
            try
            {
                users.ReplaceOne<Users>(u => u.Username == user.Username, user);
            }
            catch
            {
                throw new Exception("Couldn't update user.");
            }
        }

        public bool ResetPassword(Users user)
        {
            var filter = Builders<Users>.Filter.Eq(t => t.Username, user.Username);
            var update = Builders<Users>.Update.Set(t => t.Password, new PasswordHasher<Users>().HashPassword(user, user.Password));
            var options = new UpdateOptions { IsUpsert = false };
            UpdateResult updateResult = users.UpdateOne(filter, update, options);
            if (updateResult.MatchedCount > 0)
                return true;
            return false;
        }

        public IEnumerable<Users> GetByUsername(string username)
        {
            try
            {
                return users.Find<Users>(u => u.Username.Contains(username)).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
