using Newtonsoft.Json;

namespace Assembla.Tickets
{
    public class CustomReport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public CustomReportType ReportType { get; set; }
    }

    public enum CustomReportType
    {
        TeamReport = 1,
        UserReport = 2
    }
}