using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IAEA_CS_NoSQL_REST_API.Models
{
    public class Ubicacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("ciudad")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("ciudad")]
        public string? Ciudad { get; set; } = string.Empty;

        [BsonElement("pais")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("pais")]
        public string? Pais { get; set; } = string.Empty;

    }
}
