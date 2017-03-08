using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Spaces;

namespace Assembla
{
    public partial class HttpAssemblaClient : ISpaceConnector
    {
        async Task<IReadOnlyList<Space>> ISpaceConnector.GetAllAsync()
        {
            var spaces = await _client.GetAsync<Space[]>("/v1/spaces").ConfigureAwait(false);

            return spaces;
        }

        async Task<Space> ISpaceConnector.GetAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var space = await _client.GetAsync<Space>($"/v1/spaces/{spaceIdOrWikiName}").ConfigureAwait(false);

            return space;
        }

        async Task<Space> ISpaceConnector.CreateAsync(Space space)
        {
            if (space == null)
            {
                throw new ArgumentNullException(nameof(space));
            }

            var newSpace = await _client.PostAsync<SpaceRequest, Space>("/v1/spaces", new SpaceRequest(space)).ConfigureAwait(false);

            return newSpace;
        }

        async Task ISpaceConnector.UpdateAsync(Space space)
        {
            if (space == null)
            {
                throw new ArgumentNullException(nameof(space));
            }

            await _client.PutAsync($"/v1/spaces/{space.WikiName}", space).ConfigureAwait(false);
        }

        async Task ISpaceConnector.DeleteAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}").ConfigureAwait(false);
        }

        async Task<Space> ISpaceConnector.CopyAsync(string spaceIdOrWikiName, Space space)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (space == null)
            {
                throw new ArgumentNullException(nameof(space));
            }

            var copiedSpace = await _client.PostAsync<SpaceRequest, Space>($"/v1/spaces/{spaceIdOrWikiName}/copy", new SpaceRequest(space)).ConfigureAwait(false);

            return copiedSpace;
        }
    }
}
