using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Tags;

namespace Assembla
{
    public partial class HttpAssemblaConnector : ITagConnector
    {
        async Task<IReadOnlyList<Tag>> ITagConnector.GetAllAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Tag>> ITagConnector.GetActiveAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Tag>> ITagConnector.GetProposedAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyList<Tag>> ITagConnector.GetHiddenAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<Tag> ITagConnector.GetAsync(string spaceIdOrWikiName, int tagId)
        {
            throw new NotImplementedException();
        }

        async Task<Tag> ITagConnector.CreateAsync(string spaceIdOrWikiName, Tag tag)
        {
            throw new NotImplementedException();
        }

        async Task ITagConnector.UpdateAsync(string spaceIdOrWikiName, Tag tag)
        {
            throw new NotImplementedException();
        }

        async Task ITagConnector.DeleteAsync(string spaceIdOrWikiName, int tagId)
        {
            throw new NotImplementedException();
        }
    }

}
