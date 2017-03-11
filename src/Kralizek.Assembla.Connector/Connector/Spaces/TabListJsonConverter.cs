using System;
using System.Linq;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Spaces
{
    public class TabListJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType != typeof(string) && objectType != typeof(string[]))
            {
                throw new InvalidOperationException("The value can't be converted");
            }

            string value = (string)reader.Value;

            if (string.IsNullOrEmpty(value))
            {
                return new string[0];
            }

            var rows = value.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(s => s.Substring(2)).ToArray();

            return rows;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string[]);
        }
    }
}