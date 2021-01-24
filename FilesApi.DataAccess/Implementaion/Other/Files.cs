using FilesApi.DataAccess.Entities;
using FilesApi.DataAccess.Interfaces;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using Renci.SshNet.Async;
using System.Linq;

namespace FilesApi.DataAccess.Implementaion
{
    public class Files : IFiles
    {
        private SftpResponse sftpResponse;
        private readonly IConfiguration config;


        public Files(IConfiguration _config, SftpResponse _sftpResponse)
        {
            config = _config;
            sftpResponse = _sftpResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<SftpResponse> ListUploadFiles(List<IFormFile> files)
        {
            string host = config.GetValue<string>("sftpConfig:host");
            string username = config.GetValue<string>("sftpConfig:username");
            string password = config.GetValue<string>("sftpConfig:password");
            int port = config.GetValue<int>("sftpConfig:port"); ;
            var connectionInfo = new ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));
            var sftp = new SftpClient(connectionInfo);
            try
            {
       
                sftp.Connect();
                var subPath = "/test.files/test/camisetas";
                var path = "/test.files/test/camisetas/";
                foreach (var item in files)
                {
                    using (var stream = item.OpenReadStream())
                    {
                        if (!sftp.Exists(subPath))
                        {
                            sftp.CreateDirectory(subPath);
                        }
                        await sftp.UploadAsync(stream, path + item.FileName);
                    }
                }
                sftp.Disconnect();
                sftpResponse.result = true;
                if (sftpResponse.result)
                {
                    sftpResponse.urlMain = "http://f28-preview.awardspace.net" + path + files[0].FileName;
                    sftpResponse.files = files.Select(x => "http://f28-preview.awardspace.net" + path + x.FileName).ToList();
                }
                return sftpResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> GetFiles(string path)
        {
            try
            {
                List<Files> files = new List<Files>();

                string host = config.GetValue<string>("sftpConfig:host");
                string username = config.GetValue<string>("sftpConfig:username");
                string password = config.GetValue<string>("sftpConfig:password");
                int port = config.GetValue<int>("sftpConfig:port");
                var connectionInfo = new Renci.SshNet.ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));
                var sftp = new SftpClient(connectionInfo);
                sftp.Connect();
                var directory = "/test.files/test/camisetas";
                var result = await sftp.ListDirectoryAsync(directory);
                sftp.Disconnect();
                if (result != null)
                {
                    var filter = result.Where(x => (x.Name != ".") && (x.Name != "..")).ToList();
                    foreach (var item in filter)
                    {
                        //Files file = new Files()
                        //{
                        //    url = "http://f28-preview.awardspace.net" + item.FullName
                        //};
                        //files.Add(file);
                    }
                }
                ServiceResponse response = new ServiceResponse(); 
                response.body.result = files;
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
