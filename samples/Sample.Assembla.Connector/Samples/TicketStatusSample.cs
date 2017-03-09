using System;
using System.Linq;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces.Tools;
using Assembla.Tickets;
using Assembla.Tickets.Statuses;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class TicketStatusSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var createdClosedStatus = await client.Tickets.Statuses.CreateAsync(test.Space.WikiName, new TicketStatus {Name = "Duplicate", State = TicketState.Closed});

                var createdOpenStatus = await client.Tickets.Statuses.CreateAsync(test.Space.WikiName, new TicketStatus { Name = "Waiting for External", State = TicketState.Open });

                var existingStatuses = await client.Tickets.Statuses.GetAllAsync(test.Space.WikiName);

                existingStatuses.ShouldContain(c => c.Id == createdClosedStatus.Id);

                existingStatuses.ShouldContain(c => c.Id == createdOpenStatus.Id);

                await client.Tickets.Statuses.DeleteAsync(test.Space.WikiName, createdClosedStatus.Id);

                await client.Tickets.Statuses.UpdateAsync(test.Space.WikiName, new TicketStatus {Id = createdOpenStatus.Id, Name = "Updated Ticket Status"});

                var updatedStatus = await client.Tickets.Statuses.GetAsync(test.Space.WikiName, createdOpenStatus.Id);

                updatedStatus.Name.ShouldBe("Updated Ticket Status");
            }
        }
    }
}