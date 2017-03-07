using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla.Spaces
{
    public interface ISpaceConnector
    {
        Task<IReadOnlyList<Space>> GetAllAsync();

        Task<Space> GetAsync(string spaceIdOrWikiName);

        Task<Space> CreateAsync(NewSpace newSpace);

        Task UpdateAsync(Space space);

        Task DeleteAsync(string spaceIdOrWikiName);

        Task CopyAsync(string spaceIdOrWikiName);
    }
}