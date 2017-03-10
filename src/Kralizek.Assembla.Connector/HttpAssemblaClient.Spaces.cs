using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Spaces;
using Assembla.Spaces.Tools;

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

            var newSpace = await _client.PostJsonAsync<SpaceRequest, Space>("/v1/spaces", new SpaceRequest(space)).ConfigureAwait(false);

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

            var copiedSpace = await _client.PostJsonAsync<SpaceRequest, Space>($"/v1/spaces/{spaceIdOrWikiName}/copy", new SpaceRequest(space)).ConfigureAwait(false);

            return copiedSpace;
        }

        IToolConnector ISpaceConnector.Tools => this;
    }

    public partial class HttpAssemblaClient : IToolConnector
    {
        async Task<IReadOnlyList<Tool>> IToolConnector.GetAllInSpaceAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var tools = await _client.GetAsync<Tool[]>($"/v1/spaces/{spaceIdOrWikiName}/space_tools").ConfigureAwait(false);

            return tools;
        }

        async Task<IReadOnlyList<Tool>> IToolConnector.GetAllRepoInSpaceAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var tools = await _client.GetAsync<Tool[]>($"/v1/spaces/{spaceIdOrWikiName}/space_tools/repo").ConfigureAwait(false);

            return tools;
        }

        async Task<Tool> IToolConnector.GetAsync(string spaceIdOrWikiName, string toolIdOrName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (toolIdOrName == null)
            {
                throw new ArgumentNullException(nameof(toolIdOrName));
            }

            var tool = await _client.GetAsync<Tool>($"/v1/spaces/{spaceIdOrWikiName}/space_tools/{toolIdOrName}").ConfigureAwait(false);

            return tool;
        }

        async Task<Tool> IToolConnector.AddAsync(string spaceIdOrWikiName, ToolType toolType)
        {
            var tool = await _client.PostCommandAsync<Tool>($"/v1/spaces/{spaceIdOrWikiName}/space_tools/{toolType:D}/add").ConfigureAwait(false);

            return tool;
        }

        async Task IToolConnector.UpdateAsync(string spaceIdOrWikiName, Tool tool)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (tool == null)
            {
                throw new ArgumentNullException(nameof(tool));
            }
            if (string.IsNullOrEmpty(tool.Id))
            {
                throw new ArgumentNullException(nameof(tool.Id));
            }

            await _client.PutAsync($"/v1/spaces/{spaceIdOrWikiName}/space_tools/{tool.Id}", tool).ConfigureAwait(false);
        }

        async Task IToolConnector.DeleteAsync(string spaceIdOrWikiName, string toolIdOrName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (toolIdOrName == null)
            {
                throw new ArgumentNullException(nameof(toolIdOrName));
            }

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/space_tools/{toolIdOrName}").ConfigureAwait(false);

        }
    }
}
