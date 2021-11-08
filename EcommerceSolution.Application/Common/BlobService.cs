using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace EcommerceSolution.Application.Common
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobClient;

        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<IEnumerable<string>> GetBlobsAsync(string containerName)
        {
            // This will me to access data inside the personal container
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            var files = new List<string>();

            var blobs = containerClient.GetBlobsAsync();

            await foreach (var item in blobs)
            {
                files.Add(item.Name);
            }

            return files;
        }

        public async Task<bool> DeleteBlobAsync(string name, string containerName)
        {
            // This will me to access data inside the personal container
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(name);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> GetBlobAsync(string name, string containerName)
        {
            // This will me to access data inside the personal container
            var containerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            var str = blobClient.Uri.AbsoluteUri;
            //return new BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
            return str;
        }

        public async Task<bool> UploadFileBlobAsync(string name, IFormFile file, string containerName)
        {
            try
            {
                var containerClient = _blobClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(name);
                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = file.ContentType
                };

                var blobInfo = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

                if (blobInfo != null)
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
