using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces.Tools;
using Assembla.Tickets;
using Assembla.Tickets.CustomFields;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class CustomFieldSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var customField = await client.Tickets.CustomFields.CreateAsync(test.Space.WikiName, new CustomField {Title = "Test Field", DefaultValue = "DefaultValue", IsRequired = true});

                var ticketWithDefaultValue = await client.Tickets.CreateAsync(test.Space.WikiName, new Ticket {Summary = "Ticket with Default Value"});

                ticketWithDefaultValue.CustomFields[customField.Title].ShouldBe(customField.DefaultValue);

                const string specialValue = "A Special Value";

                var ticketWithSpecifiedValue = await client.Tickets.CreateAsync(test.Space.WikiName, new Ticket { Summary = "Ticket with Default Value", CustomFields = new Dictionary<string, string>{[customField.Title] = specialValue} });

                ticketWithSpecifiedValue.CustomFields[customField.Title].ShouldBe(specialValue);

                var allCustomFields = await client.Tickets.CustomFields.GetAllAsync(test.Space.WikiName);

                allCustomFields.ShouldContain(c => c.Id == customField.Id);

                const string newDefaultValue = "New Default Value";

                await client.Tickets.CustomFields.UpdateAsync(test.Space.WikiName, new CustomField {Id = customField.Id, DefaultValue = newDefaultValue});

                var updatedCustomField = await client.Tickets.CustomFields.GetAsync(test.Space.WikiName, customField.Id);

                updatedCustomField.DefaultValue.ShouldBe(newDefaultValue);

                await client.Tickets.CustomFields.DeleteAsync(test.Space.WikiName, customField.Id);
            }
        }
    }
}