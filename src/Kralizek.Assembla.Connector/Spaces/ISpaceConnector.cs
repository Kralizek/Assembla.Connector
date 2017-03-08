using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Spaces.Tools;

namespace Assembla.Spaces
{
    public interface ISpaceConnector
    {
        Task<IReadOnlyList<Space>> GetAllAsync();

        Task<Space> GetAsync(string spaceIdOrWikiName);

        Task<Space> CreateAsync(Space newSpace);

        Task UpdateAsync(Space space);

        Task DeleteAsync(string spaceIdOrWikiName);

        Task<Space> CopyAsync(string spaceIdOrWikiName, Space newSpace);

        IToolConnector Tools { get; }
    }
}