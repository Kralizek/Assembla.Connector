using System.IO;
using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Files.Content;
using Shouldly;
using Xunit;

namespace Tests.Assembla.Connector.Files
{
    public class FileContentTests
    {
        private const string FileName = "fileName.txt";

        [Fact]
        public void FromString_returns_StringFileContent()
        {
            const string fileContent = "Some Content";

            var content = FileContent.FromString(fileContent, fileName: FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<StringFileContent>();
        }

        [Fact]
        public void FromByteArray_returns_ByteArrayFileContent()
        {
            byte[] byteArray = {1, 2, 3, 4};

            var content = FileContent.FromByteArray(byteArray, FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<ByteArrayFileContent>();
        }

        [Fact]
        public void FromStream_returns_StreamFileContent()
        {
            var stream = Stream.Null;

            var content = FileContent.FromStream(stream, FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<StreamFileContent>();
        }

        [Fact]
        public void FromObjectAsJson_returns_StringFileContent()
        {
            var obj = new {text = "Hello World"};

            var content = FileContent.FromObjectAsJson(obj, fileName: FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<StringFileContent>();
        }
    }
}