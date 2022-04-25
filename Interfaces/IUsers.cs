using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace com.tweetapp.Interfaces
{
    public interface IUsers
    {
        public void InsertUser(Users users);
        public Users GetUser(string username);
        public IEnumerable<Users> GetAllUsers();
        public void UpdateUser(Users users);
        public bool ResetPassword(Users user);
        public IEnumerable<Users> GetByUsername(string username);
    }
}
