using Azure.Storage.Blobs;
using FilesApi.DataAccess.Entities;
using FilesApi.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Implementaion
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobService(BlobServiceClient _blobServiceClient)
        {
            this.blobServiceClient = _blobServiceClient;
        }

        public async Task<List<string>> UploadFileBlobAsync(List<IFormFile> files)
        {
            List<string> lstUrl = new List<string>();
            var containerClient = blobServiceClient.GetBlobContainerClient("camisetas");
            foreach (var item in files)
            {
                var blobClient = containerClient.GetBlobClient(item.FileName);
                lstUrl.Add(blobClient.Uri.ToString());
                using (var stream = item.OpenReadStream())
                {
                    var result = await blobClient.UploadAsync(stream);

                }
            }
            return lstUrl;
        }

        public async Task<bool> DeleteBlobAsync(string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient("camisetas");
            var blobClient = containerClient.GetBlobClient(blobName);
            var result = await blobClient.DeleteIfExistsAsync();
            return result.Value;        
        }
    }
}
