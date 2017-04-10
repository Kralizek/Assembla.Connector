using System;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Users.Roles
{
    public class UserRoleRequest
    {
        public UserRoleRequest(UserRole userRole)
        {
            UserRole = userRole ?? throw new ArgumentNullException(nameof(userRole));
        }

        [JsonProperty("user_role")]
        public UserRole UserRole { get; }
    }
}