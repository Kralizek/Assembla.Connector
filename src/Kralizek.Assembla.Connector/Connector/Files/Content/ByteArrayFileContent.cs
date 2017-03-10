using System;
using System.Net.Http;

namespace Kralizek.Assembla.Connector.Files.Content
{
    public class ByteArrayFileContent : FileContent
    {
        private readonly byte[] _content;

        public ByteArrayFileContent(byte[] content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            _content = content;
        }

        public override HttpContent ToContent() => new ByteArrayContent(_content);
    }
}