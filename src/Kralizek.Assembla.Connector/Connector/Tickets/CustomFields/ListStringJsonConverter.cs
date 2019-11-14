using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.CustomFields
{
    public class ListStringJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (!(value is string[]))
            {
                throw new InvalidOperationException($"The value cannot be converted");
            }
            var list = (string[]) value;

            writer.WriteValue(string.Join(",", list));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string[]);
        }
    }
}