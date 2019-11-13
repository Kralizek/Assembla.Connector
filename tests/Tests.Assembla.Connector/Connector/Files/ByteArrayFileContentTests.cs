using System.Text;
using System.Threading.Tasks;
using Kralizek.Assembla.Connector.Files.Content;
using NUnit.Framework;

namespace Tests.Connector.Files
{
    [TestFixture]
    public class ByteArrayFileContentTests
    {
        [Test, CustomAutoData]
        public async Task ToContent_returns_valid_content(string testContent)
        {
            var bytes = Encoding.UTF8.GetBytes(testContent);

            var fileContent = new ByteArrayFileContent(bytes);
            var httpContent = fileContent.ToContent();
            var actualValue = await httpContent.ReadAsStringAsync();

            Assert.That(httpContent, Is.Not.Null);
            Assert.That(actualValue, Is.EqualTo(testContent));
        }
    }
}