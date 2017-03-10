using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla.Users
{
    public interface IUserConnector
    {
        Task<User> GetAsync();

        Task<User> GetAsync(string userIdOrLogin);

        Task<byte[]> GetPictyreAsync(string userIdOrLogin);

        Task<IReadOnlyList<User>> GetInSpaceAsync(string spaceId);
    }
}