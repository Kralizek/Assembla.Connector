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

            var documents = await _client.GetAsync<Document[]>($"/v1/spaces/{spaceIdOrWikiName}/documents", queryParameters).ConfigureAwait(false);

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

            var document = await _client.GetAsync<Document>($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}").ConfigureAwait(false);

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

            var content = await _client.GetRawAsync($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}/download").ConfigureAwait(false);

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

            throw new NotSupportedException();
        }

        async Task IDocumentConnector.UpdateAsync(string spaceIdOrWikiName, Document document)
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

            await _client.PutAsync($"/v1/spaces/{spaceIdOrWikiName}/documents/{document.Id}", new DocumentRequest(document)).ConfigureAwait(false);
        }

        async Task IDocumentConnector.UpdateAsync(string spaceOrWikiName, string documentId, IFileContent fileContent)
        {
            if (spaceOrWikiName == null)
            {
                throw new ArgumentNullException(nameof(spaceOrWikiName));
            }
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }
            if (fileContent == null)
            {
                throw new ArgumentNullException(nameof(fileContent));
            }
            throw new NotSupportedException();
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

            await _client.DeleteAsync($"/v1/spaces/{spaceIdOrWikiName}/documents/{documentId}").ConfigureAwait(false);
        }
    }
}
