using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kralizek.Assembla.Connector.Users
{
    public interface IUserConnector
    {
        Task<User> GetAsync();

        Task<User> GetAsync(string userIdOrLogin);

        Task<byte[]> GetPictyreAsync(string userIdOrLogin);

        Task<IReadOnlyList<User>> GetInSpaceAsync(string spaceId);
    }
}