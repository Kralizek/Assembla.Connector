using Newtonsoft.Json;

namespace Assembla.Milestones
{
    public class MilestoneRequest
    {
        [JsonProperty("milestone")]
        public Milestone Milestone { get; set; }
    }
}