using System.IO;
using System.Net.Http;
using Kralizek.Assembla.Connector.Files.Content;
using Newtonsoft.Json;

namespace Kralizek.Assembla.Connector.Files
{
    public abstract class FileContent : IFileContent
    {
        public string FileName { get; set; }

        public abstract HttpContent ToContent();

        public static FileContent FromByteArray(byte[] content, string fileName = null) => new ByteArrayFileContent(content) {FileName = fileName};

        public static FileContent FromStream(Stream content, string fileName = null) => new StreamFileContent(content) { FileName = fileName };

        public static FileContent FromString(string content, string fileName = null) => new StringFileContent(content) { FileName = fileName };

        public static FileContent FromObjectAsJson<T>(T content, string fileName = null) => new StringFileContent(JsonConvert.SerializeObject(content)) { FileName = fileName };
    }
}