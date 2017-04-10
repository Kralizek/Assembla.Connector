using System.Threading.Tasks;
using Kralizek.Assembla;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class UserRoleSample : ISample
    {
        public async Task Execute(IAssemblaClient client)
        {
            var currentUser = await client.Users.GetAsync();

            using (var test = await client.CreateDisposableSpace())
            {
                var userRolesInSpace = await client.Users.UserRoles.GetAsync(test.Space.Id);

                userRolesInSpace.ShouldContain(ur => ur.UserId == currentUser.Id);
            }
        }
    }
}