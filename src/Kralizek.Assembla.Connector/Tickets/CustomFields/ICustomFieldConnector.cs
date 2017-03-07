using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla.Tickets.CustomFields
{
    public interface ICustomFieldConnector
    {
        Task<IReadOnlyList<CustomField>> GetAllAsync(string spaceIdOrWikiName);

        Task<CustomField> GetAsync(string spaceIdOrWikiName, string customFieldId);

        Task<CustomField> CreateAsync(string spaceIdOrWikiName, NewCustomField newCustomField);

        Task UpdateAsync(string spaceIdOrWikiName, CustomField customField);

        Task DeleteAsync(string spaceIdOrWikiName, string customFieldId);
    }
}