using FilesApi.DataAccess.Data;
using FilesApi.DataAccess.Entities;
using FilesApi.Models.Sftp;
using FilesApi.Utilities.Response;
using FilesApi.Utilities.Response.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using Renci.SshNet.Async;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Services
{
    public class FilesSftp
    {
        private SftpResponse sftpResponse;
        private readonly IConfiguration config;
        private ServiceResponse response;
        //MongoDb
        private readonly ProductsDb productsDb;


        public FilesSftp(IConfiguration _config,
                            ServiceResponse _response,
                            SftpResponse _sftpResponse,
                            ProductsDb _productsDb)
        {
            config = _config;
            response = _response;
            sftpResponse = _sftpResponse;
            productsDb = _productsDb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> UploadFiles(IFormFile file)
        {

            string host = config.GetValue<string>("sftpConfig:host");
            string username = config.GetValue<string>("sftpConfig:username");
            string password = config.GetValue<string>("sftpConfig:password");
            int port = config.GetValue<int>("sftpConfig:port"); ;

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
                sftpResponse.fileName = file.FileName;
                sftpResponse.result = true;

                if (response != null)
                {
                    response = new ServiceResponse();
                    response.body = new Body();
                    response.body.result = sftpResponse;

                    if (sftpResponse.result)
                    {
                        Products product = new Products
                        {
                            Name = file.FileName,
                            filePath = "http://" + "f28-preview.awardspace.net" + fileName,
                            date = DateTime.Now.ToString(),
                            price = 30000
                        };

                        productsDb.Create(product);
                    }


                }

                return response;
            }
            catch (Exception ex)
            {

                sftpResponse.result = false;

                throw;
            }
        }

        public async Task<ServiceResponse> ListFiles(string path)
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
                var directory = "/test.files/files";
                var result = await sftp.ListDirectoryAsync(directory);
                sftp.Disconnect();
                if (result != null)
                {
                    var filter = result.Where(x => (x.Name != ".") && (x.Name != "..")).ToList();
                    foreach (var item in filter)
                    {
                        Files file = new Files()
                        {
                            url = "f28-preview.awardspace.net" + item.FullName
                        };
                        files.Add(file);
                    }
                }
                response = new ServiceResponse();
                response.body = new Body();
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
