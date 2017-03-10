using System;
using System.Net.Http;

namespace Kralizek.Assembla.Connector.Files.Content
{
    public class StringFileContent : FileContent
    {
        private readonly string _content;

        public StringFileContent(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            _content = content;
        }

        public override HttpContent ToContent()
        {
            return new StringContent(_content);
        }
    }
}