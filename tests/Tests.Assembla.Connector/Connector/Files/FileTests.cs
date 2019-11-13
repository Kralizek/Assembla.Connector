using Kralizek.Assembla.Connector.Files;
using NUnit.Framework;
using Shouldly;

namespace Tests.Connector.Files
{
    [TestFixture]
    public class FileTests
    {
        [Test, CustomAutoData]
        public void File_IsAttachedToTicket_should_be_TRUE_when_attached_to_Ticket(string fileId)
        {
            File file = new File
            {
                AttachableId = fileId,
                AttachableType = AttachableType.Ticket
            };

            file.IsAttachedToTicket().ShouldBe(true);
        }

        [Test, CustomAutoData]
        public void File_IsAttachedToMilestone_should_be_TRUE_when_attached_to_Milestone(string fileId)
        {
            File file = new File
            {
                AttachableId = fileId,
                AttachableType = AttachableType.Milestone
            };

            file.IsAttachedToMilestone().ShouldBe(true);
        }

        [Test, CustomAutoData]
        public void File_IsAttachedToMessage_should_be_TRUE_when_attached_to_Message(string fileId)
        {
            File file = new File
            {
                AttachableId = fileId,
                AttachableType = AttachableType.Flow
            };

            file.IsAttachedToMessage().ShouldBe(true);
        }
    }
}