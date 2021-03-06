using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Tags;
using Kralizek.Assembla.Connector.Tickets;
using Kralizek.Assembla.Connector.Tickets.Associations;
using Kralizek.Assembla.Connector.Tickets.Comments;
using Kralizek.Assembla.Connector.Tickets.CustomFields;
using Kralizek.Assembla.Connector.Tickets.CustomReports;
using Kralizek.Assembla.Connector.Tickets.Statuses;

namespace Kralizek.Assembla.Connector
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

            var tickets = await GetJsonAsync<Ticket[]>(url, queryParameters).ConfigureAwait(false);

            return tickets ?? new Ticket[0];
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetCurrentUserActiveAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/my_active";

            var tickets = await GetJsonAsync<Ticket[]>(url).ConfigureAwait(false);

            return tickets ?? new Ticket[0];
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetCurrentUserFollowedAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/my_followed";

            var tickets = await GetJsonAsync<Ticket[]>(url).ConfigureAwait(false);

            return tickets ?? new Ticket[0];
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

            var tickets = await GetJsonAsync<Ticket[]>(url, queryParameters).ConfigureAwait(false);

            return tickets ?? new Ticket[0];
        }

        async Task<IReadOnlyList<Ticket>> ITicketConnector.GetInNoMilestoneAsync(string spaceIdOrWikiName, int? page, int? pageSize)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetTicketQueryParameters(page: page, pageSize: pageSize);

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/no_milestone";

            var tickets = await GetJsonAsync<Ticket[]>(url, queryParameters).ConfigureAwait(false);

            return tickets ?? new Ticket[0];
        }

        async Task<IReadOnlyList<CustomReport>> ITicketConnector.GetAllCustomReportAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var url = $"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_reports";

            var reports = await GetJsonAsync<CustomReportList>(url).ConfigureAwait(false);

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

            var ticket = await GetJsonAsync<Ticket>(url).ConfigureAwait(false);

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

            var ticket = await GetJsonAsync<Ticket>(url).ConfigureAwait(false);

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

            var createdTicket = await PostAsync<TicketRequest, Ticket>(uri, new TicketRequest(newTicket)).ConfigureAwait(false);

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

            await PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticket.Number}", new TicketRequest(ticket)).ConfigureAwait(false);
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

            await DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}").ConfigureAwait(false);
        }

        async Task<IReadOnlyList<File>> ITicketConnector.GetTicketAttachmentsAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            var attachments = await GetJsonAsync<File[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/attachments").ConfigureAwait(false);

            return attachments ?? new File[0];
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

            var tags = await GetJsonAsync<Tag[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/tags").ConfigureAwait(false);

            return tags ?? new Tag[0];
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

            var tickets = await GetJsonAsync<Ticket[]>($"/v1/spaces/{spaceIdOrWikiName}/tags/{tagId}/tickets", queryParameters).ConfigureAwait(false);

            return tickets ?? new Ticket[0];
        }

        ICustomFieldConnector ITicketConnector.CustomFields => this;

        ITicketStatusConnector ITicketConnector.Statuses => this;

        ITicketCommentConnector ITicketConnector.Comments => this;

        ITicketAssociationConnector ITicketConnector.Associations => this;
    }

    public partial class HttpAssemblaClient : ITicketAssociationConnector
    {
        async Task<IReadOnlyList<TicketAssociation>> ITicketAssociationConnector.GetAllOfTicketAsync(string spaceIdOrWikiName, int ticketNumber)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            var associations = await GetJsonAsync<TicketAssociation[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_associations").ConfigureAwait(false);

            return associations ?? new TicketAssociation[0];
        }

        async Task<TicketAssociation> ITicketAssociationConnector.GetAsync(string spaceIdOrWikiName, int ticketNumber, string associationId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (associationId == null)
            {
                throw new ArgumentNullException(nameof(associationId));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            var association = await GetJsonAsync<TicketAssociation>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_associations/{associationId}").ConfigureAwait(false);

            return association;
        }

        async Task<TicketAssociation> ITicketAssociationConnector.CreateAsync(string spaceIdOrWikiName, int ticketNumber, TicketAssociation association)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (association == null)
            {
                throw new ArgumentNullException(nameof(association));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            var createdAssociation = await PostAsync<TicketAssociationRequest, TicketAssociation>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_associations", new TicketAssociationRequest(association)).ConfigureAwait(false);

            return createdAssociation;
        }

        async Task ITicketAssociationConnector.UpdateAsync(string spaceIdOrWikiName, int ticketNumber, TicketAssociation association)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (association == null)
            {
                throw new ArgumentNullException(nameof(association));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }
            if (association.Id == null)
            {
                throw new ArgumentNullException(nameof(association.Id));
            }

            await PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_associations/{association.Id}", new TicketAssociationRequest(association)).ConfigureAwait(false);
        }

        async Task ITicketAssociationConnector.DeleteAsync(string spaceIdOrWikiName, int ticketNumber, string associationId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (associationId == null)
            {
                throw new ArgumentNullException(nameof(associationId));
            }
            if (ticketNumber == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketNumber));
            }

            await DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_associations/{associationId}").ConfigureAwait(false);
        }
    }

    public partial class HttpAssemblaClient : ITicketCommentConnector
    {
        async Task<IReadOnlyList<Comment>> ITicketCommentConnector.GetAllInTicketAsync(string spaceIdOrWikiName, int ticketNumber, int? page, int? pageSize)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetTicketQueryParameters(page: page, pageSize: pageSize);

            var comments = await GetJsonAsync<Comment[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_comments", queryParameters).ConfigureAwait(false);

            return comments ?? new Comment[0];
        }

        async Task<Comment> ITicketCommentConnector.GetAsync(string spaceIdOrWikiName, int ticketNumber, int commentId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var comment = await GetJsonAsync<Comment>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_comments/{commentId}").ConfigureAwait(false);

            return comment;
        }

        async Task<Comment> ITicketCommentConnector.CreateAsync(string spaceIdOrWikiName, int ticketNumber, Comment comment)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var createdComment = await PostAsync<CommentRequest, Comment>($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_comments", new CommentRequest(comment)).ConfigureAwait(false);

            return createdComment;
        }

        async Task ITicketCommentConnector.UpdateAsync(string spaceIdOrWikiName, int ticketNumber, Comment comment)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (comment.Id == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(comment.Id));
            }

            await PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/{ticketNumber}/ticket_comments/{comment.Id}", new CommentRequest(comment)).ConfigureAwait(false);
        }
    }

    public partial class HttpAssemblaClient : ITicketStatusConnector
    {
        async Task<IReadOnlyList<TicketStatus>> ITicketStatusConnector.GetAllAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var statuses = await GetJsonAsync<TicketStatus[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses").ConfigureAwait(false);

            return statuses ?? new TicketStatus[0];
        }

        async Task<TicketStatus> ITicketStatusConnector.GetAsync(string spaceIdOrWikiName, string statusId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (statusId == null)
            {
                throw new ArgumentNullException(nameof(statusId));
            }

            var status = await GetJsonAsync<TicketStatus>($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses/{statusId}").ConfigureAwait(false);

            return status;
        }

        async Task<TicketStatus> ITicketStatusConnector.CreateAsync(string spaceIdOrWikiName, TicketStatus status)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }

            var createdStatus = await PostAsync<TicketStatusRequest, TicketStatus>($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses", new TicketStatusRequest(status)).ConfigureAwait(false);

            return createdStatus;
        }

        async Task ITicketStatusConnector.UpdateAsync(string spaceIdOrWikiName, TicketStatus status)
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

            await PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses/{status.Id}", new TicketStatusRequest(status)).ConfigureAwait(false);
        }

        async Task ITicketStatusConnector.DeleteAsync(string spaceIdOrWikiName, string statusId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (statusId == null)
            {
                throw new ArgumentNullException(nameof(statusId));
            }

            await DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/statuses/{statusId}").ConfigureAwait(false);
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

            var fields = await GetJsonAsync<CustomField[]>($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields").ConfigureAwait(false);

            return fields ?? new CustomField[0];
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
            
            var field = await GetJsonAsync<CustomField>($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields/{customFieldId}").ConfigureAwait(false);

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

            var createdField = await PostAsync<CustomFieldRequest, CustomField>($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields", new CustomFieldRequest(customField)).ConfigureAwait(false);

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

            await PutAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields/{customField.Id}", customField).ConfigureAwait(false);
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

            await DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/tickets/custom_fields/{customFieldId}").ConfigureAwait(false);
        }
    }
}
