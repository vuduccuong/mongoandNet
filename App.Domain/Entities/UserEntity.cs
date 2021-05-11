using App.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Domain.Entities
{
    [BsonCollection("users")]
    [BsonIgnoreExtraElements]
    public class UserEntity : Document
    {
        [BsonElement("user_name")]
        public string UserName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }
    }
}