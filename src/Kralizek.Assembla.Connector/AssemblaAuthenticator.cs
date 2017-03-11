using System;
using System.Net.Http;

namespace Kralizek.Assembla
{
    public abstract class AssemblaAuthenticator : HttpClientHandler
    {
        public virtual Uri ServiceUri { get; } = new Uri("https://api.assembla.com");
    }
}