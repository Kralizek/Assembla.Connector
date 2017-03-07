using Newtonsoft.Json;

namespace Assembla.Spaces
{
    public class SpaceRequest
    {
        [JsonProperty("space")]
        public Space Space { get; set; }
    }
}