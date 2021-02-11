
using FilesApi.DataAccess.MongoDb.Configuration;
using FilesApi.DataAccess.MongoDb.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.MongoDb.Base
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : class, EntityBase
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoDbRepository(IStoreDataBaseSettings settings)
        {
            var mdbClient = new MongoClient(settings.connectionString);
            var database = mdbClient.GetDatabase(settings.dataBaseName);
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            try
            {
                var result = await _collection.DeleteOneAsync(i => i.id == id);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            try
            {
                var all = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);
                return await all.ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            try
            {
                var result = await _collection.Find<TEntity>(i => i.id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
                }

                await _collection.InsertOneAsync(entity);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return entity;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public  IList<TEntity> SearchForAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _collection.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();
            }
            catch (Exception)
            {
    
                return null;
            }


        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
               
                var filter = Builders<TEntity>.Filter.Eq(i => i.id, entity.id);
                var res = await _collection.FindOneAndReplaceAsync(filter, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
        }

    }
}