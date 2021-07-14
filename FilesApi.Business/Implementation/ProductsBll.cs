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
using FilesApi.DataAccess.MongoDb.Interfaces;

namespace FilesApi.Business.Implementation
{
    public class ProductsBll : IProductsBll
    {
        private ServiceResponse response;
        private readonly IFiles iFiles;
        private readonly IBlobService iBlobService;
        private readonly IProductsRepository iRepository;

        public ProductsBll(ServiceResponse _response, IFiles _iFiles, IBlobService _iBlobService, IProductsRepository _iRepository)
        {
            this.response = _response;
            this.iFiles = _iFiles;
            this.iBlobService = _iBlobService;
            this.iRepository = _iRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(string id)
        {
            var blob = await iRepository.GetByIdAsync(id);
            if (blob != null)
            {
                foreach (var item in blob.filesName)
                {
                    await iBlobService.DeleteBlobAsync(item);
                }
                return await iRepository.DeleteByIdAsync(id);
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
            var result = await iRepository.GetByIdAsync(id);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Products>> GetProducts()
        {
            var result = await iRepository.GetAllAsync();            
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<bool> Update(Products products)
        {
           
             return await iRepository.UpdateAsync(products);
   
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
                return await iRepository.InsertAsync(products);
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
                var db = await iRepository.InsertAsync(products);
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
