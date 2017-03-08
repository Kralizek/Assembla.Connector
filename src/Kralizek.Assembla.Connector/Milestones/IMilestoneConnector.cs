using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla.Milestones
{
    public interface IMilestoneConnector
    {
        Task<IReadOnlyList<Milestone>> GetAllAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null, SortOrder? sort = null);

        Task<IReadOnlyList<Milestone>> GetUpcomingAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null, SortOrder? sort = null);

        Task<IReadOnlyList<Milestone>> GetCompletedAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null, SortOrder? sort = null);

        Task<IReadOnlyList<Milestone>> GetReleaseNotesAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null, SortOrder? sort = null);

        Task<Milestone> GetAsync(string spaceIdOrWikiName, string milestoneId);

        Task<Milestone> CreateAsync(string spaceIdOrWikiName, Milestone newMilestone);

        Task UpdateAsync(string spaceIdOrWikiName, Milestone milestone);

        Task DeleteAsync(string spaceIdOrWikiName, string milestoneId);
    }
}
