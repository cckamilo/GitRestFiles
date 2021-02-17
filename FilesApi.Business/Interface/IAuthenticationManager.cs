using FilesApi.Utilities.Response;
using System;
using System.Threading.Tasks;

namespace FilesApi.Business.Interface
{
    public interface IAuthenticationManager
    {
        AuthenticationResponse Authenticate(string username, string password);

    }
}
