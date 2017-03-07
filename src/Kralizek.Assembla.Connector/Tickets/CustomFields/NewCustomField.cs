using Newtonsoft.Json;

namespace Assembla.Tickets.CustomFields
{
    public class NewCustomField
    {
        [JsonProperty("type")]
        public CustomFieldType Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("required")]
        public bool IsRequired { get; set; }

        [JsonProperty("hide")]
        public bool IsHidden { get; set; }

        [JsonProperty("default_value")]
        public string DefaultValue { get; set; }

        [JsonProperty("list_options")]
        public string[] ListOptions { get; set; }
    }
}