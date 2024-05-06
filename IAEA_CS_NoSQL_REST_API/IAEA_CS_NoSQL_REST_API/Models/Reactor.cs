using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IAEA_CS_NoSQL_REST_API.Models
{
    public class Reactor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;

        [BsonElement("nombre")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = string.Empty;

        [BsonElement("potencia")]
        [BsonRepresentation(BsonType.Double)]
        [JsonPropertyName("potencia")]
        public double? Potencia { get; set; } = 0;

        [BsonElement("estado")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("estado")]
        public string? Estado { get; set; } = string.Empty;

        [BsonElement("fecha")]
        [BsonRepresentation(BsonType.DateTime)]
        [JsonPropertyName("fecha")]
        public DateTime? Fecha { get; set; } = new DateTime(2000, 1, 1);

        [BsonElement("tipo")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("tipo")]
        public string? Tipo { get; set; } = string.Empty;

        [BsonElement("pais")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("pais")]
        public string? Pais { get; set; } = string.Empty;

        [BsonElement("ciudad")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("ciudad")]
        public string? Ciudad { get; set; } = string.Empty;
    }
}