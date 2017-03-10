using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kralizek.Assembla.Connector.Users
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("picture")]
        public Uri Picture { get; set; }

        [JsonProperty("im")]
        public InstantMessagingAccount InstantMessagingAccount1 { get; set; }

        [JsonProperty("im2")]
        public InstantMessagingAccount InstantMessagingAccount2 { get; set; }
    }

    public class InstantMessagingAccount
    {
        [JsonProperty("type")]
        public InstantMessagingAccountType Type { get; set; }

        [JsonProperty("id")]
        public string Account { get; set; }
    }

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