using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Documents;
using Assembla.Tags;
using Assembla.Tickets.CustomFields;

namespace Assembla.Tickets
{
    public interface ITicketConnector
    {
        Task<IReadOnlyList<Ticket>> GetAsync(string spaceIdOrWikiName, Report? report = null, int? page = null, int? pageSize = null, TicketSortExpression? sortExpression = null, SortOrder? sort = null);

        Task<IReadOnlyList<Ticket>> GetUserActiveAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Ticket>> GetUserFollowedAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Ticket>> GetInMilestoneAsync(string spaceIdOrWikiName, string milestoneId);

        Task<IReadOnlyList<Ticket>> GetInNoMilestoneAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Ticket>> GetByTagAsync(string spaceIdOrWikiName, int tagId);

        Task<IReadOnlyList<CustomReport>> GetSpaceCustomReportAsync(string spaceIdOrWikiName);

        Task<Ticket> GetByNumberAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<Ticket> GetByIdAsync(string spaceIdOrWikiName, string ticketId);

        Task<Ticket> CreateAsync(string spaceIdOrWikiName, NewTicket newTicket);

        Task UpdateAsync(string spaceIdOrWikiName, Ticket ticket);

        Task DeleteAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<IReadOnlyList<Document>> GetTicketDocumentsAsync(string spaceIdOrWikiName, int ticketNumber);

        Task<IReadOnlyList<Tag>> GetTicketTagsAsync(string spaceIdOrWikiName, int ticketNumber);

        ICustomFieldConnector CustomFields { get; }
    }

    public struct TicketSortExpression
    {
        public string Expression { get; }

        private TicketSortExpression(string expression)
        {
            Expression = expression;
        }

        public static readonly TicketSortExpression Id = new TicketSortExpression("id");
        public static readonly TicketSortExpression Number = new TicketSortExpression("number");
        public static readonly TicketSortExpression Summary = new TicketSortExpression("summary");
        public static readonly TicketSortExpression Priority = new TicketSortExpression("priority");
        public static readonly TicketSortExpression CompletedDate = new TicketSortExpression("completed_date");
        public static readonly TicketSortExpression CreatedOn = new TicketSortExpression("created_on");
        public static readonly TicketSortExpression Importance = new TicketSortExpression("importance");
        public static readonly TicketSortExpression IsStory = new TicketSortExpression("is_story");
        public static readonly TicketSortExpression MilestoneId = new TicketSortExpression("milestone_id");
        public static readonly TicketSortExpression UpdatedAt = new TicketSortExpression("updated_at");
        public static readonly TicketSortExpression WorkingHours = new TicketSortExpression("working_hours");
        public static readonly TicketSortExpression Estimate = new TicketSortExpression("estimate");
        public static readonly TicketSortExpression TotalEstimate = new TicketSortExpression("total_estimate");
        public static readonly TicketSortExpression TotalInvestedHours = new TicketSortExpression("total_invested_hours");
        public static readonly TicketSortExpression TotalWorkingHours = new TicketSortExpression("total_working_hours");
    }

    public struct Report
    {
        public int Id { get; }

        public Report(int id)
        {
            Id = id;
        }

        public static implicit operator Report(int id) => new Report(id);

        public static readonly Report AllActive = new Report(0);
        public static readonly Report ActiveByMilestone = new Report(1);
        public static readonly Report ActiveByUser = new Report(3);
        public static readonly Report ClosedByMilestone = new Report(4);
        public static readonly Report ClosedByDate = new Report(6);
        public static readonly Report AllUserTickets = new Report(7);
        public static readonly Report AllUserActiveTickets = new Report(8);
        public static readonly Report AllUserClosedTickets = new Report(9);
        public static readonly Report AllUserFollowedTickets = new Report(10);
    }
}