using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Milestones
{
    public class NewMilestone
    {
        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("due_date")]
        public DateTimeOffset DueDate { get; set; }

        [JsonProperty("budget")]
        public decimal Budget { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user_id")]
        public string ResponsibleUser { get; set; }

        [JsonProperty("release_level")]
        public ReleaseLevel ReleaseLevel { get; set; }

        [JsonProperty("release_notes")]
        public string ReleaseNotes { get; set; }
    }
}
