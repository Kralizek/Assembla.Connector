using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Spaces
{
    public class SpaceRequest
    {
        public SpaceRequest(Space space)
        {
            Space = space ?? throw new ArgumentNullException(nameof(space));
        }

        [JsonProperty("space")]
        public Space Space { get; }
    }
}