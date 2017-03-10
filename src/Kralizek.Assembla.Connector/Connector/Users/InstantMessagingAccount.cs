using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Users
{
    public class InstantMessagingAccount
    {
        [JsonProperty("type")]
        public InstantMessagingAccountType Type { get; set; }

        [JsonProperty("id")]
        public string Account { get; set; }
    }
}