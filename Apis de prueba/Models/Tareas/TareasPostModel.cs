using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Apis_de_prueba.Models
{
    public class TareasPostModel
    {
        [Key]
        public int Codigotarea { get; set; }
        public string Mitarea { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringDateTimeConverter))]
        public DateTime Fechainicio { get; set; }

        public string Estado { get; set; } = string.Empty;

        [NotMapped]
        public int DiasActivos
        {
            get
            {
                return (DateTime.Now - Fechainicio).Days;
            }
        }

        public class JsonStringDateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    string? dateString = reader.GetString();
                    if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime result))
                    {
                        return result;
                    }
                    else if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result2))
                    {
                        return result2;
                    }
                }

                return DateTime.Now; 
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
            }
        }
    }
}