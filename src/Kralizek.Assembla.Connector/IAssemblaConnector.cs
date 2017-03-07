using Assembla.Milestones;
using Assembla.Spaces;
using Assembla.Tags;
using Assembla.Tickets;

namespace Assembla
{
    public interface IAssemblaConnector
    {
        ISpaceConnector Spaces { get; }

        IMilestoneConnector Milestones { get; }

        ITicketConnector Tickets { get; }

        ITagConnector Tags { get; }
    }
}
