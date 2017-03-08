using System;
using Newtonsoft.Json;

namespace Assembla.Spaces
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