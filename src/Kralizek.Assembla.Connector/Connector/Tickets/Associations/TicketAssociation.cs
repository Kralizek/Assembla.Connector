using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.Associations
{
    public class TicketAssociation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ticket1_id")]
        public string FirstTicketId { get; set; }

        [JsonProperty("ticket2_id")]
        public string SecondTicketId { get; set; }

        [JsonProperty("relationship")]
        public TicketRelationship? Relationship { get; set; }

        public string Explain()
        {
            switch (Relationship)
            {
                case TicketRelationship.Parent:
                    return $"#{SecondTicketId} is parent of #{FirstTicketId} and #{FirstTicketId} is child of #{SecondTicketId}";
                case TicketRelationship.Child:
                    return $"#{SecondTicketId} is child of #{FirstTicketId} and #{FirstTicketId} is parent of #{SecondTicketId}";
                case TicketRelationship.Related:
                    return $"#{SecondTicketId} is related to #{FirstTicketId}";
                case TicketRelationship.Duplicate:
                    return $"#{SecondTicketId} is duplication of #{FirstTicketId}";
                case TicketRelationship.Sibling:
                    return $"#{SecondTicketId} is sibling of #{FirstTicketId}";
                case TicketRelationship.Story:
                    return $"#{SecondTicketId} is story and #{FirstTicketId} is subtask of the story";
                case TicketRelationship.Subtask:
                    return $"#{SecondTicketId} is subtask of a story and #{FirstTicketId} is the story";
                case TicketRelationship.Dependent:
                    return $"#{SecondTicketId} depends on #{FirstTicketId}";
                case TicketRelationship.Block:
                    return $"#{SecondTicketId} blocks #{FirstTicketId}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(Relationship));
            }
        }
    }

    public enum TicketRelationship
    {
        Parent = 0,
        Child = 1,
        Related = 2,
        Duplicate = 3,
        Sibling = 4,
        Story = 5,
        Subtask = 6,
        Dependent = 7,
        Block = 8
    }

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