using FilesApi.Business.Interface;
using FilesApi.DataAccess.Data;
using FilesApi.Utilities.Response;
using FilesApi.Utilities.Response.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.Business.Implementation
{
    public class ProductsBll : IProducts
    {
        private readonly ProductsDb productsDb;
        private ServiceResponse response;
        public ProductsBll(ProductsDb _productsDb)
        {
            this.productsDb = _productsDb;
        }

        public async Task<ServiceResponse> GetProducts()
        {

            var result = await productsDb.Get();

            response = new ServiceResponse();
            response.body = new Body();
            response.body.result = result;

            return response;

        }
    }
}
