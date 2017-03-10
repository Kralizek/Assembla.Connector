using System.IO;
using System.Net.Http;
using System.Text;
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

        public static FileContent FromString(string content, Encoding encoding = null, string contentType = null, string fileName = null) => new StringFileContent(content, encoding, contentType) {FileName = fileName};

        public static FileContent FromObjectAsJson<T>(T content, JsonSerializerSettings serializerSettings = null, Encoding encoding = null, string fileName = null)
        {
            var jsonContent = JsonConvert.SerializeObject(content, serializerSettings);

            return new StringFileContent(jsonContent, encoding, "application/json") {FileName = fileName};
        }
    }
}