using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace UpdateBQ.Data.Dtos
{
    internal class UpdateBigQueryDto
    {
        public string Id { get; set; }
        [BsonElement("properties")]
        public IDictionary<string, string> Properties { get; set; }
    }
}
