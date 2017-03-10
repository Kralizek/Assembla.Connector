using System.Text;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Files.Content;
using Kralizek.Assembla.Connector.Spaces.Tools;
using Kralizek.Assembla.Connector.Tickets;
using Kralizek.Assembla.Connector.Users;
using Newtonsoft.Json;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class FileSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace("File Test Space", ToolType.Files, ToolType.Tickets))
            {
                await TestWithNoExtraData(client, test);
                await TestWithExtraData(client, test);
                await TestAttachToTicket(client, test);
            }
        }

        private static async Task TestWithNoExtraData(IAssemblaClient client, DisposableSpace test)
        {
            const string fileContent = "HelloWorld";
            const string fileName = "hello-world.txt";

            var uploadedFile = await client.Files.CreateAsync(test.Space.WikiName, FileContent.FromString(fileContent, fileName: fileName));

            uploadedFile.FileName.ShouldBe(fileName);
            uploadedFile.CreatedBy.ShouldBe(test.CurrentUser.Id);
        }

        private static async Task TestWithExtraData(IAssemblaClient client, DisposableSpace test)
        {
            var content = FileContent.FromObjectAsJson(test.CurrentUser, fileName: "user.txt");
            var document = new File { Name = "Current User", FileName = "currentUser.txt", Description = "Some text to describe the file" };
            var uploadedFile = await client.Files.CreateAsync(test.Space.WikiName, content, document);

            uploadedFile.FileName.ShouldBe(document.FileName);
            uploadedFile.Name.ShouldBe(document.Name);
            uploadedFile.Description.ShouldBe(document.Description);
            uploadedFile.CreatedBy.ShouldBe(test.CurrentUser.Id);

            var downloadedFile = await client.Files.DownloadAsync(test.Space.WikiName, uploadedFile.Id);
            var downloadedUser = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(downloadedFile));

            downloadedUser.Login.ShouldBe(test.CurrentUser.Login);
        }

        private static async Task TestAttachToTicket(IAssemblaClient client, DisposableSpace test)
        {
            var ticket = await client.Tickets.CreateAsync(test.Space.Id, new Ticket {Summary = "Test Ticket"});

            const string fileContent = "HelloWorld";
            const string fileName = "hello-world.txt";
            var content = FileContent.FromString(fileContent, fileName: fileName);
            var document = new File { AttachableId = ticket.Id, AttachableType = AttachableType.Ticket };

            var uploadedFile = await client.Files.CreateAsync(test.Space.WikiName, content, document);

            uploadedFile.TicketId.ShouldBe(ticket.Id);
            uploadedFile.AttachableType.ShouldBe(AttachableType.Ticket);
            uploadedFile.IsAttachedToTicket().ShouldBeTrue();
        }

    }
}