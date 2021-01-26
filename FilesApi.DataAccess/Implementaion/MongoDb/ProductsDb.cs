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
            try
            {
                var result = await _productsCollection.Find<Products>(item => item.id != null).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Products> GetById(string id)
        {
            try
            {
                var result = await _productsCollection.Find<Products>(item => item.id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        public async Task<bool> Update(string id, Products product)
        {

            try
            {
                var update = Builders<Products>.Update.Set(a => a.price, product.price)
                    .Set(a => a.title, product.title)
                    .Set(a => a.description, product.description)
                    .Set(a => a.size, product.size)
                    .Set(a => a.quantity, product.quantity);

                var result = await _productsCollection.UpdateOneAsync(item => item.id == id, update);
                return result.IsModifiedCountAvailable;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<Products> Create(Products product)
        {
            try
            {
                await _productsCollection.InsertOneAsync(product);
                return product;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public void Delete(Products product)
        {
            _productsCollection.DeleteOne(item => item.id == product.id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> DeleteById(string id)
        {
            var result = await _productsCollection.DeleteOneAsync(item => item.id == id);
            if (result.DeletedCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
