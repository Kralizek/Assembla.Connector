using System;
using Newtonsoft.Json;

namespace Assembla.Tickets.Statuses
{
    public class TicketStatus
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("space_tool_id")]
        public string SpaceToolId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public TicketState? State { get; set; }

        [JsonProperty("list_order")]
        public int Order { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }

    
}