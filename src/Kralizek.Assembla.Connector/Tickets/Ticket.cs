using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assembla.Tickets
{
    public class Ticket
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("priority")]
        public TicketPriority Priority { get; set; }

        [JsonProperty("completed_date")]
        public DateTimeOffset? CompletedDate { get; set; }

        [JsonProperty("created_on")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonProperty("permission_type")]
        public PermissionType PermissionType { get; set; }

        [JsonProperty("importance")]
        public float Importance { get; set; }

        [JsonProperty("is_story")]
        public bool IsStory { get; set; }

        [JsonProperty("milestone_id")]
        public string MilestoneId { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("followers")]
        public string[] Followers { get; set; }

        [JsonProperty("notification_list")]
        public string NotificationList { get; set; }

        [JsonProperty("space_id")]
        public string SpaceId { get; set; }

        [JsonProperty("state")]
        public TicketState State { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("story_importance")]
        public int StoryImportance { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("working_hours")]
        public float WorkingHours { get; set; }

        [JsonProperty("estimate")]
        public float Estimate { get; set; }

        [JsonProperty("total_estimate")]
        public float TotalEstimate { get; set; }

        [JsonProperty("total_invested_hours")]
        public float TotalInvestedHours { get; set; }

        [JsonProperty("total_working_hours")]
        public float TotalWorkingHours { get; set; }

        [JsonProperty("assigned_to_id")]
        public string AssignedToUser { get; set; }

        [JsonProperty("reporter_id")]
        public string Reporter { get; set; }

        [JsonProperty("custom_fields")]
        public IDictionary<string, string> CustomFields { get; set; }

        [JsonProperty("hierarchy_type")]
        public HierarchyType HierarchyType { get; set; }

        [JsonProperty("is_support")]
        public bool? IsSupport { get; set; }
    }

    public enum TicketPriority
    {
        Highest = 1,
        High = 2,
        Normal = 3,
        Low = 4,
        Lowest = 5
    }

    public enum PermissionType
    {
        Development = 0,
        PrivateSupport = 1,
        PublicSupport = 2
    }

    public enum TicketState
    {
        Closed = 0,
        Open = 1
    }

    public enum HierarchyType
    {
        None = 0,
        SubTask = 1,
        Story = 2,
        Epic = 3
    }

    public class TicketRequest
    {
        [JsonProperty("ticket")]
        public Ticket Ticket { get; }

        public TicketRequest(Ticket ticket)
        {
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
        }
    }
}