using FilesApi.DataAccess.Entities;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Interface
{
    public interface IProducts
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse> GetProducts();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResponse> GetById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>                                        s
        /// <returns></returns>
        Task<ServiceResponse> DeleteById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<ServiceResponse> Update(string id, Products products);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<ServiceResponse> PostProducts(List<IFormFile> files, Products products);



    }
}
