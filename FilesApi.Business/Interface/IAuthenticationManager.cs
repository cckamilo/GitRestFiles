using FilesApi.Utilities.Response;
using System;
using System.Threading.Tasks;

namespace FilesApi.Business.Interface
{
    public interface IAuthenticationManager
    {
        UserResponse Authenticate(string username, string password);

    }
}
