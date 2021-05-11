using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Domain.Base
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; set; }
    }

    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
    }
}