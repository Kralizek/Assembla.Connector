using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.Statuses
{
    public class TicketStatusRequest
    {
        [JsonProperty("status")]
        public TicketStatus Status { get; }

        public TicketStatusRequest(TicketStatus status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            Status = status;
        }
    }
}