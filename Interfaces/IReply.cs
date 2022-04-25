using com.tweetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetapp.Interfaces
{
    public interface IReply
    {
        public void ReplyToTweet(Reply reply);
        public IEnumerable<Reply> GetReplyList(string tweetId);
    }
}
