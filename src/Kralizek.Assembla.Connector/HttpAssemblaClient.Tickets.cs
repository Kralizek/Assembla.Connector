using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Documents;
using Assembla.Tags;
using Assembla.Tickets;
using Assembla.Tickets.CustomFields;

namespace Assembla
{
    public partial class HttpAssemblaClient : ITicketConnector
    {
        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetAsync(string spaceIdOrWikiName, Report? report, int? page, int? pageSize, TicketSortExpression? sortExpression, SortOrder? sort)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetUserActiveAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetUserFollowedAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetInMilestoneAsync(string spaceIdOrWikiName, string milestoneId)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetInNoMilestoneAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<CustomReport>> ITicketConnector.GetSpaceCustomReportAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<Ticket> ITicketConnector.GetByNumberAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            throw new NotImplementedException();
        }

        async Task<Ticket> ITicketConnector.GetByIdAsync(string spaceIdOrWikiName, string ticketId)
        {
            throw new NotImplementedException();
        }

        async Task<Ticket> ITicketConnector.CreateAsync(string spaceIdOrWikiName, NewTicket newTicket)
        {
            throw new NotImplementedException();
        }

        async Task ITicketConnector.UpdateAsync(string spaceIdOrWikiName, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        async Task ITicketConnector.DeleteAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Document>> ITicketConnector.GetTicketDocumentsAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Tag>> ITicketConnector.GetTicketTagsAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetByTagAsync(string spaceIdOrWikiName, int tagId)
        {
            throw new NotImplementedException();
        }

        ICustomFieldConnector ITicketConnector.CustomFields => this;
    }

    public partial class HttpAssemblaClient : ICustomFieldConnector
    {
        async Task<IReadOnlyList<CustomField>> ICustomFieldConnector.GetAllAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<CustomField> ICustomFieldConnector.GetAsync(string spaceIdOrWikiName, string customFieldId)
        {
            throw new NotImplementedException();
        }

        async Task<CustomField> ICustomFieldConnector.CreateAsync(string spaceIdOrWikiName, NewCustomField newCustomField)
        {
            throw new NotImplementedException();
        }

        async Task ICustomFieldConnector.UpdateAsync(string spaceIdOrWikiName, CustomField customField)
        {
            throw new NotImplementedException();
        }

        async Task ICustomFieldConnector.DeleteAsync(string spaceIdOrWikiName, string customFieldId)
        {
            throw new NotImplementedException();
        }
    }

}
