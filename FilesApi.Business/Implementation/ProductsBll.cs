using FilesApi.Business.Interface;
using FilesApi.Utilities.Response;
using FilesApi.Utilities.Response.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using FilesApi.DataAccess.MongoDb.Repository;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.DataAccess.Other;
using FilesApi.DataAccess.Azure;

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
        public async Task<bool> DeleteById(string id)
        {
            var blob = await productsDb.GetById(id);
            if (blob != null)
            {
                foreach (var item in blob.filesName)
                {
                    await iBlobService.DeleteBlobAsync(item);
                }
                return await productsDb.DeleteById(id);
            }
            else
            {
                return false;
            }
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Products> GetById(string id)
        {         
            var result = await productsDb.GetById(id);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Products>> GetProducts()
        {
            var result = await productsDb.Get();            
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<bool> Update(string id, Products products)
        {

             return await productsDb.Update(id, products);
   
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<Products> UploadFilesAsync(List<IFormFile> files, Products products)
        {
            var lstResult = await iBlobService.UploadFileBlobAsync(files);
            if (lstResult != null)
            {
                products.urlMain = lstResult[0];
                products.files = lstResult;
                products.date = DateTime.Now.ToString();
                products.filesName = files.Select(i => i.FileName).ToList();
                return await productsDb.Create(products);
            }
            else
            {
                return null;
            }
  
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
