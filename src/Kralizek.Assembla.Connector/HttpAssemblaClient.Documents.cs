using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Assembla.Documents;

namespace Assembla
{
    public partial class HttpAssemblaClient : IDocumentConnector
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

        async Task<IReadOnlyList<Document>> IDocumentConnector.GetAllAsync(string spaceIdOrWikiName, int? page, int? pageSize)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }

            var queryParameters = GetDocumentQueryParameters(page, pageSize);

            var documents = await GetJsonAsync<Document[]>($"/v1/spaces/{spaceIdOrWikiName}/documents", queryParameters).ConfigureAwait(false);

            return documents;
        }

        async Task<Document> IDocumentConnector.GetAsync(string spaceIdOrWikiName, string documentId)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var document = await GetJsonAsync<Document>($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}").ConfigureAwait(false);

            return document;
        }

        async Task<byte[]> IDocumentConnector.DownloadAsync(string spaceIdOrWikiName, string documentId)
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

        async Task<Document> IDocumentConnector.CreateAsync(string spaceIdOrWikiName, IFileContent fileContent, Document document, string folderName)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (fileContent == null)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            var content = CreateContent(fileContent, folderName, document);

            var createdDocument = await PostAsync<Document>("https://bigfiles.assembla.com" + $"/v1/spaces/{spaceIdOrWikiName}/documents", content).ConfigureAwait(false);

            return createdDocument;
        }

        private HttpContent CreateContent(IFileContent fileContent, string folderName = null, Document document = null)
        {
            var content = new MultipartFormDataContent
            {
                {fileContent.ToContent(), "document[file]", fileContent.FileName}
            };
            if (folderName != null)
            {
                content.Add(new StringContent(folderName), "folder_name");
            }
            if (document != null)
            {
                if (document.IsAttachedToTicket() || document.IsAttachedToMessage() || document.IsAttachedToMilestone())
                {
                    content.Add(new StringContent(document.AttachableType.Value.ToString("G")), "document[attachable_type]");
                    content.Add(new StringContent(document.AttachableId.Value.ToString()), "document[attachable_id]");
                }

                if (document.Description != null)
                {
                    content.Add(new StringContent(document.Description), "document[description]");
                }

                if (document.FileName != null)
                {
                    content.Add(new StringContent(document.FileName), "document[filename]");
                }

                if (document.Name != null)
                {
                    content.Add(new StringContent(document.FileName), "document[name]");
                }
            }
            return content;
        }

        async Task IDocumentConnector.UpdateAsync(string spaceIdOrWikiName, Document document, IFileContent fileContent)
        {
            if (spaceIdOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceIdOrWikiName));
            }
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            if (document.Id == null)
            {
                throw new ArgumentNullException(nameof(document.Id));
            }
            if (fileContent == null)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }

            var content = CreateContent(fileContent, document: document);
            await PutAsync("https://bigfiles.assembla.com" + $"/v1/spaces/{spaceIdOrWikiName}/documents/{document.Id}", content).ConfigureAwait(false);
        }

        async Task IDocumentConnector.DeleteAsync(string spaceIdOrWikiName, string documentId)
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
