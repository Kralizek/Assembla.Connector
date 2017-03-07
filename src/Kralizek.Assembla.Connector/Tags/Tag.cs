using System;
using Newtonsoft.Json;

namespace Assembla.Tags
{
    public class Tag
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("state")]
        public TagState State { get; set; }

    }

    public enum TagState
    {
        Proposed = 1,
        Active = 2,
        Hidden = 4
    }
}
