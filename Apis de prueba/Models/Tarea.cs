using System.Text.Json;
using System.Text.Json.Serialization;

namespace Apis_de_prueba.Models
{
    public partial class Tarea
    {
        public int CodigoTarea { get; set; }
        public string MiTarea { get; set; } = null!;

        [JsonConverter(typeof(JsonStringDateTimeConverter))]
        public DateTime FechaInicio { get; set; } 

        public int? DiasActivos { get; set; }
        public string Estado { get; set; } = null!;
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