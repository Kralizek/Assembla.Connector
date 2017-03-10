using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Milestones
{
    public class MilestoneRequest
    {
        public MilestoneRequest(Milestone milestone)
        {
            if (milestone == null)
            {
                throw new ArgumentNullException(nameof(milestone));
            }
            Milestone = milestone;
        }

        [JsonProperty("milestone")]
        public Milestone Milestone { get; }
    }
}