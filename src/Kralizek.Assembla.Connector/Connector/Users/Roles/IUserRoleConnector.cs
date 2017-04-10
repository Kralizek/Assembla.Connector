using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Users.Roles
{
    public interface IUserRoleConnector
    {
        Task<IReadOnlyList<UserRole>> GetAsync(string spaceIdOrWikiName);

        Task<UserRole> GetAsync(string spaceIdOrWikiName, int userRoleId);

        Task<UserRole> CreateAsync(string spaceIdOrWikiName, UserRole userRole);

        Task UpdateAsync(string spaceIdOrWikiName, UserRole userRole);

        Task DeleteAsync(string spaceIdOrWikiName, int userRoleId);
    }
}