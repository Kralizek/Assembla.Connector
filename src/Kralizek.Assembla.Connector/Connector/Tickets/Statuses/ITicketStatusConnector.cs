using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Tickets.Statuses
{
    public interface ITicketStatusConnector
    {
        Task<IReadOnlyList<TicketStatus>> GetAllAsync(string spaceIdOrWikiName);

        Task<TicketStatus> GetAsync(string spaceIdOrWikiName, string statusId);

        Task<TicketStatus> CreateAsync(string spaceIdOrWikiName, TicketStatus status);

        Task UpdateAsync(string spaceIdOrWikiName, TicketStatus status);

        Task DeleteAsync(string spaceIdOrWikiName, string statusId);
    }
}