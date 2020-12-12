using FilesApi.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Interface
{
    public interface IProducts
    {
       Task<ServiceResponse> GetProducts();


    }
}
