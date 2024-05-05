using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IAEA_CS_PoC_Consola
{
    public class Reactor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? ObjectId { get; set; }

        [BsonElement("nombre")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; } = String.Empty;

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
        public string? Tipo { get; set; } = String.Empty;

        [BsonElement("pais")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("pais")]
        public string? Pais { get; set; } = String.Empty;

        [BsonElement("cuidad")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("cuidad")]
        public string? Cuidad { get; set; } = String.Empty;
    }
}

