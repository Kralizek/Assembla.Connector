using System.Linq;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class SpacesCopySample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace())
            {
                var copiedSpace = await client.Spaces.CopyAsync(test.Space.WikiName, new Space { Name = "Copied Space", WikiName = "asd" });
                copiedSpace.ShouldNotBeNull();

                var listAll = await client.Spaces.GetAllAsync();

                listAll.Count(c => c.Id == copiedSpace.Id).ShouldBe(1);

                await client.Spaces.DeleteAsync(copiedSpace.Id);
            }
        }
    }
}