using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace car_management_backend.Utilities.Parser
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat;

        public DateTimeConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            return DateTime.ParseExact(dateString, _dateFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}
