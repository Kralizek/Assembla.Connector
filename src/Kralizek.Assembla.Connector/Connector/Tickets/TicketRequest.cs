using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets
{
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