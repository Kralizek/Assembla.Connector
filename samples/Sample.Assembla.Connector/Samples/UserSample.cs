using System;
using System.Threading.Tasks;
using Assembla;
using Microsoft.Extensions.Logging;
using Shouldly;

namespace Sample.Assembla.Connector.Samples
{
    public class UserSample : ISample
    {
        private readonly ILogger _logger;

        public UserSample(ILogger<UserSample> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IAssemblaClient client)
        {
            var currentUser = await client.Users.GetAsync();

            _logger.LogInformation(currentUser, c => $"CURRENT: {c.Id} {c.Name} {c.Login} {c.Email}");

            var user = await client.Users.GetAsync(currentUser.Login);

            user.Login.ShouldBe(currentUser.Login);
            user.Name.ShouldBe(currentUser.Name);

            var picture = await client.Users.GetPictyreAsync(currentUser.Id);

            _logger.LogInformation($"Retrieved {picture.Length} bytes");

            using (var test = await client.CreateDisposableSpace())
            {
                var usersInSpace = await client.Users.GetInSpaceAsync(test.Space.Id);

                usersInSpace.ShouldContain(u => u.Id == currentUser.Id);
            }
        }
    }
}