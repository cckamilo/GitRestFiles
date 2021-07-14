
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Interface
{
    public interface IProductsBll
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<Products>> GetProducts();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Products> GetById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        Task<bool> DeleteById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<bool> Update(Products products);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<ServiceResponse> PostProducts(List<IFormFile> files, Products products);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<Products> UploadFilesAsync(List<IFormFile> files, Products products);



    }
}
