using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBDemo.Models
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
