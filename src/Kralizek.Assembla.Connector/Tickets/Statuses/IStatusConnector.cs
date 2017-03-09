using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assembla.Tickets.Statuses
{
    public interface IStatusConnector
    {
        Task<IReadOnlyList<TicketStatus>> GetAllAsync(string spaceIdOrWikiName);

        Task<TicketStatus> GetAsync(string spaceIdOrWikiName, string statusId);

        Task<TicketStatus> CreateAsync(string spaceIdOrWikiName, TicketStatus status);

        Task UpdateAsync(string spaceIdOrWikiName, TicketStatus status);

        Task DeleteAsync(string spaceIdOrWikiName, string statusId);
    }

    public class TicketStatusRequest
    {
        [JsonProperty("status")]
        public TicketStatus Status { get; }

        public TicketStatusRequest(TicketStatus status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            Status = status;
        }
    }
}