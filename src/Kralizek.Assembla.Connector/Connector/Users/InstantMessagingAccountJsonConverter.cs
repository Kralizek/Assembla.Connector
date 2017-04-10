using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kralizek.Assembla.Connector.Users
{
    public class InstantMessagingAccountJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            if (string.IsNullOrEmpty(jObject["type"].Value<string>()))
                return null;

            var account = new InstantMessagingAccount();
            serializer.Populate(jObject.CreateReader(), account);

            return account;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InstantMessagingAccount);
        }
    }
}