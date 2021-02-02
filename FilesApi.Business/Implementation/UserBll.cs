using FilesApi.Business.Interface;
using FilesApi.DataAccess.Entities.MongoDb;
using FilesApi.DataAccess.Interfaces.MongoDb;

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
        //private readonly IUserRepository userRepository;
        private readonly IUserRepository respository;

        private ServiceResponse response;

        public UserBll(ServiceResponse _response, IUserRepository _repository)
        {
            //this.userRepository = _userRepository;
            this.response = _response;
            this.respository = _repository;
        }

        public async Task<ServiceResponse> DeleteById(string id)
        {
            //var result = await userRepository.DeleteIdAsync(id);
            var result = await respository.DeleteByIdAsync(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> Get()
        {
            //var result = await userRepository.GetAsync();
            var result = await respository.GetAllAsync();
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> GetById(string id)
        {
            //var result = await userRepository.GetIdAsync(id);
            var result = await respository.GetByIdAsync(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> Insert(Users user)
        {
            //var result = await userRepository.InsertAsync(user);
            var result = await respository.InsertAsync(user);
            response.body = new Body();
            response.body.result = result.id;
            return response;
        }

        public async Task<ServiceResponse> Update(Users user)
        {
            //var result = await userRepository.UpdateAsync(id, user);
            var result = await respository.UpdateAsync(user);
            response.body = new Body();
            response.body.result = result;
            return response;
        }
    }
}
