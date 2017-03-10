using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Tickets.CustomFields
{
    public interface ICustomFieldConnector
    {
        Task<IReadOnlyList<CustomField>> GetAllAsync(string spaceIdOrWikiName);

        Task<CustomField> GetAsync(string spaceIdOrWikiName, string customFieldId);

        Task<CustomField> CreateAsync(string spaceIdOrWikiName, CustomField newCustomField);

        Task UpdateAsync(string spaceIdOrWikiName, CustomField customField);

        Task DeleteAsync(string spaceIdOrWikiName, string customFieldId);
    }
}