using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Kralizek.Assembla
{
    public class SecretKeyAuthenticator : AssemblaAuthenticator
    {
        private readonly AssemblaAuthenticatorOptions _options;

        public SecretKeyAuthenticator(IOptions<AssemblaAuthenticatorOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options.Value;
        }

        public override Uri ServiceUri => _options.ServiceUri;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Api-Key", _options.ApiKey);
            request.Headers.Add("X-Api-Secret", _options.ApiSecretKey);

            return base.SendAsync(request, cancellationToken);
        }
    }

    public class AssemblaAuthenticatorOptions
    {
        public string ApiKey { get; set; }

        public string ApiSecretKey { get; set; }

        public Uri ServiceUri { get; set; } = new Uri("https://api.assembla.com");
    }
}
