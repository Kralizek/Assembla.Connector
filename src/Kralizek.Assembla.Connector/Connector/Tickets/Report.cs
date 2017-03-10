using Kralizek.Assembla.Connector.Tickets.CustomReports;

namespace Kralizek.Assembla.Connector.Tickets
{
    public struct Report
    {
        public string Id { get; }

        private Report(int id)
        {
            Id = id.ToString("D");
        }

        private Report(string id)
        {
            Id = id;
        }

        public static implicit operator Report(CustomReport customReport) => FromCustomReport(customReport);

        public static Report FromCustomReport(CustomReport customReport) => new Report($"u{customReport.Id:D}");

        public static Report FromCustomReportId(int customReportId) => new Report($"u{customReportId:D}");

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