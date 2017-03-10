using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Files
{
    public interface IFileContent
    {
        HttpContent ToContent();

        string FileName { get; }
    }

    public abstract class FileContent : IFileContent
    {
        public string FileName { get; set; }

        public abstract HttpContent ToContent();

        public static FileContent FromByteArray(byte[] content, string fileName = null) => new ByteArrayFileContent(content) {FileName = fileName};

        public static FileContent FromStream(Stream content, string fileName = null) => new StreamFileContent(content) { FileName = fileName };

        public static FileContent FromString(string content, string fileName = null) => new StringFileContent(content) { FileName = fileName };

        public static FileContent FromObjectAsJson<T>(T content, string fileName = null) => new StringFileContent(JsonConvert.SerializeObject(content)) { FileName = fileName };
    }

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