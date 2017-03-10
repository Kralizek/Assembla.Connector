using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assembla.Files
{
    public interface IFileConnector
    {
        Task<IReadOnlyList<File>> GetAllAsync(string spaceIdOrWikiName, int? page = null, int? pageSize = null);

        Task<File> GetAsync(string spaceIdOrWikiName, string documentId);

        Task<byte[]> DownloadAsync(string spaceIdOrWikiName, string documentId);

        Task<File> CreateAsync(string spaceIdOrWikiName, IFileContent fileContent, File file = null, string folderName = null);

        Task UpdateAsync(string spaceOrWikiName, File file, IFileContent fileContent = null);

        Task DeleteAsync(string spaceIdOrWikiName, string documentId);
    }
}