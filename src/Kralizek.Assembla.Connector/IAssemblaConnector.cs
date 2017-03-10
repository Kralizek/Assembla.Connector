using Assembla.Files;
using Assembla.Milestones;
using Assembla.Spaces;
using Assembla.Tags;
using Assembla.Tickets;
using Assembla.Users;

namespace Assembla
{
    public interface IAssemblaClient
    {
        ISpaceConnector Spaces { get; }

        IMilestoneConnector Milestones { get; }

        ITicketConnector Tickets { get; }

        ITagConnector Tags { get; }

        IFileConnector Files { get; }

        IUserConnector Users { get; }
    }
}
