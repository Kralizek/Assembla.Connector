using System.IO;
using System.Text;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files.Content;
using NUnit.Framework;
using Shouldly;

namespace Tests.Connector.Files
{
    [TestFixture]
    public class StreamFileContentTests
    {
        [Test, CustomAutoData]
        public async Task ToContent_returns_valid_content(string testContent)
        {
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(testContent));

            var fileContent = new StreamFileContent(stream);
            var httpContent = fileContent.ToContent();

            httpContent.ShouldNotBeNull();
            var actualValue = await httpContent.ReadAsStringAsync();

            actualValue.ShouldBe(testContent);
        }
    }
}