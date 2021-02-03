using System;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.MongoDb.Interfaces;
using System.Linq;

namespace FilesApi.Business.Implementation
{
    public class AuthenticationManager: IAuthenticationManager
    {
        private readonly IUserRepository repository;
        public AuthenticationManager(IUserRepository _respository)
        {
            this.repository = _respository;
        }

        public string Authenticate(string username, string password)
        {

            var result = repository.SearchForAsync(x => x.userName == username && x.password == password);

            if (result.Count > 0)
            {

            }
            else
            {

            }

            throw new NotImplementedException();
        }
    }
}
