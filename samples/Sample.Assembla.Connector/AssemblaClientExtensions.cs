using System;
using System.Threading.Tasks;
using Kralizek.Assembla;
using Kralizek.Assembla.Connector.Spaces;
using Kralizek.Assembla.Connector.Spaces.Tools;
using Kralizek.Assembla.Connector.Users;

namespace Sample.Assembla.Connector
{
    public static class AssemblaClientExtensions
    {
        public static async Task<DisposableSpace> CreateDisposableSpace(this IAssemblaClient client, string spaceName = "Test Space", params ToolType[] requiredTools)
        {
            var testSpace = await client.Spaces.CreateAsync(new Space {Name = spaceName});

            foreach (var toolType in requiredTools)
            {
                await client.Spaces.Tools.AddAsync(testSpace.WikiName, toolType);
            }

            var currentUser = await client.Users.GetAsync();

            return new DisposableSpace(client, testSpace, currentUser);
        }
    }

    public class DisposableSpace : IDisposable
    {
        private readonly IAssemblaClient _client;

        public Space Space { get; }

        public User CurrentUser { get; }

        public DisposableSpace(IAssemblaClient client, Space testSpace, User currentUser)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (testSpace == null)
            {
                throw new ArgumentNullException(nameof(testSpace));
            }
            if (currentUser == null)
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            _client = client;
            Space = testSpace;
            CurrentUser = currentUser;
        }

        public void Dispose()
        {
            _client.Spaces.DeleteAsync(Space.WikiName).Wait();
        }
    }

}