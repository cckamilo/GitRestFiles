using System;
using FilesApi.DataAccess.MongoDb.Base;
using FilesApi.DataAccess.MongoDb.Configuration;
using FilesApi.DataAccess.MongoDb.Entities;
using FilesApi.DataAccess.MongoDb.Interfaces;

namespace FilesApi.DataAccess.MongoDb.Repository
{
    public class ProductsRepository : MongoDbRepository<Products>, IProductsRepository
    {
        public ProductsRepository(IStoreDataBaseSettings settings) : base(settings)
        {
        }
    }
}
