using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assembla.Users;

namespace Assembla
{
    public partial class HttpAssemblaClient : IUserConnector
    {
        async Task<User> IUserConnector.GetAsync()
        {
            var currentUser = await GetJsonAsync<User>("/v1/user").ConfigureAwait(false);

            return currentUser;
        }

        async Task<User> IUserConnector.GetAsync(string userIdOrLogin)
        {
            if (userIdOrLogin == null)
            {
                throw new ArgumentNullException(nameof(userIdOrLogin));
            }
            var user = await GetJsonAsync<User>($"/v1/users/{userIdOrLogin}").ConfigureAwait(false);

            return user;
        }

        async Task<byte[]> IUserConnector.GetPictyreAsync(string userIdOrLogin)
        {
            if (userIdOrLogin == null)
            {
                throw new ArgumentNullException(nameof(userIdOrLogin));
            }

            var picture = await GetRawAsync($"/v1/users/{userIdOrLogin}/picture").ConfigureAwait(false);

            return picture;
        }

        async Task<IReadOnlyList<User>> IUserConnector.GetInSpaceAsync(string spaceId)
        {
            if (spaceId == null)
            {
                throw new ArgumentNullException(nameof(spaceId));
            }
            var users = await GetJsonAsync<User[]>($"/v1/spaces/{spaceId}/users").ConfigureAwait(false);

            return users;
        }
    }
}
