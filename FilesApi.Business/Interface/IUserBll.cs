
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Interface
{
    public interface IUserBll
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse> Get();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResponse> GetById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResponse> DeleteById(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<ServiceResponse> Update(Users user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<ServiceResponse> Insert(Users user);

    }
}
