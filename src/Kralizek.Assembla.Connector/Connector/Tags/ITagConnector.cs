using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Tags
{
    public interface ITagConnector
    {
        Task<IReadOnlyList<Tag>> GetAllAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Tag>> GetActiveAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Tag>> GetProposedAsync(string spaceIdOrWikiName);

        Task<IReadOnlyList<Tag>> GetHiddenAsync(string spaceIdOrWikiName);

        Task<Tag> GetAsync(string spaceIdOrWikiName, int tagId);

        Task<Tag> CreateAsync(string spaceIdOrWikiName, Tag tag);

        Task UpdateAsync(string spaceIdOrWikiName, Tag tag);

        Task DeleteAsync(string spaceIdOrWikiName, int tagId);
    }
}