using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assembla.Documents;
using Assembla.Tags;
using Assembla.Tickets;
using Assembla.Tickets.CustomFields;
using Assembla.Tickets.CustomReports;
using Assembla.Tickets.Statuses;

namespace Assembla
{
    public partial class HttpAssemblaClient : ITicketConnector
    {
        private IReadOnlyDictionary<string, string> GetTicketQueryParameters(Report? report = null, int? page = null, int? pageSize = null, TicketSortExpression? sortExpression = null, SortOrder? sort = null, TicketStatusFilter? ticketStatus = null)
        {
            var queryParameters = new Dictionary<string, string>();

            if (report.HasValue)
            {
                queryParameters.Add("report", report.Value.Id);
            }
            if (page.HasValue)
            {
                queryParameters.Add("page", page.Value.ToString("D"));
            }
            if (pageSize.HasValue)
            {
                queryParameters.Add("per_page", pageSize.Value.ToString("D"));
            }
            if (sortExpression.HasValue)
            {
                queryParameters.Add("sort_by", sortExpression.Value.Expression);
            }
            if (sort.HasValue)
            {
                queryParameters.Add("sort_order", sort.Value.Direction);
            }
            if (ticketStatus.HasValue)
            {
                queryParameters.Add("ticket_status", ticketStatus.Value.Status);
            }
            return queryParameters;
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetAsync(string spaceIdOrWikiName, Report? report, int? page, int? pageSize, TicketSortExpression? sortExpression, SortOrder? sort)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetTicketQueryParameters(report, page, pageSize, sortExpression, sort);

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets";

            var tickets = await _client.GetAsync<Ticket[]>(url, queryParameters).ConfigureAwait(false);

            return tickets;
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetCurrentUserActiveAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/my_active";

            var tickets = await _client.GetAsync<Ticket[]>(url).ConfigureAwait(false);

            return tickets;
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetCurrentUserFollowedAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/my_followed";

            var tickets = await _client.GetAsync<Ticket[]>(url).ConfigureAwait(false);

            return tickets;
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetInMilestoneAsync(string spaceIdOrWikiName, string milestoneId, int? page, int? pageSize, TicketSortExpression? sortExpression, SortOrder? sort, TicketStatusFilter? ticketStatusFilter)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (milestoneId == null)
            {
                throw new ArgumentNullException(nameof(milestoneId));
            }

            var queryParameters = GetTicketQueryParameters(page: page, pageSize: pageSize, sortExpression: sortExpression, sort: sort, ticketStatus: ticketStatusFilter);

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/milestone/{milestoneId}";

            var tickets = await _client.GetAsync<Ticket[]>(url, queryParameters).ConfigureAwait(false);

            return tickets;
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetInNoMilestoneAsync(string spaceIdOrWikiName, int? page, int? pageSize)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetTicketQueryParameters(page: page, pageSize: pageSize);

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/no_milestone";

            var tickets = await _client.GetAsync<Ticket[]>(url, queryParameters).ConfigureAwait(false);

            return tickets;
        }

        async Task<IReadOnlyList<CustomReport>> ITicketConnector.GetAllCustomReportAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_reports";

            var reports = await _client.GetAsync<CustomReportList>(url).ConfigureAwait(false);

            var result = new List<CustomReport>();
            result.AddRange(reports.TeamReports.Select(c => new CustomReport{Id = c.Id, Title = c.Title, ReportType = CustomReportType.TeamReport}));
            result.AddRange(reports.UserReports.Select(c => new CustomReport { Id = c.Id, Title = c.Title, ReportType = CustomReportType.UserReport }));

            return result;
        }

        async Task<Ticket> ITicketConnector.GetByNumberAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}";

            var ticket = await _client.GetAsync<Ticket>(url).ConfigureAwait(false);

            return ticket;
        }

        async Task<Ticket> ITicketConnector.GetByIdAsync(string spaceIdOrWikiName, string ticketId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticketId == null)
            {
                throw new ArgumentNullException(nameof(ticketId));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/id/{ticketId}";

            var ticket = await _client.GetAsync<Ticket>(url).ConfigureAwait(false);

            return ticket;
        }

        async Task<Ticket> ITicketConnector.CreateAsync(string spaceIdOrWikiName, Ticket newTicket)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (newTicket == null)
            {
                throw new ArgumentNullException(nameof(newTicket));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tickets";

            var createdTicket = await _client.PostAsync<TicketRequest, Ticket>(uri, new TicketRequest(newTicket)).ConfigureAwait(false);

            return createdTicket;
        }

        async Task ITicketConnector.UpdateAsync(string spaceIdOrWikiName, Ticket ticket)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            if (ticket.Number == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticket.Number));
            }

            await _client.PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticket.Number}", new TicketRequest(ticket)).ConfigureAwait(false);
        }

        async Task ITicketConnector.DeleteAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}").ConfigureAwait(false);
        }

        async Task<IReadOnlyList<Document>> ITicketConnector.GetTicketAttachmentsAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            var attachments = await _client.GetAsync<Document[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/attachments").ConfigureAwait(false);

            return attachments;
        }

        async Task<IReadOnlyList<Tag>> ITicketConnector.GetTicketTagsAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            var tags = await _client.GetAsync<Tag[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/tags").ConfigureAwait(false);

            return tags;
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetByTagAsync(string spaceIdOrWikiName, int tagId, int? page, int? pageSize)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (tagId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tagId));
            }

            var queryParameters = GetTicketQueryParameters(page: page, pageSize: pageSize);

            var tickets = await _client.GetAsync<Ticket[]>($"/v1/spaces/{spaceIdOrWikiName}/tags/{tagId}/tickets", queryParameters).ConfigureAwait(false);

            return tickets;
        }

        ICustomFieldConnector ITicketConnector.CustomFields => this;

        IStatusConnector ITicketConnector.Statuses => this;
    }

    public partial class HttpAssemblaClient : IStatusConnector
    {
        async Task<IReadOnlyList<TicketStatus>> IStatusConnector.GetAllAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var statuses = await _client.GetAsync<TicketStatus[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses").ConfigureAwait(false);

            return statuses;
        }

        async Task<TicketStatus> IStatusConnector.GetAsync(string spaceIdOrWikiName, string statusId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (statusId == null)
            {
                throw new ArgumentNullException(nameof(statusId));
            }

            var status = await _client.GetAsync<TicketStatus>($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses/{statusId}").ConfigureAwait(false);

            return status;
        }

        async Task<TicketStatus> IStatusConnector.CreateAsync(string spaceIdOrWikiName, TicketStatus status)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            var createdStatus = await _client.PostAsync<TicketStatusRequest, TicketStatus>($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses", new TicketStatusRequest(status)).ConfigureAwait(false);

            return createdStatus;
        }

        async Task IStatusConnector.UpdateAsync(string spaceIdOrWikiName, TicketStatus status)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            if (status.Id == null)
            {
                throw new ArgumentNullException(nameof(status.Id));
            }

            await _client.PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses/{status.Id}", new TicketStatusRequest(status)).ConfigureAwait(false);
        }

        async Task IStatusConnector.DeleteAsync(string spaceIdOrWikiName, string statusId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (statusId == null)
            {
                throw new ArgumentNullException(nameof(statusId));
            }

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses/{statusId}").ConfigureAwait(false);
        }
    }

    public partial class HttpAssemblaClient : ICustomFieldConnector
    {
        async Task<IReadOnlyList<CustomField>> ICustomFieldConnector.GetAllAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var fields = await _client.GetAsync<CustomField[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields").ConfigureAwait(false);

            return fields;
        }

        async Task<CustomField> ICustomFieldConnector.GetAsync(string spaceIdOrWikiName, string customFieldId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (customFieldId == null)
            {
                throw new ArgumentNullException(nameof(customFieldId));
            }
            
            var field = await _client.GetAsync<CustomField>($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields/{customFieldId}").ConfigureAwait(false);

            return field;
        }

        async Task<CustomField> ICustomFieldConnector.CreateAsync(string spaceIdOrWikiName, CustomField customField)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (customField == null)
            {
                throw new ArgumentNullException(nameof(customField));
            }

            var createdField = await _client.PostAsync<CustomFieldRequest, CustomField>($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields", new CustomFieldRequest(customField)).ConfigureAwait(false);

            return createdField;
        }

        async Task ICustomFieldConnector.UpdateAsync(string spaceIdOrWikiName, CustomField customField)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (customField == null)
            {
                throw new ArgumentNullException(nameof(customField));
            }
            if (customField.Id == null)
            {
                throw new ArgumentNullException(nameof(customField.Id));
            }

            await _client.PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields/{customField.Id}", customField).ConfigureAwait(false);
        }

        async Task ICustomFieldConnector.DeleteAsync(string spaceIdOrWikiName, string customFieldId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            if (customFieldId == null)
            {
                throw new ArgumentNullException(nameof(customFieldId));
            }

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields/{customFieldId}").ConfigureAwait(false);
        }
    }
}
