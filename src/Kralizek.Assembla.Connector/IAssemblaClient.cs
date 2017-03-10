using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Milestones;
using Kralizek.Assembla.Connector.Spaces;
using Kralizek.Assembla.Connector.Tags;
using Kralizek.Assembla.Connector.Tickets;
using Kralizek.Assembla.Connector.Users;

namespace Kralizek.Assembla
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
