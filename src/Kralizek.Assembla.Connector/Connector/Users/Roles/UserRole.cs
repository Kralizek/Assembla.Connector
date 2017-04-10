using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kralizek.Assembla.Connector.Users.Roles
{
    public class UserRole
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        [JsonProperty("role")]
        public Role Role { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("invited_time")]
        public DateTimeOffset InvitedTime { get; set; }

        [JsonProperty("agreed_time")]
        public DateTimeOffset? AgreedTime { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("invited_by_id")]
        public string InvitedById { get; set; }

        [JsonProperty("message")]
        public string InvitationMessage { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        [EnumMember(Value = "watcher")]Watcher = 1,
        [EnumMember(Value = "member")]Member = 2,
        [EnumMember(Value = "owner")] Owner = 3
    }

    public enum Status
    {
        Invited = 1,
        Accepted = 2,
        Removed = 3
    }
}
