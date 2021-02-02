using FilesApi.DataAccess.Data.Configuration;
using FilesApi.DataAccess.Entities.MongoDb;
using FilesApi.DataAccess.Interfaces.MongoDb;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilesApi.DataAccess.Implementaion.MongoDb
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : class, EntityBase
    {
        private readonly IMongoCollection<TEntity> _usersCollection;

        public MongoDbRepository(IStoreDataBaseSettings settings)
        {
            var mdbClient = new MongoClient(settings.connectionString);
            var database = mdbClient.GetDatabase(settings.dataBaseName);
            _usersCollection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            try
            {
                var all = await _usersCollection.FindAsync(Builders<TEntity>.Filter.Empty);
                return await all.ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
                }

                await _usersCollection.InsertOneAsync(entity);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return entity;

        }

        public IList<TEntity> SearchForAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq(i => i.id, entity.id);
                var res = await _usersCollection.FindOneAndReplaceAsync(filter, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return true;
        }

    }
}