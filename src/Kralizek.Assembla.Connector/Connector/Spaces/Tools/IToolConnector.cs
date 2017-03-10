using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Spaces.Tools
{
    public interface IToolConnector
    {
        Task<IReadOnlyList<Tool>> GetAllInSpaceAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Tool>> GetAllRepoInSpaceAsync(string spaceIdOrWikiName);

        Task<Tool> GetAsync(string spaceIdOrWikiName, string toolId);

        Task<Tool> AddAsync(string spaceIdOrWikiName, ToolType toolType);

        Task UpdateAsync(string spaceIdOrWikiName, Tool tool);

        Task DeleteAsync(string spaceIdOrWikiName, string toolId);
    }
}