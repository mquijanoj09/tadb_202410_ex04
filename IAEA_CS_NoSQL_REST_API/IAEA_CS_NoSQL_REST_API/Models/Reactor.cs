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

        [BsonElement("tipo_id")]
        [BsonRepresentation(BsonType.Int32)]
        [JsonPropertyName("tipo_id")]
        public int? Tipo_id { get; set; } = 0;

        [BsonElement("ciudad_id")]
        [BsonRepresentation(BsonType.Int32)]
        [JsonPropertyName("ciudad_id")]
        public int? Ciudad_id { get; set; } = 0;
    }
}