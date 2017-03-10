using System.Collections.Generic;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Tags;
using Kralizek.Assembla.Connector.Tickets.Associations;
using Kralizek.Assembla.Connector.Tickets.Comments;
using Kralizek.Assembla.Connector.Tickets.CustomFields;
using Kralizek.Assembla.Connector.Tickets.CustomReports;
using Kralizek.Assembla.Connector.Tickets.Statuses;

namespace Kralizek.Assembla.Connector.Tickets
{
    public interface ITicketConnector
    {
        Task<IReadOnlyList<Ticket>> GetAsync(string spaceIdOrWikiName, Report? report = null, int? page = null, int? pageSize = null, TicketSortExpression? sortExpression = null, SortOrder? sort = null);

        Task<IReadOnlyList<Ticket>> GetCurrentUserActiveAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Ticket>> GetCurrentUserFollowedAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Ticket>> GetInMilestoneAsync(string spaceIdOrWikiName, string milestoneId, int? page = null, int? pageSize = null, TicketSortExpression? sortExpression = null, SortOrder? sort = null, TicketStatusFilter? ticketStatusFilter = null);

        Task<IReadOnlyList<Ticket>> GetInNoMilestoneAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null);

        Task<IReadOnlyList<Ticket>> GetByTagAsync(string spaceIdOrWikiName, int tagId, int? page = null, int? pageSize = null);

        Task<IReadOnlyList<CustomReport>> GetAllCustomReportAsync(string spaceIdOrWikiName);

        Task<Ticket> GetByNumberAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<Ticket> GetByIdAsync(string spaceIdOrWikiName, string ticketId);

        Task<Ticket> CreateAsync(string spaceIdOrWikiName, Ticket newTicket);

        Task UpdateAsync(string spaceIdOrWikiName, Ticket ticket);

        Task DeleteAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<IReadOnlyList<File>> GetTicketAttachmentsAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<IReadOnlyList<Tag>> GetTicketTagsAsync(string spaceIdOrWikiName, int ticketNumber);

        ICustomFieldConnector CustomFields { get; }

        ITicketStatusConnector Statuses { get; }

        ITicketCommentConnector Comments { get; }

        ITicketAssociationConnector Associations { get; }
    }
}