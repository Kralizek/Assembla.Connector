using System.Collections.Generic;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Users.Roles;

namespace Kralizek.Assembla.Connector.Users
{
    public interface IUserConnector
    {
        Task<User> GetAsync();

        Task<User> GetAsync(string userIdOrLogin);

        Task<byte[]> GetPictyreAsync(string userIdOrLogin);

        Task<IReadOnlyList<User>> GetInSpaceAsync(string spaceId);

        IUserRoleConnector UserRoles { get; }
    }
}