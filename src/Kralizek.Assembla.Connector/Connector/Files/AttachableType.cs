using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kralizek.Assembla.Connector.Files
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AttachableType
    {
        [EnumMember(Value = "Ticket")] Ticket = 1,
        [EnumMember(Value = "Flow")] Flow = 2,
        [EnumMember(Value = "Milestone")] Milestone = 3
    }
}