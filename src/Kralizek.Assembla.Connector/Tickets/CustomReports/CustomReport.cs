using Newtonsoft.Json;

namespace Assembla.Tickets.CustomReports
{
    public class CustomReport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonIgnore]
        public CustomReportType ReportType { get; set; }
    }

    public enum CustomReportType
    {
        TeamReport = 1,
        UserReport = 2
    }
}