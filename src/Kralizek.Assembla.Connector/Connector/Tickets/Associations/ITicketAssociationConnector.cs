using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Tickets.Associations
{
    public interface ITicketAssociationConnector
    {
        Task<IReadOnlyList<TicketAssociation>> GetAllOfTicketAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<TicketAssociation> GetAsync(string spaceIdOrWikiName, int ticketNumber, string associationId);

        Task<TicketAssociation> CreateAsync(string spaceIdOrWikiName, int ticketNumber, TicketAssociation association);

        Task UpdateAsync(string spaceIdOrWikiName, int ticketNumber, TicketAssociation association);

        Task DeleteAsync(string spaceIdOrWikiName, int ticketNumber, string associationId);
    }
}