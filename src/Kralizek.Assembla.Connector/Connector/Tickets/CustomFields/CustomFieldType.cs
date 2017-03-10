using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kralizek.Assembla.Connector.Tickets.CustomFields
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomFieldType
    {
        [EnumMember(Value = "Text")] Text,
        [EnumMember(Value = "Numeric")] Numeric,
        [EnumMember(Value = "Team List")]TeamList,
        [EnumMember(Value = "List")] List,
        [EnumMember(Value = "Date")] Date
    }
}