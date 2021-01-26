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
        private readonly IUserRepository userRepository;

        private ServiceResponse response;

        public UserBll(IUserRepository _userRepository, ServiceResponse _response)
        {
            this.userRepository = _userRepository;
            this.response = _response;
        }

        public async Task<ServiceResponse> DeleteById(string id)
        {
            var result = await userRepository.DeleteIdAsync(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> Get()
        {
            var result = await userRepository.GetAsync();
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> GetById(string id)
        {
            var result = await userRepository.GetIdAsync(id);
            response.body = new Body();
            response.body.result = result;
            return response;
        }

        public async Task<ServiceResponse> Insert(Users user)
        {
            var result = await userRepository.InsertAsync(user);
            response.body = new Body();
            response.body.result = result.id;
            return response;
        }

        public async Task<ServiceResponse> Update(string id, Users user)
        {
            var result = await userRepository.UpdateAsync(id, user);
            response.body = new Body();
            response.body.result = result;
            return response;
        }
    }
}
