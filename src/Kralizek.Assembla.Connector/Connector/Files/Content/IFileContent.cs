using System.Net.Http;

namespace Kralizek.Assembla.Connector.Files.Content
{
    public interface IFileContent
    {
        HttpContent ToContent();

        string FileName { get; }
    }
}