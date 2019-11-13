using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files.Content;
using Shouldly;
using NUnit.Framework;

namespace Tests.Connector.Files
{
    public class StringFileContentTests
    {
        [Test, CustomAutoData]
        public async Task ToContent_returns_StringContent(string testContent)
        {
            var fileContent = new StringFileContent(testContent);
            var httpContent = fileContent.ToContent();

            httpContent.ShouldNotBeNull();
            var actualValue = await httpContent.ReadAsStringAsync();

            actualValue.ShouldBe(testContent);
        }

        [Test, CustomAutoData]
        public void ContentType_is_attached_to_HttpContent(string testContent)
        {
            const string testContentType = "test/json";

            var fileContent = new StringFileContent(testContent, contentType: testContentType);
            var httpContent = fileContent.ToContent();

            httpContent.Headers.ContentType.MediaType.ShouldBe(testContentType);
        }
    }
}