using System.IO;
using System.Text;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files.Content;
using Shouldly;
using Xunit;

namespace Tests.Assembla.Connector.Files
{
    public class StreamFileContentTests
    {
        [Fact]
        public async Task ToContent_returns_valid_content()
        {
            const string testContent = "Some Content";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(testContent));

            var fileContent = new StreamFileContent(stream);
            var httpContent = fileContent.ToContent();

            httpContent.ShouldNotBeNull();
            var actualValue = await httpContent.ReadAsStringAsync();

            actualValue.ShouldBe(testContent);
        }
    }
}