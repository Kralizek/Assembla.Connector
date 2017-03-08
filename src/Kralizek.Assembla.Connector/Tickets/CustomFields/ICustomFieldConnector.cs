using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assembla.Tickets.CustomFields
{
    public interface ICustomFieldConnector
    {
        Task<IReadOnlyList<CustomField>> GetAllAsync(string spaceIdOrWikiName);

        Task<CustomField> GetAsync(string spaceIdOrWikiName, string customFieldId);

        Task<CustomField> CreateAsync(string spaceIdOrWikiName, CustomField newCustomField);

        Task UpdateAsync(string spaceIdOrWikiName, CustomField customField);

        Task DeleteAsync(string spaceIdOrWikiName, string customFieldId);
    }

    public class CustomFieldRequest
    {
        [JsonProperty("custom_field")]
        public CustomField CustomField { get; }

        public CustomFieldRequest(CustomField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            CustomField = field;
        }
    }
}