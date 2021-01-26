using FilesApi.Business.Interface;
using FilesApi.DataAccess.Data;
using FilesApi.DataAccess.Entities;
using FilesApi.Utilities.Response;
using FilesApi.Utilities.Response.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FilesApi.DataAccess.Interfaces;
using System.Linq;
namespace FilesApi.Business.Implementation
{
    public class ProductsBll : IProductsBll
    {
        private readonly ProductsDb productsDb;
        private ServiceResponse response;
        private readonly IFiles iFiles;
        private readonly IBlobService iBlobService;
        public ProductsBll(ProductsDb _productsDb, ServiceResponse _response, IFiles _iFiles, IBlobService _iBlobService)
        {
            this.productsDb = _productsDb;
            this.response = _response;
            this.iFiles = _iFiles;
            this.iBlobService = _iBlobService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> DeleteById(string id)
        {
            var blob = await productsDb.GetById(id);
            if (blob != null)
            {
                foreach (var item in blob.filesName)
                {
                    await iBlobService.DeleteBlobAsync(item);
                }
                var result = await productsDb.DeleteById(id);
                response.body = new Body();
                response.body.result = result;
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> GetById(string id)
        {
            var result = await productsDb.GetById(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse> GetProducts()
        {
            var result = await productsDb.Get();
            response.body = new Body();
            response.body.result = result;
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Update(string id, Products products)
        {
            bool result;

            if (products.price > 0 && products.title != null && products.description != null && products.size != null && products.quantity > 0)
            {
                result = await productsDb.Update(id, products);
            }
            else
            {
                result = false;
            }
            response.body = new Body();
            response.body.result = result;
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> UploadFilesAsync(List<IFormFile> files, Products products)
        {
            var lstResult = await iBlobService.UploadFileBlobAsync(files);
            if (lstResult != null)
            {
                products.urlMain = lstResult[0];
                products.files = lstResult;
                products.date = DateTime.Now.ToString();
                products.filesName = files.Select(i => i.FileName).ToList();
                var db = await productsDb.Create(products);
                if (response != null)
                {
                    response.body = new Body();
                    response.body.result = db.id;
                }
            }
            return response;
        }
        /// <summary>
        /// Metodo que guarda archivos en sftp - No se utiliza, se guarda como aprendizaje
        /// </summary>
        /// <param name="files"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> PostProducts(List<IFormFile> files, Products products)
        {
            var result = await iFiles.ListUploadFiles(files);
            if (result != null)
            {
                products.urlMain = result.urlMain;
                products.files = result.files;
                products.date = DateTime.Now.ToString();
                var db = await productsDb.Create(products);
                if (response != null)
                {
                    response.body = new Body();
                    response.body.result = db.id;
                }
            }
            return response;
        }
    }
}
