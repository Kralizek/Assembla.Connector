using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Assembla.Files;

namespace Assembla
{
    public partial class HttpAssemblaClient : IFileConnector
    {
        private IReadOnlyDictionary<string, string> GetDocumentQueryParameters(int? page = null, int? pageSize = null)
        {
            var queryParameters = new Dictionary<string, string>();

            if (page.HasValue)
            {
                queryParameters.Add("page", page.Value.ToString("D"));
            }
            if (pageSize.HasValue)
            {
                queryParameters.Add("per_page", pageSize.Value.ToString("D"));
            }
            return queryParameters;
        }

        async Task<IReadOnlyList<File>> IFileConnector.GetAllAsync(string spaceIdOrWikiName, int? page, int? pageSize)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetDocumentQueryParameters(page, pageSize);

            var documents = await GetJsonAsync<File[]>($"/v1/spaces/{spaceIdOrWikiName}/documents", queryParameters).ConfigureAwait(false);

            return documents;
        }

        async Task<File> IFileConnector.GetAsync(string spaceIdOrWikiName, string documentId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var document = await GetJsonAsync<File>($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}").ConfigureAwait(false);

            return document;
        }

        async Task<byte[]> IFileConnector.DownloadAsync(string spaceIdOrWikiName, string documentId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var content = await GetRawAsync($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}/download").ConfigureAwait(false);

            return content;
        }

        async Task<File> IFileConnector.CreateAsync(string spaceIdOrWikiName, IFileContent fileContent, File file, string folderName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (fileContent == null)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            var content = CreateContent(fileContent, folderName, file);

            var createdDocument = await PostAsync<File>("https://bigfiles.assembla.com" + $"/v1/spaces/{spaceIdOrWikiName}/documents", content).ConfigureAwait(false);

            return createdDocument;
        }

        private HttpContent CreateContent(IFileContent fileContent, string folderName = null, File file = null)
        {
            var content = new MultipartFormDataContent
            {
                {fileContent.ToContent(), "document[file]", fileContent.FileName}
            };
            if (folderName != null)
            {
                content.Add(new StringContent(folderName), "folder_name");
            }
            if (file != null)
            {
                if (file.IsAttachedToTicket() || file.IsAttachedToMessage() || file.IsAttachedToMilestone())
                {
                    content.Add(new StringContent(file.AttachableType.Value.ToString("G")), "document[attachable_type]");
                    content.Add(new StringContent(file.AttachableId), "document[attachable_id]");
                }

                if (file.Description != null)
                {
                    content.Add(new StringContent(file.Description), "document[description]");
                }

                if (file.FileName != null)
                {
                    content.Add(new StringContent(file.FileName), "document[filename]");
                }

                if (file.Name != null)
                {
                    content.Add(new StringContent(file.Name), "document[name]");
                }
            }
            return content;
        }

        async Task IFileConnector.UpdateAsync(string spaceIdOrWikiName, File file, IFileContent fileContent)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            if (file.Id == null)
            {
                throw new ArgumentNullException(nameof(file.Id));
            }
            if (fileContent == null)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            var content = CreateContent(fileContent, file: file);
            await PutAsync("https://bigfiles.assembla.com" + $"/v1/spaces/{spaceIdOrWikiName}/documents/{file.Id}", content).ConfigureAwait(false);
        }

        async Task IFileConnector.DeleteAsync(string spaceIdOrWikiName, string documentId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            await DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}").ConfigureAwait(false);
        }
    }
}
