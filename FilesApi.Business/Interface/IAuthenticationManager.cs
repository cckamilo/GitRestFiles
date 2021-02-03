using System;
namespace FilesApi.Business.Interface
{
    public interface IAuthenticationManager
    {
        string Authenticate(string username, string password);

    }
}
