namespace Kralizek.Assembla.Connector.Tickets
{
    public struct TicketStatusFilter
    {
        public string Status { get; }

        public TicketStatusFilter(string status)
        {
            Status = status;
        }

        public static readonly TicketStatusFilter Active = new TicketStatusFilter("active");
        public static readonly TicketStatusFilter Closed = new TicketStatusFilter("closed");
        public static readonly TicketStatusFilter All = new TicketStatusFilter("all");
    }
}