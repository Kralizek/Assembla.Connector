using Assembla.Milestones;
using Assembla.Spaces;
using Assembla.Tags;
using Assembla.Tickets;
using Microsoft.Extensions.Logging;
using System;

namespace Assembla
{

    public partial class HttpAssemblaClient : IAssemblaClient
    {
        private readonly ILogger _logger;

        public HttpAssemblaClient(ILogger<HttpAssemblaClient> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ISpaceConnector Spaces => this;

        public IMilestoneConnector Milestones => this;

        public ITicketConnector Tickets => this;

        public ITagConnector Tags => this;
    }
}
