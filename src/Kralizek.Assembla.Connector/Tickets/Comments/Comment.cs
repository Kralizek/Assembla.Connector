using System;
using Newtonsoft.Json;

namespace Assembla.Tickets.Comments
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("comment")]
        public string Text { get; set; }

        [JsonProperty("ticket_id")]
        public int TicketId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("created_on")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("ticket_changes")]
        public string TicketChanges { get; set; }


    }

    public class CommentRequest
    {
        [JsonProperty("ticket_comment")]
        public Comment Comment { get; }

        public CommentRequest(Comment comment)
        {
            Comment = comment;
        }
    }
}