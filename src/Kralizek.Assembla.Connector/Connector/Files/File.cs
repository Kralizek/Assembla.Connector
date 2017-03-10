using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kralizek.Assembla.Connector.Files
{
    public class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attachable_type")]
        public AttachableType? AttachableType { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }

        [JsonProperty("attachable_guid")]
        public string AttachableGuid { get; set; }

        [JsonProperty("attachable_id")]
        public string AttachableId { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("cached_tag_list")]
        public string CachedTagList { get; set; }

        [JsonProperty("filesize")]
        public int Size { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("has_thumbnail")]
        public bool HasThumbnail { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        public bool IsAttachedToTicket() => AttachableType.HasValue && AttachableType.Value == Files.AttachableType.Ticket && AttachableId != null;

        public bool IsAttachedToMessage() => AttachableType.HasValue && AttachableType.Value == Files.AttachableType.Flow && AttachableId != null;

        public bool IsAttachedToMilestone() => AttachableType.HasValue && AttachableType.Value == Files.AttachableType.Milestone && AttachableId != null;

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AttachableType
    {
        [EnumMember(Value = "Ticket")] Ticket = 1,
        [EnumMember(Value = "Flow")] Flow = 2,
        [EnumMember(Value = "Milestone")] Milestone = 3
    }
}
