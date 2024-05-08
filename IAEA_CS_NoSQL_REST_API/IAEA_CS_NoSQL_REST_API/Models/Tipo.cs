using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IAEA_CS_NoSQL_REST_API.Models
{
    public class Tipo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("tipo_nombre")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("tipo_nombre")]
        public string? Tipo_nombre { get; set; } = string.Empty;

    }
}
