using System.Collections.Generic;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector.Spaces.Tools;
using Kralizek.Assembla.Connector.Tickets;
using Kralizek.Assembla.Connector.Tickets.CustomFields;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class CustomFieldListSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var customField = await client.Tickets.CustomFields.CreateAsync(test.Space.WikiName,
                    new CustomField
                    {
                        Title = "Type", 
                        IsRequired = true, 
                        Type = CustomFieldType.List,
                        ListOptions = new[] {"Research", "Development", "Maintenance"},
                    });

                var ticketWithRequiredCustomField = await client.Tickets.CreateAsync(test.Space.WikiName,
                    new Ticket
                    {
                        Summary = "Ticket with required custom field",
                        CustomFields = new Dictionary<string, string>() {{"Type", "Development"}}
                    });

                ticketWithRequiredCustomField.CustomFields[customField.Title].ShouldBe("Development");

                var customFieldwithDefault = await client.Tickets.CustomFields.CreateAsync(test.Space.WikiName,
                    new CustomField
                    {
                        Title = "TypeDefault",
                        IsRequired = true,
                        Type = CustomFieldType.List,
                        ListOptions = new[] { "Research", "Development", "Maintenance" },
                        DefaultValue = "Maintenance"
                    });

                var ticketWithDefaultCustomField = await client.Tickets.CreateAsync(test.Space.WikiName,
                    new Ticket
                    {
                        Summary = "Ticket with required custom field",
                        CustomFields = new Dictionary<string, string>() { { "Type", "Development" } }
                    });

                ticketWithDefaultCustomField.CustomFields[customField.Title].ShouldBe("Development");
                ticketWithDefaultCustomField.CustomFields[customFieldwithDefault.Title].ShouldBe("Maintenance");

                var allCustomFields = await client.Tickets.CustomFields.GetAllAsync(test.Space.WikiName);

                allCustomFields.ShouldContain(c => c.Id == customField.Id);
            }
        }
    }
}