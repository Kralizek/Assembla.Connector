using System;
using Newtonsoft.Json;

namespace Assembla.Tickets
{
    public class NewTicket
    {
        public NewTicket(string summary)
        {
            if (string.IsNullOrEmpty(summary))
            {
                throw new ArgumentNullException(nameof(summary));
            }
            Summary = summary;
        }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("priority")]
        public TicketPriority Priority { get; set; } = TicketPriority.Normal;
    }
}