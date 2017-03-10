using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Milestones;

namespace Assembla
{
    public partial class HttpAssemblaClient : IMilestoneConnector
    {
        private IReadOnlyDictionary<string, string> GetMilestoneQueryParameters(int? page = null, int? pageSize = null, SortOrder? sort = null)
        {
            var queryParameters = new Dictionary<string, string>();

            if (page.HasValue)
            {
                queryParameters.Add("page", page.Value.ToString("D"));
            }
            if (pageSize.HasValue)
            {
                queryParameters.Add("per_page", pageSize.Value.ToString("D"));
            }
            if (sort.HasValue)
            {
                queryParameters.Add("due_date_order", sort.Value.Direction);
            }

            return queryParameters;
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetAllAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetMilestoneQueryParameters(page: page, pageSize: pageSize, sort: sort);

            var milestones = await _client.GetAsync<Milestone[]>($"/v1/spaces/{spaceIdOrWikiName}/milestones/all", queryParameters).ConfigureAwait(false);

            return milestones;
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetUpcomingAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetMilestoneQueryParameters(page: page, pageSize: pageSize, sort: sort);

            var milestones = await _client.GetAsync<Milestone[]>($"/v1/spaces/{spaceIdOrWikiName}/milestones/upcoming", queryParameters).ConfigureAwait(false);

            return milestones;
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetCompletedAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetMilestoneQueryParameters(page: page, pageSize: pageSize, sort: sort);

            var milestones = await _client.GetAsync<Milestone[]>($"/v1/spaces/{spaceIdOrWikiName}/milestones/completed", queryParameters).ConfigureAwait(false);

            return milestones;
        }

        async Task<IReadOnlyList<Milestone>> IMilestoneConnector.GetReleaseNotesAsync(string spaceIdOrWikiName, int? page, int? pageSize, SortOrder? sort)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetMilestoneQueryParameters(page: page, pageSize: pageSize, sort: sort);

            var milestones = await _client.GetAsync<Milestone[]>($"/v1/spaces/{spaceIdOrWikiName}/milestones/release_notes", queryParameters).ConfigureAwait(false);

            return milestones;
        }

        async Task<Milestone> IMilestoneConnector.GetAsync(string spaceIdOrWikiName, string milestoneId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (milestoneId == null)
            {
                throw new ArgumentNullException(nameof(milestoneId));
            }

            var milestone = await _client.GetAsync<Milestone>($"/v1/spaces/{spaceIdOrWikiName}/milestones/{milestoneId}").ConfigureAwait(false);

            return milestone;
        }

        async Task<Milestone> IMilestoneConnector.CreateAsync(string spaceIdOrWikiName, Milestone milestone)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (milestone == null)
            {
                throw new ArgumentNullException(nameof(milestone));
            }

            var createdMilestone = await _client.PostJsonAsync<MilestoneRequest, Milestone>($"/v1/spaces/{spaceIdOrWikiName}/milestones", new MilestoneRequest(milestone)).ConfigureAwait(false);

            return createdMilestone;
        }

        async Task IMilestoneConnector.UpdateAsync(string spaceIdOrWikiName, Milestone milestone)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (milestone == null)
            {
                throw new ArgumentNullException(nameof(milestone));
            }

            await _client.PutAsync($"/v1/spaces/{spaceIdOrWikiName}/milestones/{milestone.Id}", new MilestoneRequest(milestone)).ConfigureAwait(false);
        }

        async Task IMilestoneConnector.DeleteAsync(string spaceIdOrWikiName, string milestoneId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (milestoneId == null)
            {
                throw new ArgumentNullException(nameof(milestoneId));
            }

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/milestones/{milestoneId}").ConfigureAwait(false);
        }
    }
}
