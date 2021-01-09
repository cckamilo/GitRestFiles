using FilesApi.DataAccess.Entities;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Interfaces
{
    public interface IFiles
    {
        Task<SftpResponse> ListUploadFiles(List<IFormFile> files);

    }
}
