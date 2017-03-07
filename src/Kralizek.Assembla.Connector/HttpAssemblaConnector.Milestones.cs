using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Milestones;

namespace Assembla
{
    public partial class HttpAssemblaConnector : IMilestoneConnector
    {
        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetAllAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetUpcomingAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetCompletedAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetReleaseNotesAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            throw new NotImplementedException();
        }

        async Task<Milestone> IMilestoneConnector.GetAsync(string spaceIdOrWikiName, string milestoneId)
        {
            throw new NotImplementedException();
        }

        async Task<Milestone> IMilestoneConnector.CreateAsync(string spaceIdOrWikiName, NewMilestone newMilestone)
        {
            throw new NotImplementedException();
        }

        async Task IMilestoneConnector.UpdateAsync(string spaceIdOrWikiName, Milestone milestone)
        {
            throw new NotImplementedException();
        }

        async Task IMilestoneConnector.DeleteAsync(string spaceIdOrWikiName, string milestoneId)
        {
            throw new NotImplementedException();
        }
    }
}
