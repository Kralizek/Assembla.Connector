using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.CustomReports
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
}