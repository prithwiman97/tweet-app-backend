using com.tweetapp.Models;
using com.tweetapp.Repositories;
using com.tweetapp.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.tweetapp.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersRepository users;
        public UsersController(UsersRepository userRepo)
        {
            users = userRepo;
        }
        // GET: api/<UsersController>/all
        [HttpGet("all")]
        public ObjectResult All()
        {
            return Ok(users.GetAllUsers());
        }

        // GET api/v1.0/<UsersController>/{username}
        [HttpGet("{username}")]
        public ObjectResult Get(string username)
        {
            return Ok(users.GetByUsername(username));
        }

        // POST api/v1.0/<UsersController>/register
        [HttpPost("register")]
        public ObjectResult Register([FromBody] Users user)
        {
            if(!user.ValidateRequired())
                return StatusCode(400, new { msg = "Firstname, username, password are required fields." });
            if (!Validation.Username(user.Username))
                return StatusCode(400, new { msg = "Username must be within 8 - 20 characters.\nCan contain only alpha-numeric characters." });
            if (!Validation.Password(user.Password))
                return StatusCode(400, new { msg = Validation.PasswordErrorMessage() });
            if (!Validation.ValidateGender(user.Gender))
                return StatusCode(400, new { msg = "Gender can be M - Male, F - Female, O - Other" });
            user.Password = new PasswordHasher<Users>().HashPassword(user, user.Password);
            users.InsertUser(user);
            return StatusCode(201, new { msg = "User created successfully" });
        }

        // POST api/v1.0/<UsersController>/login
        [HttpPost("login")]
        public ObjectResult Login([FromBody] Users user)
        {
            Users authUser = users.GetUser(user.Username);
            if(authUser != null)
            {
                if (new PasswordHasher<Users>().VerifyHashedPassword(user, authUser.Password, user.Password) == PasswordVerificationResult.Success)
                    return Ok(new { loggedIn = true, msg = "Authentication Successful." });
                return StatusCode(401, new { loggedIn = false, msg = "Password is incorrect." });
            }
            return StatusCode(404,new { loggedIn = false, msg = $"User with username {user.Username} doesn't exist." });
        }

        // GET api/v1.0/<UsersController>/forgot
        [HttpPost("forgot")]
        public ObjectResult ForgotPassword([FromBody] Users user)
        {
            try
            {
                if (!Validation.Password(user.Password))
                    return StatusCode(400, new { msg = Validation.PasswordErrorMessage() });
                bool reset = users.ResetPassword(user);
                if (reset)
                    return Ok(new { msg = "Password reset successful." });
                return StatusCode(404, new { msg = "Couldn't reset password." });
                
            }
            catch (Exception err)
            {
                return StatusCode(500, new { msg = "Something went wrong.", error = err });
            }
        }


    }
}
