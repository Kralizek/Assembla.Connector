using System;
using System.Threading.Tasks;
using Assembla;
using Assembla.Spaces;

namespace Sample.Assembla.Connector
{
    public static class AssemblaClientExtensions
    {
        public static async Task<DisposableSpace> CreateDisposableSpace(this IAssemblaClient client, string spaceName = "Test Space")
        {
            var testSpace = await client.Spaces.CreateAsync(new Space {Name = spaceName});

            return new DisposableSpace(client, testSpace);
        }

        public class DisposableSpace : IDisposable
        {
            private readonly IAssemblaClient _client;
            public Space Space { get; }

            public DisposableSpace(IAssemblaClient client, Space testSpace)
            {
                if (client == null) throw new ArgumentNullException(nameof(client));
                if (testSpace == null) throw new ArgumentNullException(nameof(testSpace));

                _client = client;
                Space = testSpace;
            }

            public void Dispose()
            {
                _client.Spaces.DeleteAsync(Space.WikiName).Wait();
            }
        }
    }
}