using System;
using System.Net.Http;
using System.Text;

namespace Kralizek.Assembla.Connector.Files.Content
{
    public class StringFileContent : FileContent
    {
        private readonly string _content;
        private readonly string _contentType;
        private readonly Encoding _encoding;

        public StringFileContent(string content, Encoding encoding = null, string contentType = null)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            _content = content;
            _contentType = contentType;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public override HttpContent ToContent()
        {
            return new StringContent(_content, _encoding, _contentType);
        }
    }
}