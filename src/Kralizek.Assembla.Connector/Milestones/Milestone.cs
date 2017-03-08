using System;
using Newtonsoft.Json;

namespace Assembla.Milestones
{
    public class Milestone
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("due_date")]
        public DateTimeOffset? DueDate { get; set; }

        [JsonProperty("budget")]
        public decimal? Budget { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user_id")]
        public string ResponsibleUser { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty("completed_date")]
        public DateTimeOffset? CompletedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; }

        [JsonProperty("release_level")]
        public ReleaseLevel? ReleaseLevel { get; set; }

        [JsonProperty("release_notes")]
        public string ReleaseNotes { get; set; }

        [JsonProperty("planner_type")]
        public PlannerType? PlannerType { get; set; }
    }
}
