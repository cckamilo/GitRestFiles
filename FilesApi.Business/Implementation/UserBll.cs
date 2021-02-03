using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.DataAccess.MongoDb.Interfaces;
using FilesApi.Utilities.Response;
using FilesApi.Utilities.Response.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Implementation
{
    public class UserBll : IUserBll
    {
     
        private readonly IUserRepository repository;

        private ServiceResponse response;

        public UserBll(ServiceResponse _response, IUserRepository _repository)
        {
         
            this.response = _response;
            this.repository = _repository;
        }

        public async Task<ServiceResponse> DeleteById(string id)
        {
           
            var result = await repository.DeleteByIdAsync(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> Get()
        {

           
            var result = await repository.GetAllAsync();
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> GetById(string id)
        {
         
            var result = await repository.GetByIdAsync(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> Insert(Users user)
        {
           
            var result = await repository.InsertAsync(user);
            response.body = new Body();
            response.body.result = result.id;
            return response;
        }

        public async Task<ServiceResponse> Update(Users user)
        {
      
            var result = await repository.UpdateAsync(user);
            response.body = new Body();
            response.body.result = result;
            return response;
        }
    }
}
