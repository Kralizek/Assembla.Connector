using Assembla.Milestones;
using Assembla.Spaces;
using Assembla.Tags;
using Assembla.Tickets;
using Microsoft.Extensions.Logging;
using System;
using Assembla.Documents;

namespace Assembla
{

    public partial class HttpAssemblaClient : IAssemblaClient
    {
        private readonly IHttpClient _client;
        private readonly ILogger _logger;

        public HttpAssemblaClient(IHttpClient client, ILogger<HttpAssemblaClient> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ISpaceConnector Spaces => this;

        public IMilestoneConnector Milestones => this;

        public ITicketConnector Tickets => this;

        public ITagConnector Tags => this;
        public IDocumentConnector Documents => this;
    }
}
