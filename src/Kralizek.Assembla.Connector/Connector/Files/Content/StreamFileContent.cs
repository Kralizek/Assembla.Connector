using System;
using System.IO;
using System.Net.Http;

namespace Kralizek.Assembla.Connector.Files.Content
{
    public class StreamFileContent : FileContent
    {
        private readonly Stream _content;

        public StreamFileContent(Stream content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            _content = content;
        }

        public override HttpContent ToContent() => new StreamContent(_content);
    }
}