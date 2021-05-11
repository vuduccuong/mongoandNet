using App.Domain.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Domain.Entities
{
    [BsonCollection("shipwrecks")]
    [BsonIgnoreExtraElements]
    public class ShipwreckEntity : Document
    {
        [BsonElement("feature_type")]
        public string FeautureType { get; set; }

        [BsonElement("chart")]
        public string Chart { get; set; }

        [BsonElement("latdec")]
        public double Latitude { get; set; }

        [BsonElement("londec")]
        public double Longitude { get; set; }
    }
}