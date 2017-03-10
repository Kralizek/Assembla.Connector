using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Users
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("picture")]
        public Uri Picture { get; set; }

        [JsonProperty("im")]
        public InstantMessagingAccount InstantMessagingAccount1 { get; set; }

        [JsonProperty("im2")]
        public InstantMessagingAccount InstantMessagingAccount2 { get; set; }
    }
}