using System.Linq;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces.Tools;
using Assembla.Tags;
using Assembla.Tickets;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class TicketSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var newTicket = new Ticket {Summary = "Test Ticket", Priority = TicketPriority.Highest};

                var createdTicket = await client.Tickets.CreateAsync(test.Space.WikiName, newTicket);

                createdTicket.ShouldNotBeNull();
                createdTicket.Summary.ShouldBe(newTicket.Summary);
                createdTicket.Priority.ShouldBe(newTicket.Priority);

                var tag = await client.Tags.CreateAsync(test.Space.WikiName, new Tag {Name = "Test Tag"});

                await client.Tickets.UpdateAsync(test.Space.WikiName, new Ticket {Number = createdTicket.Number, Priority = TicketPriority.Lowest, Tags = new[] {tag.Name}});

                var updatedTicket = await client.Tickets.GetByNumberAsync(test.Space.WikiName, createdTicket.Number);

                updatedTicket.ShouldNotBeNull();
                updatedTicket.Summary.ShouldBe(createdTicket.Summary);
                updatedTicket.Priority.ShouldBe(TicketPriority.Lowest);

                var ticketTags = await client.Tickets.GetTicketTagsAsync(test.Space.WikiName, createdTicket.Number);

                ticketTags.Single().Name.ShouldBe(tag.Name);

                await client.Tickets.DeleteAsync(test.Space.WikiName, createdTicket.Number);
            }
        }
    }
}