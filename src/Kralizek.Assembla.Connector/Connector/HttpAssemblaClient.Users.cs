using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Users;
using Kralizek.Assembla.Connector.Users.Roles;

namespace Kralizek.Assembla.Connector
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

            return users ?? new User[0];
        }

        IUserRoleConnector IUserConnector.UserRoles => this;
    }

    public partial class HttpAssemblaClient : IUserRoleConnector
    {
        async Task<IReadOnlyList<UserRole>> IUserRoleConnector.GetAsync(string spaceIdOrWikiName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var roles = await GetJsonAsync<UserRole[]>($"/v1/spaces/{spaceIdOrWikiName}/user_roles").ConfigureAwait(false);

            return roles ?? new UserRole[0];
        }

        async Task<UserRole> IUserRoleConnector.GetAsync(string spaceIdOrWikiName, int userRoleId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (userRoleId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userRoleId));
            }

            var role = await GetJsonAsync<UserRole>($"/v1/spaces/{spaceIdOrWikiName}/user_roles/{userRoleId}").ConfigureAwait(false);

            return role;
        }

        async Task<UserRole> IUserRoleConnector.CreateAsync(string spaceIdOrWikiName, UserRole userRole)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole));
            }

            var createdUserRole = await PostAsync<UserRoleRequest, UserRole>($"/v1/spaces/{spaceIdOrWikiName}/user_roles", new UserRoleRequest(userRole)).ConfigureAwait(false);

            return createdUserRole;
        }

        async Task IUserRoleConnector.UpdateAsync(string spaceIdOrWikiName, UserRole userRole)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole));
            }
            if (userRole.Id == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userRole.Id));
            }

            await PutAsync($"/v1/spaces/{spaceIdOrWikiName}/user_roles/{userRole.Id}", new UserRoleRequest(userRole)).ConfigureAwait(false);
        }

        async Task IUserRoleConnector.DeleteAsync(string spaceIdOrWikiName, int userRoleId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (userRoleId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userRoleId));
            }

            await DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/user_roles/{userRoleId}").ConfigureAwait(false);
        }
    }
}
