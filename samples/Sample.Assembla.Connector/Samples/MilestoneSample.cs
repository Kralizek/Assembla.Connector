using System.Threading.Tasks;
using Assembla;
using Assembla.Milestones;
using Assembla.Spaces.Tools;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class MilestoneSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            using (var test = await client.CreateDisposableSpace(requiredTools: ToolType.Milestones))
            {
                const string firstMilestoneName = "Test Milestone";
                const string newMilestoneName = "Edited Milestone";

                var createdMilestone = await client.Milestones.CreateAsync(test.Space.WikiName, new Milestone{ Title = firstMilestoneName});

                createdMilestone.Title.ShouldBe(firstMilestoneName);

                await client.Milestones.UpdateAsync(test.Space.WikiName, new Milestone{ Id = createdMilestone.Id, Title = newMilestoneName});

                var updatedMilestone = await client.Milestones.GetAsync(test.Space.WikiName, createdMilestone.Id);

                updatedMilestone.Title.ShouldBe(newMilestoneName);
            }
        }
    }
}