namespace com.tweetapp.Interfaces
{
    public interface ITweetDatabaseSettings
    {
        public string TweetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}