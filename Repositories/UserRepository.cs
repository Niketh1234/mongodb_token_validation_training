using MongoDB.Driver;
using MongoDBDemo.Models;

namespace MongoDBDemo.Repositories
{
    public class UserRepository
    {
        IMongoDatabase bmv;
        IMongoClient bmvClient;
        public UserRepository()
        {
            bmvClient = new MongoClient("mongodb://localhost:27017/");
            bmv = bmvClient.GetDatabase("users");
        }
        public IMongoCollection<User> Users => bmv.GetCollection<User>("users");
        public bool AddUser(User user)
        {
            Users.InsertOne(user);
            return true;
        }
        public List<User> GetUsers()
        {
            return Users.Find(u=>true).ToList();
        }
        public bool UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id ,user.Id);
            var update = Builders<User>.Update.Set(u => u.password, user.password);
            var result = Users.UpdateOne(filter, update);
            return result.IsAcknowledged && result.ModifiedCount == 1;
        }
        public bool DeleteUser(int id) {
            var result =  Users.DeleteOne(u => u.Id ==id);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }
        public bool Validate(User user)
        {
            var result = Users.Find(u => u.email == user.email && u.password == user.password);
            if (result != null)
                return result.CountDocuments() > 0;
            return false;
        }

    }
}
