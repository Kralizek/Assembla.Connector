using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces.Tools;
using Assembla.Tickets;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class TicketListSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var createdTickets = new List<Ticket>();
                for (int i = 1; i <= 15; i++)
                {
                    var ticket = CreateRandomTicket(i, (TicketPriority) (i % 5) + 1);
                    createdTickets.Add(await client.Tickets.CreateAsync(test.Space.WikiName, ticket));
                }

                var noInMilestone = await client.Tickets.GetInNoMilestoneAsync(test.Space.WikiName, pageSize: 20);

                foreach (var ticket in createdTickets)
                {
                    noInMilestone.ShouldContain(i => i.Summary == ticket.Summary);
                }
            }
        }

        private Ticket CreateRandomTicket(int index, TicketPriority priority)
        {
            return new Ticket
            {
                Summary = $"Test Ticket #{index}",
                Priority = priority
            };
        }
    }
}