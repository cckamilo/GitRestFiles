using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.DataAccess.MongoDb.Interfaces;
using FilesApi.Utilities.Response;
using FilesApi.Utilities.Response.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Implementation
{
    public class UserBll : IUserBll
    {
     
        private readonly IUserRepository repository;

        private UserResponse response;

        public UserBll(UserResponse _response, IUserRepository _repository)
        {
         
            this.response = _response;
            this.repository = _repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteById(string id)
        {

            var result = await repository.DeleteByIdAsync(id);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserResponse>> Get()
        {
            var users = new List<UserResponse>();
            var result = await repository.GetAllAsync();
            if (result.Count > 0)
            {
                result.ToList().ForEach(x =>
                {
                    users.Add(new UserResponse
                    {
                        id = x.id,
                        username = x.userName,
                        role = x.role
                    });
                });
            }
            return users;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetById(string id)
        {
            var result = await repository.GetByIdAsync(id);

            if (result != null)
            {               
                response.id = result.id;
                response.role = result.role;
                response.username = result.userName;
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserResponse> Insert(Users user)
        {
            var result = await repository.InsertAsync(user);
            if (result != null)
            {
                response.id = result.id;
                response.role = result.role;
                response.username = result.userName;
            }
            return response;
        }

        public async Task<bool> Update(Users user)
        {
            var result = await repository.UpdateAsync(user);    
            return result;
        }
    }
}
