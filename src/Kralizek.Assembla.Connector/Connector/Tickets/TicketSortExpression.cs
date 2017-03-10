namespace Kralizek.Assembla.Connector.Tickets
{
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
}