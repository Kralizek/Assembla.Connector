using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.Associations
{
    public class TicketAssociationRequest
    {
        [JsonProperty("ticket_association")]
        public TicketAssociation Association { get; }

        public TicketAssociationRequest(TicketAssociation association)
        {
            if (association == null)
            {
                throw new ArgumentNullException(nameof(association));
            }
            Association = association;
        }
    }
}