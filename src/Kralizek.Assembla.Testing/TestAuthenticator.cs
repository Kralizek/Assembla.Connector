using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Kralizek.Assembla
{
    public class TestAuthenticator : AssemblaAuthenticator
    {
        private readonly HttpMessageHandler _innerTestHandler;
        private readonly Sender _sender;

        public TestAuthenticator(HttpMessageHandler innerTestHandler)
        {
            _innerTestHandler = innerTestHandler;
            if (innerTestHandler == null)
            {
                throw new ArgumentNullException(nameof(innerTestHandler));
            }

            _sender = BuildSender(innerTestHandler.GetType());
        }

        private static Sender BuildSender(Type type)
        {
            var method = type.GetTypeInfo().GetDeclaredMethod("SendAsync");

            return (handler, request, cancellationToken) => (Task<HttpResponseMessage>) method.Invoke(handler, new object[] {request, cancellationToken});
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _sender(_innerTestHandler, request, cancellationToken);
        }

        private delegate Task<HttpResponseMessage> Sender(HttpMessageHandler handler, HttpRequestMessage request, CancellationToken cancellationToken);
    }
}
