using System;
using System.Text;
using Newtonsoft.Json;

namespace Assembla.Tickets.CustomFields
{
    public class CustomField
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("space_tool_id")]
        public string SpaceToolId { get; set; }

        [JsonProperty("type")]
        public CustomFieldType Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("order")]
        public int? Order { get; set; }

        [JsonProperty("required")]
        public bool IsRequired { get; set; }

        [JsonProperty("hide")]
        public bool IsHidden { get; set; }

        [JsonProperty("default_value")]
        public string DefaultValue { get; set; }

        [JsonProperty("list_options")]
        public string[] ListOptions { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
