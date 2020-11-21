using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using Renci.SshNet.Async;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Services
{
    public class FilesSftp
    {
        private SftpResponse response;
        private readonly IConfiguration _config;

        public FilesSftp(IConfiguration config)
        {
            _config = config;
        }

        public async Task<SftpResponse> UploadFiles(IFormFile file)
        {
            response = new SftpResponse();

            string host = _config.GetValue<string>("sftpConfig:host");
            string username = _config.GetValue<string>("sftpConfig:username");
            string password = _config.GetValue<string>("sftpConfig:password");
            int port = _config.GetValue<int>("sftpConfig:port"); ;

            //string host = "f28-preview.awardspace.net";
            //string usename = "3655500";
            //string password = "Kreator6";
            //int port = 221;

            var connectionInfo = new Renci.SshNet.ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));
            var sftp = new SftpClient(connectionInfo);
            sftp.Connect();

            try
            {
                var fileName = "/test.files/" + file.FileName;
                using (var stream = file.OpenReadStream())
                {
                    await sftp.UploadAsync(stream, fileName);
                }
                sftp.Disconnect();
                response.path = fileName;
                response.fileName = file.FileName;
                response.result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.result = false;
                response.message = ex.ToString();
                throw;
            }
        }
    }
}
