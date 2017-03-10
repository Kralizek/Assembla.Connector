using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Tags
{
    public class TagRequest
    {
        public TagRequest(Tag tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        [JsonProperty("tag")]
        public Tag Tag { get; }
    }
}