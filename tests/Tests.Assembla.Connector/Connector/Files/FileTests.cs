using Kralizek.Assembla.Connector.Files;
using Shouldly;
using Xunit;

namespace Tests.Assembla.Connector.Files
{
    public class FileTests
    {
        [Fact]
        public void File_IsAttachedToTicket_should_be_TRUE_when_attached_to_Ticket()
        {
            File file = new File
            {
                AttachableId = "1234",
                AttachableType = AttachableType.Ticket
            };

            file.IsAttachedToTicket().ShouldBe(true);
        }

        [Fact]
        public void File_IsAttachedToMilestone_should_be_TRUE_when_attached_to_Milestone()
        {
            File file = new File
            {
                AttachableId = "1234",
                AttachableType = AttachableType.Milestone
            };

            file.IsAttachedToMilestone().ShouldBe(true);
        }

        [Fact]
        public void File_IsAttachedToMessage_should_be_TRUE_when_attached_to_Message()
        {
            File file = new File
            {
                AttachableId = "1234",
                AttachableType = AttachableType.Flow
            };

            file.IsAttachedToMessage().ShouldBe(true);
        }
    }
}