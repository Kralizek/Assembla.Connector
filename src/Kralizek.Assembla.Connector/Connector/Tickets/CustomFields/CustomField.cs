using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.CustomFields
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
        [JsonConverter(typeof(ListStringJsonConverter))]
        public string[] ListOptions { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("without_default_on_form")]
        public bool DefaultValueNotRequired => string.IsNullOrEmpty(DefaultValue);
    }
}
