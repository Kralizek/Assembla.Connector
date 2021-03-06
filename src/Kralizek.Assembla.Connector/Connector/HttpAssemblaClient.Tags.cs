﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Tags;

namespace Kralizek.Assembla.Connector
{
    public partial class HttpAssemblaClient : ITagConnector
    {
        async Task<IReadOnlyList<Tag>> ITagConnector.GetAllAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            
            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags";

            var tags = await GetJsonAsync<Tag[]>(uri).ConfigureAwait(false);

            return tags ?? new Tag[0];
        }

        async Task<IReadOnlyList<Tag>> ITagConnector.GetActiveAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags/active";

            var tags = await GetJsonAsync<Tag[]>(uri).ConfigureAwait(false);

            return tags ?? new Tag[0];
        }

        async Task<IReadOnlyList<Tag>> ITagConnector.GetProposedAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags/proposed";

            var tags = await GetJsonAsync<Tag[]>(uri).ConfigureAwait(false);

            return tags ?? new Tag[0];
        }

        async Task<IReadOnlyList<Tag>> ITagConnector.GetHiddenAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags/hidden";

            var tags = await GetJsonAsync<Tag[]>(uri).ConfigureAwait(false);

            return tags ?? new Tag[0];
        }

        async Task<Tag> ITagConnector.GetAsync(string spaceIdOrWikiName, int tagId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags/{tagId}";

            var tag = await GetJsonAsync<Tag>(uri).ConfigureAwait(false);

            return tag;
        }

        async Task<Tag> ITagConnector.CreateAsync(string spaceIdOrWikiName, Tag tag)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags";

            var newTag = await PostAsync<TagRequest, Tag>(uri, new TagRequest(tag)).ConfigureAwait(false);

            return newTag;
        }

        async Task ITagConnector.UpdateAsync(string spaceIdOrWikiName, Tag tag)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags/{tag.Id}";

            await PutAsync(uri, new TagRequest(tag)).ConfigureAwait(false);
        }

        async Task ITagConnector.DeleteAsync(string spaceIdOrWikiName, int tagId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var uri = $"/v1/spaces/{spaceIdOrWikiName}/tags/{tagId}";

            await _client.DeleteAsync(uri).ConfigureAwait(false);
        }
    }

}
