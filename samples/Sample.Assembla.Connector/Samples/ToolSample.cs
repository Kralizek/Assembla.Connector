using System.Linq;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector;
using Kralizek.Assembla.Connector.Spaces.Tools;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class ToolSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace())
            {
                var tool = await client.Spaces.Tools.AddAsync(test.Space.WikiName, ToolType.SourceGit);

                tool.ShouldNotBeNull();
                tool.Id.ShouldNotBeNull();
                tool.ToolType.ShouldBe(ToolType.SourceGit);

                var listAll = await client.Spaces.Tools.GetAllInSpaceAsync(test.Space.WikiName);

                listAll.Count().ShouldBe(1);
                listAll.Single().Id.ShouldBe(tool.Id);

                var listRepo = await client.Spaces.Tools.GetAllRepoInSpaceAsync(test.Space.WikiName);

                listRepo.Count().ShouldBe(1);
                listRepo.Single().Id.ShouldBe(tool.Id);

                await client.Spaces.Tools.UpdateAsync(test.Space.WikiName, new Tool {Id = tool.Id, WatcherPermissions = Permissions.All});

                var updatedTool = await client.Spaces.Tools.GetAsync(test.Space.WikiName, tool.Id);

                updatedTool.WatcherPermissions.ShouldBe(Permissions.All);

                await client.Spaces.Tools.DeleteAsync(test.Space.WikiName, tool.Id);
            }
        }
    }
}