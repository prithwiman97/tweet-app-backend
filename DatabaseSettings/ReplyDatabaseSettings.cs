using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.tweetapp.Interfaces;

namespace com.tweetapp.DatabaseSettings
{
    public class ReplyDatabaseSettings : IReplyDatabaseSettings
    {
        public string ReplyCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
