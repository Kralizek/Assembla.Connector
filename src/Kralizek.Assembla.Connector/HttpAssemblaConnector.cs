using Assembla.Milestones;
using Assembla.Spaces;
using Assembla.Tags;
using Assembla.Tickets;

namespace Assembla
{

    public partial class HttpAssemblaConnector : IAssemblaConnector
    {
        public ISpaceConnector Spaces => this;

        public IMilestoneConnector Milestones => this;

        public ITicketConnector Tickets => this;

        public ITagConnector Tags => this;
    }
}
