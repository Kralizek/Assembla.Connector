using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces.Tools;
using Assembla.Tickets;
using Assembla.Tickets.Comments;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class TicketCommentSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var ticket = await client.Tickets.CreateAsync(test.Space.WikiName, new Ticket {Summary = "Test Ticket"});

                var createdComment = await client.Tickets.Comments.CreateAsync(test.Space.WikiName, ticket.Number, new Comment{ Text = "This is a comment"});

                await client.Tickets.Comments.UpdateAsync(test.Space.WikiName, ticket.Number, new Comment {Id = createdComment.Id, Text = "Updated text"});

                var updatedComment = await client.Tickets.Comments.GetAsync(test.Space.WikiName, ticket.Number, createdComment.Id);

                updatedComment.Id.ShouldBe(createdComment.Id);
                updatedComment.Text.ShouldBe("Updated text");
            }
        }
    }
}