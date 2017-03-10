using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla.Documents
{
    public interface IDocumentConnector
    {
        Task<IReadOnlyList<Document>> GetAllAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null);

        Task<Document> GetAsync(string spaceIdOrWikiName, string documentId);

        Task<byte[]> DownloadAsync(string spaceIdOrWikiName, string documentId);

        Task<Document> CreateAsync(string spaceIdOrWikiName, IFileContent fileContent, Document document = null, string folderName = null);

        Task UpdateAsync(string spaceIdOrWikiName, Document document);

        Task UpdateAsync(string spaceOrWikiName, string documentId, IFileContent fileContent);

        Task DeleteAsync(string spaceIdOrWikiName, string documentId);
    }

    public interface IFileContent
    {
        byte[] Content { get; }

        string FileName { get; }
    }
}