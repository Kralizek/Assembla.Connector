using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.CustomReports
{
    public class CustomReportList
    {
        [JsonProperty("team_reports")]
        public IReadOnlyList<CustomReport> TeamReports { get; set; }

        [JsonProperty("user_reports")]
        public IReadOnlyList<CustomReport> UserReports { get; set; }
    }
}