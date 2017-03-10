using System.Linq;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Kralizek.Assembla.Connector.Spaces;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class SpacesSimpleSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            var newSpace = new Space { Name = "Test Space" };

            var createdSpace = await client.Spaces.CreateAsync(newSpace);

            createdSpace.ShouldNotBeNull();
            createdSpace.Name.ShouldBe(newSpace.Name);
            createdSpace.Id.ShouldNotBeNull();

            const string newName = "New Name";
            await client.Spaces.UpdateAsync(new Space { Name = newName, WikiName = createdSpace.WikiName });

            var updatedSpace = await client.Spaces.GetAsync(createdSpace.WikiName);

            updatedSpace.Name.ShouldBe(newName);
            updatedSpace.Id.ShouldBe(createdSpace.Id);

            var listAll = await client.Spaces.GetAllAsync();
            listAll.Count(c => c.Id == updatedSpace.Id).ShouldBe(1);

            await client.Spaces.DeleteAsync(updatedSpace.Id);

        }
    }
}