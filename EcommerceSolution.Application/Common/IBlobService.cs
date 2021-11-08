using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace EcommerceSolution.Application.Common
{
    public interface IBlobService
    {
        public Task<IEnumerable<string>> GetBlobsAsync(string containerName);
        public Task<bool> DeleteBlobAsync(string name, string containerName);
        public Task<string> GetBlobAsync(string name, string containerName);
        public Task<bool> UploadFileBlobAsync(string name, IFormFile file, string containerName);
    }
}