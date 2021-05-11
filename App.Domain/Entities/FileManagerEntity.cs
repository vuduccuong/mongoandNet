using App.Domain.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Domain.Entities
{
    [BsonCollection("file_manager")]
    [BsonIgnoreExtraElements]
    public class FileManagerEntity : Document
    {
        [BsonElement("path")]
        public string Path { get; set; }
    }
}