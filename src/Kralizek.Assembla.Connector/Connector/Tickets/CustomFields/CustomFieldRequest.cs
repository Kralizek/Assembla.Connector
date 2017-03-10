using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tickets.CustomFields
{
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