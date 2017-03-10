using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Kralizek.Assembla.Connector.Spaces.Tools;
using Kralizek.Assembla.Connector.Tickets;
using Kralizek.Assembla.Connector.Tickets.Associations;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class TicketAssociationSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var firstTicket = await client.Tickets.CreateAsync(test.Space.WikiName, new Ticket{ Summary = "First Ticket"});

                var secondTicket = await client.Tickets.CreateAsync(test.Space.WikiName, new Ticket{ Summary = "Second Ticket"});

                var newAssociation = new TicketAssociation
                {
                    FirstTicketId = firstTicket.Id,
                    SecondTicketId = secondTicket.Id,
                    Relationship = TicketRelationship.Related
                };

                var createdAssociation = await client.Tickets.Associations.CreateAsync(test.Space.WikiName, firstTicket.Number, newAssociation);

                createdAssociation.ShouldNotBeNull();
                createdAssociation.Relationship.ShouldBe(TicketRelationship.Related);

                await client.Tickets.Associations.UpdateAsync(test.Space.WikiName, firstTicket.Number, new TicketAssociation {Id = createdAssociation.Id, Relationship = TicketRelationship.Sibling});

                var updatedAssociation = await client.Tickets.Associations.GetAsync(test.Space.WikiName, firstTicket.Number, createdAssociation.Id);

                updatedAssociation.Relationship.ShouldBe(TicketRelationship.Sibling);

                await client.Tickets.Associations.DeleteAsync(test.Space.WikiName, firstTicket.Number, createdAssociation.Id);
            }
        }
    }
}