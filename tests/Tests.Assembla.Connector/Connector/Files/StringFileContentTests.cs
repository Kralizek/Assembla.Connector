using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files.Content;
using Shouldly;
using Xunit;

namespace Tests.Assembla.Connector.Files
{
    public class StringFileContentTests
    {
        [Fact]
        public async Task ToContent_returns_StringContent()
        {
            const string testContent = "Some Content";

            var fileContent = new StringFileContent(testContent);
            var httpContent = fileContent.ToContent();

            httpContent.ShouldNotBeNull();
            var actualValue = await httpContent.ReadAsStringAsync();

            actualValue.ShouldBe(testContent);
        }

        [Fact]
        public void ContentType_is_attached_to_HttpContent()
        {
            const string testContent = "Some Content";
            const string testContentType = "test/json";

            var fileContent = new StringFileContent(testContent, contentType: testContentType);
            var httpContent = fileContent.ToContent();

            httpContent.Headers.ContentType.MediaType.ShouldBe(testContentType);
        }
    }
}