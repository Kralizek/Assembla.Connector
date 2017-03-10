using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.Comments
{
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