using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kralizek.Assembla.Connector.Users
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InstantMessagingAccountType
    {
        [EnumMember(Value = "Skype")] Skype,
        [EnumMember(Value = "Google Talk")] GoogleTalk,
        [EnumMember(Value = "Jabber")] Jabber,
        [EnumMember(Value = "Yahoo")] Yahoo,
        [EnumMember(Value = "MSN")] Msn,
        [EnumMember(Value = "ICQ")] Icq,
        [EnumMember(Value = "AIM")] Aim,
    }
}