using FilesApi.DataAccess.Data.Configuration;
using FilesApi.DataAccess.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Data
{
    public class ProductsDb
    {
        private readonly IMongoCollection<Products> _productsCollection;
        public ProductsDb(IStoreDataBaseSettings settings)
        {
            var mdbClient = new MongoClient(settings.connectionString);
            var database = mdbClient.GetDatabase(settings.dataBaseName);
            _productsCollection = database.GetCollection<Products>(settings.collectionName);
        }



        public async Task<List<Products>> Get()
        {
            var result = await _productsCollection.FindAsync<Products>(item => item.id != null);
            return await result.ToListAsync(); 
        }

        public Products GetById(string id)
        {
            return _productsCollection.Find<Products>(item => item.id != id).FirstOrDefault();
        }

        public void Update(string id, Products product)
        {
            _productsCollection.ReplaceOne(item => item.id == id, product);
        }

        public Products Create(Products product)
        {
            _productsCollection.InsertOne(product);
            return product;
        }

        public void Delete(Products product)
        {
            _productsCollection.DeleteOne(item => item.id == product.id);
        }

        public void DeleteById(string id)
        {
            _productsCollection.DeleteOne(item => item.id == id);
        }


    }
}
