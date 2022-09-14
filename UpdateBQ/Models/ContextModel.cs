using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace UpdateBQ.Models
{
    [BsonDiscriminator("context")]
    [BsonIgnoreExtraElements]
    public class ContextModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("correlationId")]
        public string CorrelationId { get; set; }
        [BsonElement("created")]
        public DateTime Created { get; set; }
        [BsonElement("properties")]
        public IDictionary<string, string> Properties { get; set; }
    }
}
