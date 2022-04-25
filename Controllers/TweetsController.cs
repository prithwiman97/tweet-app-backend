using com.tweetapp.Repositories;
using com.tweetapp.Interfaces;
using com.tweetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.tweetapp.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private TweetRepository tweets;
        public TweetsController(TweetRepository tweetRepo)
        {
            tweets = tweetRepo;
        }

        // GET: api/<TweetsController>
        [HttpGet("all")]
        public ObjectResult All()
        {
            return Ok(tweets.GetAllTweet());
        }

        // GET api/<TweetsController>/5
        [HttpGet("{username}")]
        public ObjectResult Get(string username)
        {
            return Ok(tweets.GetTweetsByUsername(username));
        }

        [HttpGet("details/{tweetId}")]
        public ObjectResult GetById(string tweetId)
        {
            return Ok(tweets.GetTweetById(tweetId));
        }
        // GET api/<TweetsController>/5
        [HttpGet("reply/{tweetId}")]
        public ObjectResult GetReplies(string tweetId)
        {
            return Ok(tweets.GetReplyList(tweetId));
        }

        // POST api/<TweetsController>
        [HttpPost("add")]
        public ObjectResult Post([FromBody] Tweet post)
        {
            try
            {
                tweets.InsertTweet(post);
                return StatusCode(201, new { msg = "Tweet created successfully.", id = post.Id });
                
            }
            catch (Exception err)
            {
                return StatusCode(500, new { msg = "Something went wrong.", error = err });
            }
        }

        [HttpPost("reply")]
        public ObjectResult Reply([FromBody] Reply reply)
        {
            try
            {
                tweets.ReplyToTweet(reply);
                return StatusCode(201, new { msg = "Reply added successfully." });
            }
            catch (Exception err)
            {
                return StatusCode(500, new { msg = "Something went wrong.", error = err });
            }
        }

        // PUT api/<TweetsController>/update/{id}
        [HttpPut("update/{id}")]
        public ObjectResult Update(string id, [FromBody] Tweet post)
        {
            try
            {
                post.Id = id;
                tweets.UpdateTweet(post);
                return Ok(new { msg = "Tweet updated successfully" });
            }
            catch(Exception err)
            {
                return StatusCode(500, new { msg = "Something went wrong", error = err });
            }
        }


        [HttpPut("like/{id}")]
        public ObjectResult Like(string id, [FromBody] Tweet post)
        {
            try
            {
                post = tweets.GetTweetById(id);
                tweets.LikeTweet(post);
                return Ok(new { msg = "Tweet liked successfully" });
            }
            catch (Exception err)
            {
                return StatusCode(500, new { msg = "Something went wrong", error = err });
            }
        }

        // DELETE api/<TweetsController>/5
        [HttpDelete("delete/{id}")]
        public ObjectResult Delete(string id)
        {
            try
            {
                tweets.DeleteTweet(id);
                return Ok(new { msg = "Tweet deleted successfully." });
            }
            catch(Exception err)
            {
                return StatusCode(500, new { msg = "Something went wrong", error = err });
            }
        }
    }
}
