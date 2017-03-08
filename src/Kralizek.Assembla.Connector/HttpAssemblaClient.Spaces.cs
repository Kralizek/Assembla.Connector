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
            throw new NotImplementedException();
        }

        async Task<Space> ISpaceConnector.GetAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task<Space> ISpaceConnector.CreateAsync(NewSpace newSpace)
        {
            throw new NotImplementedException();
        }

        async Task ISpaceConnector.UpdateAsync(Space space)
        {
            throw new NotImplementedException();
        }

        async Task ISpaceConnector.DeleteAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }

        async Task ISpaceConnector.CopyAsync(string spaceIdOrWikiName)
        {
            throw new NotImplementedException();
        }
    }
}
