using FilesApi.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Interfaces
{
    public interface IBlobService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<List<string>> UploadFileBlobAsync(List<IFormFile> files);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        Task<bool> DeleteBlobAsync(string blobName);


    }
}
