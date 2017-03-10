using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Kralizek.Assembla.Connector.Spaces.Tools;
using Kralizek.Assembla.Connector.Tags;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class TagsSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Tickets))
            {
                var tag = new Tag { Name = "New Tag" };

                var createdTag = await client.Tags.CreateAsync(test.Space.Id, tag);

                createdTag.ShouldNotBeNull();
                createdTag.Name.ShouldBe(tag.Name);

                const string newName = "New Name";
                await client.Tags.UpdateAsync(test.Space.WikiName,
                    new Tag { Id = createdTag.Id, Name = newName, State = TagState.Hidden });

                var updatedTag = await client.Tags.GetAsync(test.Space.Id, createdTag.Id);

                updatedTag.Name.ShouldBe(newName);
                updatedTag.Id.ShouldBe(createdTag.Id);

                await client.Tags.DeleteAsync(test.Space.Id, updatedTag.Id);
            }
        }
    }
}