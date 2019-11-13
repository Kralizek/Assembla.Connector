using System.IO;
using Kralizek.Assembla.Connector.Files;
using Kralizek.Assembla.Connector.Files.Content;
using NUnit.Framework;
using Shouldly;

namespace Tests.Connector.Files
{
    [TestFixture]
    public class FileContentTests
    {
        private const string FileName = "fileName.txt";

        [Test, CustomAutoData]
        public void FromString_returns_StringFileContent(string fileContent)
        {
            var content = FileContent.FromString(fileContent, fileName: FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<StringFileContent>();
        }

        [Test, CustomAutoData]
        public void FromByteArray_returns_ByteArrayFileContent(byte[] byteArray)
        {
            var content = FileContent.FromByteArray(byteArray, FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<ByteArrayFileContent>();
        }

        [Test, CustomAutoData]
        public void FromStream_returns_StreamFileContent(Stream stream)
        {
            var content = FileContent.FromStream(stream, FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<StreamFileContent>();
        }

        [Test, CustomAutoData]
        public void FromObjectAsJson_returns_StringFileContent(string text)
        {
            var obj = new {text};

            var content = FileContent.FromObjectAsJson(obj, fileName: FileName);

            content.FileName.ShouldBe(FileName);
            content.ShouldBeOfType<StringFileContent>();
        }
    }
}